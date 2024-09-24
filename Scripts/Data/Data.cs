using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class Data
{
    public enum BaseType
    {
        NONE,
        INT,
        FLOAT,
        STRING
    }
    static public DataBlock CreateData(string name, DefLibrary df)
    {
        DataBlock data = new DataBlock();

        data.Type = df.GetDBType("_" + name, BaseType.NONE);
        data.Name = name; 

        return data;
    }

    static public DataBlock CreateData(string name, int value, DefLibrary df)
    {
        DataBlock data = new DataBlock();

        data.Type = df.GetDBType("i_" + name, BaseType.NONE);
        data.Name = name;
        data.ValueI = value;

        return data;
    }

    static public DataBlock CreateData(string name, string value, DefLibrary df)
    {
        DataBlock data = new DataBlock();

        data.Type = df.GetDBType("s_" + name, BaseType.STRING);
        data.Name = name;
        data.ValueS = value;

        return data;
    }

    static public DataBlock AddData(DataBlock parent, string name, DefLibrary df)
    {
        DataBlock data = new DataBlock();

        data.Type = df.GetDBType("_" + name, BaseType.NONE);
        data.Name = name;

        parent.Subs.Add(data);
        //data.Parent = parent;

        return data;
    }

    static public DataBlock AddData(DataBlock parent, string name, int value, DefLibrary df)
    {
        DataBlock data = new DataBlock();

        data.Type = df.GetDBType("i_" + name, BaseType.INT);
        data.Name = name;
        data.ValueI = value;

        parent.Subs.Add(data);
        //data.Parent = parent;

        return data;
    }

    static public DataBlock AddData(DataBlock parent, string name, string value, DefLibrary df)
    {
        DataBlock data = new DataBlock();

        data.Type = df.GetDBType("s_" + name, BaseType.STRING);
        data.Name = name;
        data.ValueS = value;

        parent.Subs.Add(data);
        //data.Parent = parent;

        return data;
    }

    static public void RemoveData(DataBlock parent, string name, DefLibrary df)
    {
        int type = df.GetDBType("_" + name, BaseType.STRING);

        for (int idx = 0; idx < parent.Subs.Count; idx++)
        {
            DataBlock data = parent.Subs[idx];
            if (data.Type == type)
            {
                parent.Subs.RemoveAt(idx);
                break;
            }
        }
    }

    static public void RemoveData(DataBlock parent, string name, int value, DefLibrary df)
    {
        int type = df.GetDBType("i_" + name, BaseType.STRING);

        for (int idx = 0; idx < parent.Subs.Count; idx++)
        {
            DataBlock data = parent.Subs[idx];
            if (data.Type == type && data.ValueI == value)
            {
                parent.Subs.RemoveAt(idx);
                break;
            }
        }
    }

    static public void RemoveData(DataBlock parent, string name, string value, DefLibrary df)
    {
        int type = df.GetDBType("s_" + name, BaseType.STRING);

        for (int idx = 0; idx < parent.Subs.Count; idx++)
        {
            DataBlock data = parent.Subs[idx];
            if (data.Type == type && data.ValueS == value)
            {
                parent.Subs.RemoveAt(idx);
                break;
            }
        }
    }

    // ----------------------------------------------------------------------------------------------------
    static public void ChangeDataType(DataBlock data, string name, DefLibrary df)
    {
        Data.BaseType baseType = (Data.BaseType)(data.Type/10000);
        switch (baseType)
        {
            case BaseType.NONE: data.Type = df.GetDBType("_" + name, BaseType.STRING); break;
            case BaseType.INT: data.Type = df.GetDBType("i_" + name, BaseType.STRING); break;
            case BaseType.STRING: data.Type = df.GetDBType("s_" + name, BaseType.STRING); break;
        }

        data.Name = name;
    }

    // ----------------------------------------------------------------------------------------------------
    static public bool DeleteData(DataBlock original, DataBlock dataToRemove)
    {
        for (int oriIdx = 0; oriIdx < original.Subs.Count; oriIdx++)
        {
            for (int otherIdx = 0; otherIdx < dataToRemove.Subs.Count; otherIdx++)
            {
                if (original.Subs[oriIdx].Name == dataToRemove.Subs[otherIdx].Name
                    && original.Subs[oriIdx].ValueI == dataToRemove.Subs[otherIdx].ValueI
                    && original.Subs[oriIdx].ValueS == dataToRemove.Subs[otherIdx].ValueS)
                {
                    bool deletedAllSubs = DeleteData(original.Subs[oriIdx], dataToRemove.Subs[otherIdx]);
                    if (deletedAllSubs)
                    {
                        original.Subs.RemoveAt(oriIdx);
                        oriIdx--;
                        break;
                    }
                }
            }
        }
        return original.Subs.Count == 0;
    }

    // ----------------------------------------------------------------------------------------------------
    static public DataBlock LoadFile(string fileName, DefLibrary defLib)
    {
        using var file = FileAccess.Open("res:///" + fileName, FileAccess.ModeFlags.Read);
        string content = file.GetAsText();

        char[] delimiters = { '\n', '\r' ,'\t' };
        string[] rows = content.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        int rowIdx = -1;

        List<string> words = new List<string>();
        string[] wordsOnRow = LoadFile_GetWordsFromNextValidRow(rows, ref rowIdx);
        while (wordsOnRow != null)
        {
            if (words.Count > 0 && words[words.Count - 1] != "{" && words[words.Count - 1] != "}" && wordsOnRow[0] != "{")
            {
                words.Add("{");
                words.Add("}");
            }
            words.AddRange(wordsOnRow);
            wordsOnRow = LoadFile_GetWordsFromNextValidRow(rows, ref rowIdx);
        }

        return LoadData(words, defLib);
    }

    static string[] LoadFile_GetWordsFromNextValidRow(string[] rows, ref int rowIdx)
    {
        rowIdx++;
        if (rowIdx >= rows.Length)
        {
            // GD.PrintErr("Load map data error 01");
            return null;
        }
        rows[rowIdx] = rows[rowIdx].Replace("res://", "$res$");
        string[] words = rows[rowIdx].Split("//")[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (words.Length == 0)
        {
            return LoadFile_GetWordsFromNextValidRow(rows, ref rowIdx);
        }
        for (int i = 0; i < words.Length; i++) words[i] = words[i].Replace("$res$", "res://");
        return words;
    }

    static public DataBlock LoadData(List<string> words, DefLibrary df)
    {
        DataBlock data = new DataBlock();

        BaseType baseType = BaseType.INT;
        if (words[1] == "{") baseType = BaseType.NONE;
        else if (System.Char.IsLetter(words[1][0])) baseType = BaseType.STRING;
        else if (words[1][0] == '#') baseType = BaseType.STRING;
        else if (words[1].Contains(".")) baseType = BaseType.FLOAT;

        string prefix = "_";
        if (baseType == BaseType.INT) prefix = "i_";
        if (baseType == BaseType.STRING) prefix = "s_";
        data.Type = df.GetDBType(prefix + words[0], baseType);
        data.Name = words[0];
        switch (baseType)
        {
            case BaseType.INT: data.ValueI = words[1].ToInt(); break;
            case BaseType.STRING: data.ValueS = words[1]; break;
        }

        int subDataStart = 1;
        while (words[subDataStart] != "{") subDataStart++;
        subDataStart++; // {

        while (words[subDataStart] != "}")
        {
            int nextSubStart = subDataStart;
            int nextSubEnd = nextSubStart + 1;
            while (words[nextSubEnd] != "{") nextSubEnd++;
            int depth = 1;
            while (depth > 0)
            {
                nextSubEnd++;
                if (words[nextSubEnd] == "{") depth++;
                if (words[nextSubEnd] == "}") depth--;
            }

            DataBlock subData = LoadData(words.GetRange(nextSubStart, nextSubEnd + 1 - nextSubStart), df);
            data.Subs.Add(subData);
            //subData.Parent = data;

            subDataStart = nextSubEnd + 1;
        }

        return data;
    }

    // ----------------------------------------------------------------------------------------------------
    static public int SessionSave = 0;
    static public string LastSaveName = "";
    static public void SaveToFile_Progressive(DataBlock data, string dirName, DefLibrary defLib)
    {
        if (SessionSave == 0)
        {
            string content = "";
            content += Data.SaveData(data, 0, defLib);

            string fileName = dirName + "Save.sav";
            using var file = FileAccess.Open("res:///" + fileName, FileAccess.ModeFlags.Write);
            LastSaveName = fileName;
            file.StoreString(content);
            file.Close();

            SessionSave++;
        }
        else
        {
            string content = "";
            content += Data.SaveData(data, 0, defLib);

            string fileName = dirName + "Save_" + SessionSave.ToString() + ".sav";
            using var file = FileAccess.Open("res:///" + fileName, FileAccess.ModeFlags.Write);
            file.StoreString(content);
            file.Close();

            SessionSave++;

            DataBlock lastSave = Data.LoadFile(LastSaveName, defLib);
            DataBlock newSave = Data.LoadFile(fileName, defLib);
            DeleteData(newSave, lastSave);
            LastSaveName = fileName;

            content = "";
            content += Data.SaveData(newSave, 0, defLib);

            string diffFileName = dirName + "Save_" + SessionSave.ToString() + "_diff.sav";
            using var fileDiff = FileAccess.Open("res:///" + diffFileName, FileAccess.ModeFlags.Write);
            fileDiff.StoreString(content);
            fileDiff.Close();
        }
    }

    // ----------------------------------------------------------------------------------------------------
    static public void SaveToFile(DataBlock data, string fileName, DefLibrary defLib)
    {
        string content = "";

        // GameStats
        content += Data.SaveData(data, 0, defLib);

        using var file = FileAccess.Open("res:///" + fileName, FileAccess.ModeFlags.Write);
        file.StoreString(content);
        file.Close();
    }

    static public string SaveData(DataBlock dataBlock, int currentTabs, DefLibrary df)
    {
        string text = "";

        if (currentTabs > 25)
        {
            GD.PrintErr("Save over 25 deep - possible logic loop!");
            return "";
        }

        string name = df.GetDBValue(dataBlock.Type);
        if (name == "")
        {
            name = dataBlock.Name;
        }
        if (name.StartsWith("_")) name = name.Substring(1);
        else name = name.Substring(2); // for i_ and s_
        text += Helper.Tabs(currentTabs) + name + " " + dataBlock.ValueToString() + "\n";

        if (text.Contains("ActionColonyBuild"))
        {
            GD.Print("HIT");
        }

        if (dataBlock.Subs.Count > 0)
        {
            text += Helper.Tabs(currentTabs) + "{" + "\n";
            for (int subIdx = 0; subIdx < dataBlock.Subs.Count; subIdx++)
            {
                text += SaveData(dataBlock.Subs[subIdx], currentTabs + 1, df);
            }
            text += Helper.Tabs(currentTabs) + "}" + "\n";
        }
        return text;
    }

    // ----------------------------------------------------------------------------------------------------
    static public DataBlock LoadCSV(string fileName, DefLibrary defLib)
    {
        using var file = FileAccess.Open("res:///" + fileName, FileAccess.ModeFlags.Read);
        string content = file.GetAsText();

        content = content.Replace("\r", "");
        content = content.Replace("\t", "");
        content = content.Replace(" ", "");

        char[] delimiters = { '\n' };
        string[] rows = content.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

        if (rows.Length < 4) return null;

        string[] tableHeadRow_1 = rows[0].Split(',');
        string[] tableHeadRow_2 = rows[1].Split(',');
        string[] tableHeadRow_3 = rows[2].Split(',');
        if (tableHeadRow_1[0] == "") return null;

        DataBlock data = CreateData(tableHeadRow_1[0] + "s", defLib);

        List<string> words = new List<string>();
        for (int idx = 3; idx < rows.Length; idx++)
        {
            string[] wordsOnRow = rows[idx].Split(',');
            if (wordsOnRow[0] == "") continue;

            DataBlock item = AddData(data, tableHeadRow_1[0], wordsOnRow[0], defLib);

            for (int colIdx = 1; colIdx < wordsOnRow.Length; colIdx++)
            {
                if (wordsOnRow[colIdx] == "") continue;
                if (colIdx < tableHeadRow_1.Length && colIdx < tableHeadRow_2.Length && colIdx < tableHeadRow_3.Length)
                {
                    if (tableHeadRow_3[colIdx] != "" && tableHeadRow_2[colIdx] != "" && tableHeadRow_1[colIdx] != "")
                    {
                        DataBlock level1 = item.GetSub(tableHeadRow_1[colIdx], false);
                        if (level1 == null) level1 = AddData(item, tableHeadRow_1[colIdx], defLib);

                        DataBlock level2 = level1.GetSub(tableHeadRow_2[colIdx], false);
                        if (level2 == null) level2 = AddData(level1, tableHeadRow_2[colIdx], defLib);

                        if (System.Char.IsLetter(wordsOnRow[colIdx][0])) AddData(level2, tableHeadRow_3[colIdx], wordsOnRow[colIdx], defLib);
                        else AddData(level2, tableHeadRow_3[colIdx], wordsOnRow[colIdx].ToInt(), defLib);

                    }
                    else if (tableHeadRow_2[colIdx] != "" && tableHeadRow_1[colIdx] != "")
                    {
                        DataBlock level1 = item.GetSub(tableHeadRow_1[colIdx], false);
                        if (level1 == null) level1 = AddData(item, tableHeadRow_1[colIdx], defLib);

                        if (System.Char.IsLetter(wordsOnRow[colIdx][0])) AddData(level1, tableHeadRow_2[colIdx], wordsOnRow[colIdx], defLib);
                        else AddData(level1, tableHeadRow_2[colIdx], wordsOnRow[colIdx].ToInt(), defLib);
                    }
                    else
                    {
                        if (System.Char.IsLetter(wordsOnRow[colIdx][0])) AddData(item, tableHeadRow_1[colIdx], wordsOnRow[colIdx], defLib);
                        else AddData(item, tableHeadRow_1[colIdx], wordsOnRow[colIdx].ToInt(), defLib);
                    }
                }
            }
        }

        return data;
    }
}
