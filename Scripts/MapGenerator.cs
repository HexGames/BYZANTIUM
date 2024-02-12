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

        Map.Data.GenerateGameFromData(DefLibrary);

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

                Data.AddData(systemData, "ID", systemList.GetSubs().Count, DefLibrary);
                Data.AddData(systemData, "X", x, DefLibrary);
                Data.AddData(systemData, "Y", y, DefLibrary);

                GenerateNewMapSave_Systems_Planets(systemData);
            }
        }

        // make all pathways
        Array<DataBlock> systems = systemList.GetSubs("System");
        for (int fromIdx = 0; fromIdx < systems.Count; fromIdx++)
        {
            for (int toIdx = fromIdx+1; toIdx < systems.Count; toIdx++)
            {
                int from_x = systems[fromIdx].GetSub("X").ValueI;
                int from_y = systems[fromIdx].GetSub("Y").ValueI;
                int to_x = systems[toIdx].GetSub("X").ValueI;
                int to_y = systems[toIdx].GetSub("Y").ValueI;

                if ((from_x == to_x && Mathf.Abs(from_y - to_y) <= 1)
                    || (from_y == to_y && Mathf.Abs(from_x - to_x) <= 1)
                    || (from_x == to_x - 1 && from_y == to_y - 1)
                    || (from_x - 1 == to_x && from_y - 1 == to_y))
                {
                    Data.AddData(systems[fromIdx], "PathTo", systems[toIdx].GetSub("ID").ValueI, DefLibrary);
                    Data.AddData(systems[toIdx], "PathTo", systems[fromIdx].GetSub("ID").ValueI, DefLibrary);
                }
            }
        }

        // delete some pathways
        List<Vector2> protectedPaths = new List<Vector2>();
        for (int fromIdx = 0; fromIdx < systems.Count; fromIdx++)
        {
            Array<DataBlock> toSystems = GetSystemsWithDirectPath(systems, systems[fromIdx]);
            for (int toIdx = 0; toIdx < toSystems.Count; toIdx++)
            {
                if (RNG.RandiRange(0, 99) < 50 && protectedPaths.Contains(new Vector2(systems[fromIdx].GetSub("ID").ValueI, toSystems[toIdx].GetSub("ID").ValueI)) == false)
                {
                    for (int otherIdx = 0; otherIdx < toSystems.Count; otherIdx++)
                    {
                        if (toIdx != otherIdx)
                        {
                            Array<DataBlock> thirdSystems = GetSystemsWithDirectPath(systems, toSystems[otherIdx]);
                            for (int thirdIdx = 0; thirdIdx < thirdSystems.Count; thirdIdx++)
                            {
                                if (thirdSystems[thirdIdx] == toSystems[toIdx])
                                {
                                    Data.RemoveData(systems[fromIdx], "PathTo", toSystems[toIdx].GetSub("ID").ValueI, DefLibrary);
                                    Data.RemoveData(toSystems[toIdx], "PathTo", systems[fromIdx].GetSub("ID").ValueI, DefLibrary);
                                    protectedPaths.Add(new Vector2(systems[fromIdx].GetSub("ID").ValueI, toSystems[otherIdx].GetSub("ID").ValueI));
                                    protectedPaths.Add(new Vector2(toSystems[otherIdx].GetSub("ID").ValueI, systems[fromIdx].GetSub("ID").ValueI));
                                    protectedPaths.Add(new Vector2(toSystems[otherIdx].GetSub("ID").ValueI, toSystems[toIdx].GetSub("ID").ValueI));
                                    protectedPaths.Add(new Vector2(toSystems[toIdx].GetSub("ID").ValueI, toSystems[otherIdx].GetSub("ID").ValueI));
                                    // toSystems does not get updated, but it does not matter
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private static Array<DataBlock> GetSystemsWithDirectPath(Array<DataBlock> systems, DataBlock fromSystem)
    {
        Array<DataBlock> toSystems = new Array<DataBlock>();
        Array<DataBlock> paths = fromSystem.GetSubs("PathTo");
        for (int pathIdx = 0; pathIdx < paths.Count; pathIdx++)
        {
            for (int toIdx = 0; toIdx < systems.Count; toIdx++)
            {
                if (paths[pathIdx].ValueI == systems[toIdx].GetSub("ID").ValueI)
                {
                    toSystems.Add(systems[toIdx]);
                }
            }
        }

        return toSystems;
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

            GenerateNewMapSave_Players_Ship_Designs(playerData);

            GenerateNewMapSave_Players_StartingColony(playerData, startingSystem, startingPlanet);
            GenerateNewMapSave_Players_StartingStaton(playerData, startingSystem);

            GenerateNewMapSave_Players_StartingShip(playerData, startingSystem);
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

    private void GenerateNewMapSave_Players_StartingStaton(DataBlock playerData, DataBlock startingSystem)
    {
        DataBlock colonyList = playerData.GetSub("Colony_List");
        DataBlock star = GenerateNewMapSave_Players_StartingStaton_GetStar(startingSystem);

        DataBlock colonyData = Data.AddData(colonyList, "Colony", startingSystem.ValueS + "_Station", DefLibrary);

        GenerateNewMapSave_Players_StartingStation_Resources(colonyData);
        GenerateNewMapSave_Players_StartingStation_Buildings(colonyData);
        GenerateNewMapSave_Players_StartingStation_Support(colonyData);
        GenerateNewMapSave_Players_StartingStation_Bonuses(colonyData);

        Data.AddData(colonyData, "Link:System:Planet", startingSystem.ValueS + ":" + star.ValueS, DefLibrary);
        Data.AddData(star, "Link:Player:Colony", playerData.ValueS + ":" + colonyData.ValueS, DefLibrary);
    }

    private DataBlock GenerateNewMapSave_Players_StartingStaton_GetStar(DataBlock startingSystem)
    {
        Array<DataBlock> planets = startingSystem.GetSub("Planet_List").GetSubs("Planet");
        for (int idx = 0; idx < planets.Count; idx++)
        {
            if (planets[idx].GetSub("Star_Type") != null)
            {
                return planets[idx];
            }
        }
        return null;
    }

    // --------------------------------------------------------------------------------------------------
    private void LoadMapFile()
    {
        using var file = FileAccess.Open("res:///Map/" + MapTypeFile + ".map", FileAccess.ModeFlags.Read);
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
