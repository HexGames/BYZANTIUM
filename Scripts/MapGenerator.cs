using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

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
    public bool ClearMap
    {
        get => false;
        set
        {
            if (value)
            {
                ClearMapFunc();
            }
        }
    }
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

    public void ClearMapFunc()
    {
        Map.Data.Clear();
    }

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
        mapData.Type = DefLibrary.GetDBType("_Map", Data.BaseType.NONE);

        GenerateNewMapSave_GameStats(mapData);

        GenerateNewMapSave_Star(mapData);

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
    public void GenerateNewMapSave_Star(DataBlock map)
    {
        LoadMapFile();

        Data.AddData(map, "GalaxySize", FromFile_Size, DefLibrary);

        DataBlock starList = Data.AddData(map, "Star_List", DefLibrary);

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

                string name = "Star_" + idx.ToString();
                if (idx == 22) name = "Sol";

                DataBlock starData = Data.AddData(starList, "Star", name, DefLibrary);

                Data.AddData(starData, "ID", starList.GetSubs().Count, DefLibrary);

                Data.AddData(starData, "GFX_RNG_1", RNG.RandiRange(0, 100), DefLibrary);
                Data.AddData(starData, "GFX_RNG_2", RNG.RandiRange(0, 100), DefLibrary);

                Data.AddData(starData, "X", x, DefLibrary);
                Data.AddData(starData, "Y", y, DefLibrary);

                GenerateNewMapSave_Star_Planets(starData);
            }
        }

        // no paths
        return;

        // make all pathways
        Array<DataBlock> stars = starList.GetSubs("System");
        for (int fromIdx = 0; fromIdx < stars.Count; fromIdx++)
        {
            for (int toIdx = fromIdx+1; toIdx < stars.Count; toIdx++)
            {
                int from_x = stars[fromIdx].GetSub("X").ValueI;
                int from_y = stars[fromIdx].GetSub("Y").ValueI;
                int to_x = stars[toIdx].GetSub("X").ValueI;
                int to_y = stars[toIdx].GetSub("Y").ValueI;

                if ((from_x == to_x && Mathf.Abs(from_y - to_y) <= 1)
                    || (from_y == to_y && Mathf.Abs(from_x - to_x) <= 1)
                    || (from_x == to_x - 1 && from_y == to_y - 1)
                    || (from_x - 1 == to_x && from_y - 1 == to_y))
                {
                    Data.AddData(stars[fromIdx], "PathTo", stars[toIdx].GetSub("ID").ValueI, DefLibrary);
                    Data.AddData(stars[toIdx], "PathTo", stars[fromIdx].GetSub("ID").ValueI, DefLibrary);
                }
            }
        }

        // delete some pathways
        List<Vector2> protectedPaths = new List<Vector2>();
        for (int fromIdx = 0; fromIdx < stars.Count; fromIdx++)
        {
            Array<DataBlock> toStars = GetStarsWithDirectPath(stars, stars[fromIdx]);
            for (int toIdx = 0; toIdx < toStars.Count; toIdx++)
            {
                if (RNG.RandiRange(0, 99) < 50 && protectedPaths.Contains(new Vector2(stars[fromIdx].GetSub("ID").ValueI, toStars[toIdx].GetSub("ID").ValueI)) == false)
                {
                    for (int otherIdx = 0; otherIdx < toStars.Count; otherIdx++)
                    {
                        if (toIdx != otherIdx)
                        {
                            Array<DataBlock> thirdStars = GetStarsWithDirectPath(stars, toStars[otherIdx]);
                            for (int thirdIdx = 0; thirdIdx < thirdStars.Count; thirdIdx++)
                            {
                                if (thirdStars[thirdIdx] == toStars[toIdx])
                                {
                                    Data.RemoveData(stars[fromIdx], "PathTo", toStars[toIdx].GetSub("ID").ValueI, DefLibrary);
                                    Data.RemoveData(toStars[toIdx], "PathTo", stars[fromIdx].GetSub("ID").ValueI, DefLibrary);
                                    protectedPaths.Add(new Vector2(stars[fromIdx].GetSub("ID").ValueI, toStars[otherIdx].GetSub("ID").ValueI));
                                    protectedPaths.Add(new Vector2(toStars[otherIdx].GetSub("ID").ValueI, stars[fromIdx].GetSub("ID").ValueI));
                                    protectedPaths.Add(new Vector2(toStars[otherIdx].GetSub("ID").ValueI, toStars[toIdx].GetSub("ID").ValueI));
                                    protectedPaths.Add(new Vector2(toStars[toIdx].GetSub("ID").ValueI, toStars[otherIdx].GetSub("ID").ValueI));
                                    // toStars does not get updated, but it does not matter
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private static Array<DataBlock> GetStarsWithDirectPath(Array<DataBlock> stars, DataBlock fromStar)
    {
        Array<DataBlock> toStars = new Array<DataBlock>();
        Array<DataBlock> paths = fromStar.GetSubs("PathTo");
        for (int pathIdx = 0; pathIdx < paths.Count; pathIdx++)
        {
            for (int toIdx = 0; toIdx < stars.Count; toIdx++)
            {
                if (paths[pathIdx].ValueI == stars[toIdx].GetSub("ID").ValueI)
                {
                    toStars.Add(stars[toIdx]);
                }
            }
        }

        return toStars;
    }

    // --------------------------------------------------------------------------------------------------
    public void GenerateNewMapSave_Star_Planets(DataBlock star)
    {
        DataBlock planetList = Data.AddData(star, "Planet_List", DefLibrary);
        if (star.ValueS == "Sol")
        {
            GenerateNewMapSave_Stars_Planets_Sol(planetList);
        }
        else
        {
            GenerateNewMapSave_Stars_Planets_Random(planetList);
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
            DataBlock startingStar;
            GenerateNewMapSave_Players_GetStartingPlanet(map, startingPlanetCustom, startingPlanetType, out startingPlanet, out startingStar);
            if (startingPlanet == null || startingStar == null) continue;

            DataBlock playerData = Data.AddData(playerList, "Player", "Player_" + n.ToString(), DefLibrary); 
            if (human) Data.AddData(playerData, "Human", DefLibrary);

            GenerateNewMapSave_Players_Resources(playerData);
            GenerateNewMapSave_Players_Status(playerData);
            GenerateNewMapSave_Players_Civics(playerData);
            GenerateNewMapSave_Players_Bonuses(playerData);

            GenerateNewMapSave_Players_Ship_Designs(playerData);

            //GenerateNewMapSave_Players_StartingStar();
            GenerateNewMapSave_Players_StartingColony(playerData, startingStar, startingPlanet);
            //GenerateNewMapSave_Players_StartingStaton(playerData, startingStar);

            GenerateNewMapSave_Players_StartingShip(playerData, startingStar);
        }
    }

    private void GenerateNewMapSave_Players_GetStartingPlanet(DataBlock mapData, string customName, string type, out DataBlock planet, out DataBlock star)
    {
        DataBlock starList = mapData.GetSub("Star_List");
        Array<DataBlock> stars = starList.GetSubs("Star");

        for (int starIdx = 0; starIdx < stars.Count; starIdx++)
        {
            DataBlock planetList = stars[starIdx].GetSub("Planet_List");
            Array<DataBlock> planets = planetList.GetSubs("Planet");
            for (int planetIdx = 0; planetIdx < planets.Count; planetIdx++)
            {
                DataBlock player = planets[planetIdx].GetLink("Link:Player");
                if (planets[planetIdx].ValueS == customName && player == null)
                {
                    star = stars[starIdx];
                    planet = planets[planetIdx];
                    return;
                }
            }
        }

        for (int starIdx = 0; starIdx < stars.Count; starIdx++)
        {
            DataBlock planetList = stars[starIdx].GetSub("Planet_List");
            Array<DataBlock> planets = planetList.GetSubs("Planet");
            for (int planetIdx = 0; planetIdx < planets.Count; planetIdx++)
            {
                DataBlock planetType = planets[planetIdx].GetSub("Type");
                DataBlock player = planets[planetIdx].GetLink("Link:Player");
                if (planetType != null && planetType.ValueS == type && player == null)
                {
                    star = stars[starIdx];
                    planet = planets[planetIdx];
                    return;
                }
            }
        }

        star = null;
        planet = null;
    }
    // --------------------------------------------------------------------------------------------------
    private void GenerateNewMapSave_Players_StartingColony(DataBlock playerData, DataBlock startingStar, DataBlock startingPlanet)
    {
        DataBlock sectorList = Data.AddData(playerData, "Sector_List", DefLibrary);

        DataBlock sector = Data.AddData(sectorList, "Sector", "Core", DefLibrary);
        GenerateNewMapSave_Players_StartingColony_SectorResources(sector);
        GenerateNewMapSave_Players_StartingColony_SectorConTreasury(sector);
        //GenerateNewMapSave_Players_StartingColony_SectorBudget(sector);

        DataBlock systemList = Data.AddData(sector, "System_List", DefLibrary);

        DataBlock system = Data.AddData(systemList, "System", "Sol", DefLibrary);
        GenerateNewMapSave_Players_StartingColony_SystemResources(system);

        Data.AddData(system, "Link:Star", startingStar.ValueS, DefLibrary);
        Data.AddData(startingStar, "Link:Player:Sector:System", playerData.ValueS + ":" + sector.ValueS + ":" + system.ValueS, DefLibrary);

        DataBlock colonyList = Data.AddData(system, "Colony_List", DefLibrary);

        DataBlock colony = Data.AddData(colonyList, "Colony", startingPlanet.ValueS, DefLibrary);

        Data.AddData(colony, "Capital", DefLibrary);
        GenerateNewMapSave_Players_StartingColony_Resources(colony);
        GenerateNewMapSave_Players_StartingColony_Buildings(colony);
        GenerateNewMapSave_Players_StartingColony_Support(colony);
        GenerateNewMapSave_Players_StartingColony_ConBuildings(colony, system);
        GenerateNewMapSave_Players_StartingColony_ConColony(colony, system);
        GenerateNewMapSave_Players_StartingColony_ConShipyard(colony, system);
        //GenerateNewMapSave_Players_StartingColony_ConTreasury(colony, system);
        //GenerateNewMapSave_Players_StartingColony_Bonuses(colonyData);

        Data.AddData(colony, "Link:Star:Planet", startingStar.ValueS + ":" + startingPlanet.ValueS, DefLibrary);
        Data.AddData(startingPlanet, "Link:Player:Sector:System:Colony", playerData.ValueS + ":" + sector.ValueS + ":" + system.ValueS + ":" + colony.ValueS, DefLibrary);

        // actions
        //GenerateNewMapSave_Players_StartingColony_SectorCampaign(sector, system, colony);
        //GenerateNewMapSave_Players_StartingColony_SectorConstruction(sector, system, colony);
    }

    //private void GenerateNewMapSave_Players_StartingStaton(DataBlock playerData, DataBlock startingStar)
    //{
    //    DataBlock colonyList = playerData.GetSub("Colony_List");
    //    DataBlock star = GenerateNewMapSave_Players_StartingStaton_GetStar(startingStar);
    //
    //    DataBlock colonyData = Data.AddData(colonyList, "Colony", startingStar.ValueS + "_Station", DefLibrary);
    //
    //    GenerateNewMapSave_Players_StartingStation_Resources(colonyData);
    //    GenerateNewMapSave_Players_StartingStation_Buildings(colonyData);
    //    GenerateNewMapSave_Players_StartingStation_Support(colonyData);
    //    GenerateNewMapSave_Players_StartingStation_Bonuses(colonyData);
    //
    //    Data.AddData(colonyData, "Link:Star:Planet", startingStar.ValueS + ":" + star.ValueS, DefLibrary);
    //    Data.AddData(star, "Link:Player:Colony", playerData.ValueS + ":" + colonyData.ValueS, DefLibrary);
    //}

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
