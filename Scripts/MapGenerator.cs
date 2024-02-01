using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Transactions;

// Editor
[Tool]
public partial class MapGenerator : Node
{
    [Export]
    public DefLibrary DefLibrary;
    [Export]
    public MapNode Map;
    [Export]
    public string MapTypeFile = "";

    [Export]
    public bool GenerateMap
    {
        get => false;
        set
        {
            if (value)
            {
                GenerateMapFunc();
            }
        }
    }

    private RandomNumberGenerator RNG = new RandomNumberGenerator();

    private int FromFile_Size = 0;
    private char PaddingCharacter = ' ';
    private char EmptyCharacter = ' ';
    private int FromFile_SectorsCount = 0;
    private int FromFile_SectorGroupsCount = 0;
    private Array<string> FromFile_SectorGroups = new Array<string>();
    private Array<string> FromFile_Stars = new Array<string>();


    // Called when the node enters the scene tree for the first time.
    //public override void _Ready()
    //{
    //}

    public void GenerateMapFunc()
    {
        Map.Data._Data = GenerateNewMapSave();

        Map.Data.GenerateGameFromData();

        // set camera
        MapCamera Camera = GetTree().EditedSceneRoot.GetNode<MapCamera>("Camera3D");
        Camera.MoveLimitX = 8.6666f * 2.0f * 2 * FromFile_Size;
        Camera.MoveLimitY = 15.0f * 2 * FromFile_Size;
    }

    // --------------------------------------------------------------------------------------------------
    public DataBlock GenerateNewMapSave()
    {
        DataBlock mapData = new DataBlock();
        mapData.Type = DefLibrary.GetDBType("Map", Data.BaseType.NONE);

        GenerateNewMapSave_GameStats(mapData);

        GenerateNewMapSave_Systems(mapData);

        GenerateNewMapSave_Players(mapData);

        return mapData;
    }

    // --------------------------------------------------------------------------------------------------
    public void GenerateNewMapSave_GameStats(DataBlock map)
    {
        DataBlock gameStats = Data.AddData(map, "GameStats", DefLibrary);

        Data.AddData(gameStats, "Turn", 0, DefLibrary);
        Data.AddData(gameStats, "Human", 0, DefLibrary);
    }

    // --------------------------------------------------------------------------------------------------
    public void GenerateNewMapSave_Systems(DataBlock map)
    {
        LoadMapFile();

        Data.AddData(map, "GalaxySize", FromFile_Size, DefLibrary);

        DataBlock systemList = Data.AddData(map, "System_List", DefLibrary);

        for (int idx = 0; idx < FromFile_Stars.Count; idx++)
        {
            if (FromFile_Stars[idx].Length < 0)
            {
                GD.PrintErr("Map gen error 02");
                return;
            }
            if (FromFile_Stars[idx][0] != PaddingCharacter && FromFile_Stars[idx][0] != EmptyCharacter)
            {
                int x = idx % (1 + 2 * FromFile_Size);
                int y = idx / (1 + 2 * FromFile_Size);

                string name = "System_" + idx.ToString();
                if (idx == 22) name = "Sol";

                DataBlock systemData = Data.AddData(systemList, "System", name, DefLibrary);

                Data.AddData(systemData, "X", x, DefLibrary);
                Data.AddData(systemData, "Y", y, DefLibrary);

                GenerateNewMapSave_Systems_Planets(systemData);
            }
        }
    }
    // --------------------------------------------------------------------------------------------------
    public void GenerateNewMapSave_Systems_Planets(DataBlock system)
    {
        DataBlock planetList = Data.AddData(system, "Planet_List", DefLibrary);
        if (system.ValueS == "Sol")
        {
            GenerateNewMapSave_Systems_Planets_Sol(planetList);
        }
        else
        {
            GenerateNewMapSave_Systems_Planets_Random(planetList);
        }
    }

    // --------------------------------------------------------------------------------------------------
    public void GenerateNewMapSave_Players(DataBlock map)
    {
        DataBlock playerList = Data.AddData(map, "Player_List", DefLibrary);
        for (int n = 0; n < 10; n++)
        {
            bool human = false;
            string startingPlanetCustom = "";
            string startingPlanetType = "";
            if (n == 0)
            {
                startingPlanetCustom = "Terra";
                startingPlanetType = "Temperate";
                human = true;
            }
            else
            {
                startingPlanetType = "Temperate";
            }

            DataBlock startingPlanet;
            DataBlock startingSystem;
            GenerateNewMapSave_Players_GetStartingPlanet(map, startingPlanetCustom, startingPlanetType, out startingPlanet, out startingSystem);
            if (startingPlanet == null || startingSystem == null) continue;

            DataBlock playerData = Data.AddData(playerList, "Player", "Player_" + n.ToString(), DefLibrary); 
            if (human) Data.AddData(playerData, "Human", DefLibrary);

            GenerateNewMapSave_Players_Resources(playerData);
            GenerateNewMapSave_Players_Status(playerData);
            GenerateNewMapSave_Players_Civics(playerData);
            GenerateNewMapSave_Players_Bonuses(playerData);

            GenerateNewMapSave_Players_StartingColony(playerData, startingSystem, startingPlanet);
        }
    }

    private void GenerateNewMapSave_Players_GetStartingPlanet(DataBlock mapData, string customName, string type, out DataBlock planet, out DataBlock system)
    {
        DataBlock systemList = mapData.GetSub("System_List");
        Array<DataBlock> systems = systemList.GetSubs("System");

        for (int systemIdx = 0; systemIdx < systems.Count; systemIdx++)
        {
            DataBlock planetList = systems[systemIdx].GetSub("Planet_List");
            Array<DataBlock> planets = planetList.GetSubs("Planet");
            for (int planetIdx = 0; planetIdx < planets.Count; planetIdx++)
            {
                DataBlock player = planets[planetIdx].GetLink("Link:Player");
                if (planets[planetIdx].ValueS == customName && player == null)
                {
                    system = systems[systemIdx];
                    planet = planets[planetIdx];
                    return;
                }
            }
        }

        for (int systemIdx = 0; systemIdx < systems.Count; systemIdx++)
        {
            DataBlock planetList = systems[systemIdx].GetSub("Planet_List");
            Array<DataBlock> planets = planetList.GetSubs("Planet");
            for (int planetIdx = 0; planetIdx < planets.Count; planetIdx++)
            {
                DataBlock planetType = planets[planetIdx].GetSub("Type");
                DataBlock player = planets[planetIdx].GetLink("Link:Player");
                if (planetType != null && planetType.ValueS == type && player == null)
                {
                    system = systems[systemIdx];
                    planet = planets[planetIdx];
                    return;
                }
            }
        }

        system = null;
        planet = null;
    }
    // --------------------------------------------------------------------------------------------------
    private void GenerateNewMapSave_Players_StartingColony(DataBlock playerData, DataBlock startingSystem, DataBlock startingPlanet)
    {
        DataBlock colonyList = Data.AddData(playerData, "Colony_List", DefLibrary);

        DataBlock colonyData = Data.AddData(colonyList, "Colony", startingPlanet.ValueS, DefLibrary);

        GenerateNewMapSave_Players_StartingColony_Resources(colonyData);
        GenerateNewMapSave_Players_StartingColony_Buildings(colonyData);
        GenerateNewMapSave_Players_StartingColony_Support(colonyData);
        GenerateNewMapSave_Players_StartingColony_Bonuses(colonyData);

        Data.AddData(colonyData, "Link:System:Planet", startingSystem.ValueS + ":" + startingPlanet.ValueS, DefLibrary);
        Data.AddData(startingPlanet, "Link:Player:Colony", playerData.ValueS + ":" + colonyData.ValueS, DefLibrary);
    }

    // --------------------------------------------------------------------------------------------------
    private void LoadMapFile()
    {
        using var file = FileAccess.Open("res:///Mod/" + MapTypeFile + ".map", FileAccess.ModeFlags.Read);
        string content = file.GetAsText();
        char[] delimiters = { '\n', '\r' ,'\t' };
        string[] rows = content.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        int rowIdx = -1;

        string[] firstRow = GetWordsFromNextValidRow(rows, ref rowIdx);
        if (firstRow == null)
        {
            return;
        }
        if (firstRow.Length <= 4)
        {
            GD.PrintErr("Load map file error 02");
            return;
        }
        FromFile_Size = firstRow[0].ToInt();
        PaddingCharacter = firstRow[1][0];
        EmptyCharacter = firstRow[2][0];
        FromFile_SectorsCount = firstRow[3].ToInt();
        FromFile_SectorGroupsCount = firstRow[4].ToInt();
        if (firstRow.Length < 5 + FromFile_SectorGroupsCount)
        {
            GD.PrintErr("Load map file error 03");
            return;
        }
        FromFile_SectorGroups.Clear();
        for (int idx = 5; idx < 5 + FromFile_SectorGroupsCount; idx++)
        {
            FromFile_SectorGroups.Add(firstRow[idx]);
        }

        FromFile_Stars.Clear();
        for (int n = 0; n < 1 + 2 * FromFile_Size; n++)
        {
            string[] row = GetWordsFromNextValidRow(rows, ref rowIdx);
            if (row == null)
            {
                return;
            }
            if (row.Length < 1 + 2 * FromFile_Size)
            {
                GD.PrintErr("Load map file error 04");
                return;
            }
            for (int idx = 0; idx < 1 + 2 * FromFile_Size; idx++)
            {
                FromFile_Stars.Insert(idx, row[idx]);
            }
        }
    }

    private string[] GetWordsFromNextValidRow(string[] rows, ref int rowIdx)
    {
        rowIdx++;
        if (rowIdx >= rows.Length)
        {
            GD.PrintErr("Load map file error 01");
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
