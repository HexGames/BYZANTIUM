using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

// Editor
[Tool]
public partial class MapNode : Node
{
    [Export]
    public DefLibrary DefLibrary;
    [Export]
    public MapGenerator MapGenerator;
    [Export]
    public Array<LocationData> Systems = new Array<LocationData>();
    [Export]
    public Array<PawnData> Fleets = new Array<PawnData>();

    [Export]
    public string SaveName = "Save";

    [Export]
    public bool LoadMapTrigger
    {
        get => false;
        set
        {
            if (value)
            {
                LoadMap();
            }
        }
    }

    [Export]
    public bool SaveMapTrigger
    {
        get => false;
        set
        {
            if (value)
            {
                SaveMap();
            }
        }
    }

    public void ClearMap()
    {
        Systems.Clear();
        Fleets.Clear();
    }

    public void LoadMap()
    {
        MapGenerator.ClearMap();
        ClearMap();

        using var file = FileAccess.Open("res:///Saves/" + SaveName + ".sav", FileAccess.ModeFlags.Read);
        string content = file.GetAsText();

        char[] delimiters = { '\n', '\r' ,'\t' };
        string[] rows = content.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        int rowIdx = -1;

        List<string> words = new List<string>();
        string[] wordsOnRow = GetWordsFromNextValidRow(rows, ref rowIdx);
        while (wordsOnRow != null)
        {
            if (words.Count > 0 && words[words.Count - 1] != "{" && words[words.Count - 1] != "}" && wordsOnRow[0] != "{")
            {
                words.Add("{");
                words.Add("}");
            }
            words.AddRange(wordsOnRow);
            wordsOnRow = GetWordsFromNextValidRow(rows, ref rowIdx);
        }

        int wordIdx = 0;

        wordIdx++; // "Systems"
        wordIdx++; // {

        while (words[wordIdx] != "}")
        {
            string firstWord = words[wordIdx];
            wordIdx++;
            string secondWord = "";
            if (words[wordIdx] != "{" && words[wordIdx] != "}")
            {
                secondWord = words[wordIdx];
                wordIdx++;
            }
            if (firstWord == "Location" && words[wordIdx] == "{")
            {
                wordIdx++; // {

                LocationData locData = new LocationData();
                locData.Name = secondWord;

                wordIdx++; // X
                locData.X = words[wordIdx].ToInt();
                wordIdx++;
                wordIdx++; // {
                wordIdx++; // }
                wordIdx++; // Y
                locData.Y = words[wordIdx].ToInt();
                wordIdx++;
                wordIdx++; // {
                wordIdx++; // }

                int dataBlockStart = wordIdx;
                int dataBlockEnd = wordIdx + 1;
                int depth = 1;
                while (depth > 0)
                {
                    dataBlockEnd++;
                    if (words[dataBlockEnd] == "{") depth++;
                    if (words[dataBlockEnd] == "}") depth--;
                }
                locData.System = Data.LoadData(words.GetRange(dataBlockStart, dataBlockEnd - dataBlockStart), DefLibrary);
                wordIdx = dataBlockEnd + 1;

                Systems.Add(locData);

                MapGenerator.CreateLocationNode(locData.X, locData.Y, locData);
            }
        }
    }

    public void SaveMap()
    {
        string content = "";
        int currentTabs = 0;

        content += "Systems" + "\n";
        content += "{" + "\n";
        currentTabs++;
        for (int systemIdx = 0; systemIdx < Systems.Count; systemIdx++)
        {
            content += Helper.Tabs(currentTabs) + "Location " + Systems[systemIdx].Name + "\n";
            content += Helper.Tabs(currentTabs) + "{" + "\n"; 
            content += Helper.Tabs(currentTabs + 1) + "X " + Systems[systemIdx].X + "\n";
            content += Helper.Tabs(currentTabs + 1) + "Y " + Systems[systemIdx].Y + "\n";
            content += Data.SaveData(Systems[systemIdx].System, currentTabs + 1, DefLibrary);
            content += Helper.Tabs(currentTabs) + "}" + "\n";

        }
        currentTabs--;
        content += "}" + "\n";

        using var file = FileAccess.Open("res:///Saves/" + SaveName + ".sav", FileAccess.ModeFlags.Write);
        file.StoreString(content);
    }

    string[] GetWordsFromNextValidRow(string[] rows, ref int rowIdx)
    {
        rowIdx++;
        if (rowIdx >= rows.Length)
        {
            // GD.PrintErr("Load map data error 01");
            return null;
        }
        string[] words = rows[rowIdx].Split("//")[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (words.Length == 0)
        {
            return GetWordsFromNextValidRow(rows, ref rowIdx);
        }
        return words;
    }
}
