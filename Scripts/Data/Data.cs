﻿using Godot;
using Godot.Collections;
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

        data.Type = df.GetDBType(name, BaseType.NONE);
        data.Name = name;

        return data;
    }

    static public DataBlock CreateData(string name, int value, DefLibrary df)
    {
        DataBlock data = new DataBlock();

        data.Type = df.GetDBType(name, BaseType.NONE);
        data.Name = name;
        data.ValueI = value;

        return data;
    }

    static public DataBlock CreateData(string name, string value, DefLibrary df)
    {
        DataBlock data = new DataBlock();

        data.Type = df.GetDBType(name, BaseType.STRING);
        data.Name = name;
        data.ValueS = value;

        return data;
    }

    static public DataBlock AddData(DataBlock parent, string name, DefLibrary df)
    {
        DataBlock data = new DataBlock();

        data.Type = df.GetDBType(name, BaseType.NONE);
        data.Name = name;

        parent.Subs.Add(data);
        //data.Parent = parent;

        return data;
    }

    static public DataBlock AddData(DataBlock parent, string name, int value, DefLibrary df)
    {
        DataBlock data = new DataBlock();

        data.Type = df.GetDBType(name, BaseType.INT);
        data.Name = name;
        data.ValueI = value;

        parent.Subs.Add(data);
        //data.Parent = parent;

        return data;
    }

    static public DataBlock AddData(DataBlock parent, string name, string value, DefLibrary df)
    {
        DataBlock data = new DataBlock();

        data.Type = df.GetDBType(name, BaseType.STRING);
        data.Name = name;
        data.ValueS = value;

        parent.Subs.Add(data);
        //data.Parent = parent;

        return data;
    }

    static public void RemoveData(DataBlock parent, string name, DefLibrary df)
    {
        int type = df.GetDBType(name, BaseType.STRING);

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
        int type = df.GetDBType(name, BaseType.STRING);

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
        int type = df.GetDBType(name, BaseType.STRING);

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

    static public DataBlock LoadData(List<string> words, DefLibrary df)
    {
        DataBlock data = new DataBlock();

        BaseType baseType = BaseType.INT;
        if (words[1] == "{") baseType = BaseType.NONE;
        else if (System.Char.IsLetter(words[1][0])) baseType = BaseType.STRING;
        else if (words[1].Contains(".")) baseType = BaseType.FLOAT;

        data.Type = df.GetDBType(words[0], baseType);
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

    static public string SaveData(DataBlock dataBlock, int currentTabs, DefLibrary df)
    {
        string text = "";

        if (currentTabs > 25)
        {
            GD.PrintErr("Save over 25 deep - possible logic loop!");
            return "";
        }

        text += Helper.Tabs(currentTabs) + df.GetDBValue(dataBlock.Type) + " " + dataBlock.ValueToString() + "\n";

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
}
