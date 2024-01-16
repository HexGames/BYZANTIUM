using Godot;
using Godot.Collections;
using System;

public partial class UISystemDef : Resource
{
    [Export]
    public Array<UISystemPlanetPropertiesDef> DataBlockDefs = new Array<UISystemPlanetPropertiesDef>();

    public void LoadMap()
    {
        DataBlockDefs.Clear();

        //using var file = FileAccess.Open("res:///Saves/" + SaveName + ".sav", FileAccess.ModeFlags.Read);
        //string content = file.GetAsText();
        //
        //char[] delimiters = { '\n', '\r' ,'\t' };
        //string[] rows = content.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        //int rowIdx = -1;
        //
        //List<string> words = new List<string>();
        //string[] wordsOnRow = GetWordsFromNextValidRow(rows, ref rowIdx);
        //while (wordsOnRow != null)
        //{
        //    if (words.Count > 0 && words[words.Count - 1] != "{" && words[words.Count - 1] != "}" && wordsOnRow[0] != "{")
        //    {
        //        words.Add("{");
        //        words.Add("}");
        //    }
        //    words.AddRange(wordsOnRow);
        //    wordsOnRow = GetWordsFromNextValidRow(rows, ref rowIdx);
        //}
        //
        //int wordIdx = 0;
        //
        //wordIdx++; // "Systems"
        //wordIdx++; // {
        //
        //while (words[wordIdx] != "}")
        //{
        //    string firstWord = words[wordIdx];
        //    wordIdx++;
        //    string secondWord = "";
        //    if (words[wordIdx] != "{" && words[wordIdx] != "}")
        //    {
        //        secondWord = words[wordIdx];
        //        wordIdx++;
        //    }
        //    if (firstWord == "Location" && words[wordIdx] == "{")
        //    {
        //        wordIdx++; // {
        //
        //        LocationData locData = new LocationData();
        //        locData.Name = secondWord;
        //
        //        wordIdx++; // X
        //        locData.X = words[wordIdx].ToInt();
        //        wordIdx++;
        //        wordIdx++; // {
        //        wordIdx++; // }
        //        wordIdx++; // Y
        //        locData.Y = words[wordIdx].ToInt();
        //        wordIdx++;
        //        wordIdx++; // {
        //        wordIdx++; // }
        //
        //        int dataBlockStart = wordIdx;
        //        int dataBlockEnd = wordIdx + 1;
        //        int depth = 1;
        //        while (depth > 0)
        //        {
        //            dataBlockEnd++;
        //            if (words[dataBlockEnd] == "{") depth++;
        //            if (words[dataBlockEnd] == "}") depth--;
        //        }
        //        locData.System = Data.LoadData(words.GetRange(dataBlockStart, dataBlockEnd - dataBlockStart), DefLibrary);
        //        wordIdx = dataBlockEnd + 1;
        //
        //        Systems.Add(locData);
        //
        //        MapGenerator.CreateLocationNode(locData.X, locData.Y, locData);
        //    }
        //}
    }
}