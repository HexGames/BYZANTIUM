using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;

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

    private char Type = 'A';

    private int FromFile_Size = 0;
    private char PaddingCharacter = ' ';
    private char EmptyCharacter = ' ';
    private Array<string> FromFile_Stars = new Array<string>();

    private int FromFile_A_SectorsCount = 0;
    private int FromFile_A_SectorGroupsCount = 0;
    private Array<string> FromFile_A_SectorGroups = new Array<string>();

    private int FromFile_B_PlayersCount = 0;
    private DataBlock FromFile_B_PlayersData = null;


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
        MapCamera Camera = null;
        if (Engine.IsEditorHint())
        {
            Camera = GetTree().EditedSceneRoot.GetNode<MapCamera>("Camera3D");
        }
        else
        {
            Camera = GetNode<MapCamera>("/root/Main/Camera3D");
        }
        Camera.MoveLimitX = 8.6666f * 3.8f * FromFile_Size;
        Camera.MoveLimitY = 15.0f * 2.2f * FromFile_Size;
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
        GenerateNewMapSave_Star_LoadMapFile();

        if (Type == 'A')
        {
            GenerateNewMapSave_Star__Type_A(map);
        }
        else
        {
            GenerateNewMapSave_Star__Type_B(map);
        }

        // no paths
        return;

        // make all pathways
        /*Array<DataBlock> stars = starList.GetSubs("System");
        for (int fromIdx = 0; fromIdx < stars.Count; fromIdx++)
        {
            for (int toIdx = fromIdx + 1; toIdx < stars.Count; toIdx++)
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
        }*/
    }

    private void GenerateNewMapSave_Star_LoadMapFile()
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
        Type = firstRow[0][0];

        if (Type == 'A')
        {
            FromFile_Size = firstRow[1].ToInt();
            PaddingCharacter = firstRow[2][0];
            EmptyCharacter = firstRow[3][0];
            FromFile_A_SectorsCount = firstRow[4].ToInt();
            FromFile_A_SectorGroupsCount = firstRow[5].ToInt();
            if (firstRow.Length < 6 + FromFile_A_SectorGroupsCount)
            {
                GD.PrintErr("Load map file error 03");
                return;
            }
            FromFile_A_SectorGroups.Clear();
            for (int idx = 6; idx < 6 + FromFile_A_SectorGroupsCount; idx++)
            {
                FromFile_A_SectorGroups.Add(firstRow[idx]);
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

            FromFile_B_PlayersCount = 0;
        }
        else if (Type == 'B')
        {
            FromFile_Size = firstRow[1].ToInt();
            PaddingCharacter = firstRow[2][0];
            EmptyCharacter = firstRow[3][0];

            FromFile_B_PlayersCount = firstRow[4].ToInt();

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

            FromFile_A_SectorsCount = 0;
            FromFile_A_SectorGroupsCount = 0;
            FromFile_A_SectorGroups.Clear();

            FromFile_B_PlayersData = Data.LoadFile("Map/" + MapTypeFile + ".mod", DefLibrary);
        }
    }

    private void GenerateNewMapSave_Star__Type_A(DataBlock map)
    {
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
                int y = - (idx / (1 + 2 * FromFile_Size));

                string name = "Star_" + idx.ToString();
                if (idx == 22) name = "Sol";

                DataBlock starData = Data.AddData(starList, "Star", name, DefLibrary);

                Data.AddData(starData, "ID", starList.GetSubs().Count, DefLibrary);

                Data.AddData(starData, "GFX_RNG_1", RNG.RandiRange(50, 100), DefLibrary);
                Data.AddData(starData, "GFX_RNG_2", RNG.RandiRange(0, 100), DefLibrary);

                Data.AddData(starData, "X", x, DefLibrary);
                Data.AddData(starData, "Y", y, DefLibrary);

                GenerateNewMapSave_Star_Planets_A(starData);
            }
        }
    }

    private void GenerateNewMapSave_Star__Type_B(DataBlock map)
    {
        Data.AddData(map, "GalaxySize", FromFile_Size, DefLibrary);

        DataBlock starList = Data.AddData(map, "Star_List", DefLibrary);

        int starNr = 0;
        int playableID = 0;
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
                int y = - (idx / (1 + 2 * FromFile_Size));

                //string name = //"Star_" + idx.ToString();
                //if (idx == 22) name = "Sol";

                string name = DefLibrary.PlanetsNames[starNr];
                starNr++;

                DataBlock starData = Data.AddData(starList, "Star", name, DefLibrary);

                Data.AddData(starData, "ID", starList.GetSubs().Count, DefLibrary);

                Data.AddData(starData, "GFX_RNG_1", RNG.RandiRange(0, 100), DefLibrary);
                Data.AddData(starData, "GFX_RNG_2", RNG.RandiRange(0, 100), DefLibrary);

                Data.AddData(starData, "X", x, DefLibrary);
                Data.AddData(starData, "Y", y, DefLibrary);

                int level = 1;
                string capitalType = "";
                int temperature = RNG.RandiRange(2, 4);
                bool capital = false; 
                if (FromFile_Stars[idx][0] == 'x')
                {
                    level = FromFile_Stars[idx][1].ToString().ToInt();
                    Data.AddData(starData, "Init_Level", FromFile_Stars[idx][1], DefLibrary);
                }
                else
                {
                    string empireLetter = FromFile_Stars[idx][0].ToString();
                    string empireName = FromFile_B_PlayersData.GetSub("Fixed").GetSub(empireLetter).ValueS;
                    if (empireName == "Playable")
                    {
                        empireName = FromFile_B_PlayersData.GetSub("Playable").Subs[playableID].Name;
                        DataBlock empireInfo = DefLibrary.GetEmpire(empireName);
                        playableID++;
                        capitalType = empireInfo.GetSub("StartingPlanetType").ValueS;
                        temperature = empireInfo.GetSub("StartingTemperature").ValueI;
                    }
                    else
                    {
                        DataBlock empireInfo = DefLibrary.GetEmpire(empireName);
                        capitalType = empireInfo.GetSub("StartingPlanetType").ValueS;
                        temperature = empireInfo.GetSub("StartingTemperature").ValueI;
                    }

                    Data.AddData(starData, "Init_Player", empireName, DefLibrary);
                    int development = FromFile_Stars[idx][1].ToString().ToInt();
                    Data.AddData(starData, "Init_Development", development, DefLibrary);
                    level = FromFile_Stars[idx][2].ToString().ToInt();
                    Data.AddData(starData, "Init_Level", level, DefLibrary);
                    if (FromFile_Stars[idx].Length > 3 && FromFile_Stars[idx][3] == 'c')
                    {
                        capital = true;
                        Data.AddData(starData, "Init_Capital", DefLibrary);
                    }
                }


                GenerateNewMapSave_Star_Planets_B(starData, level, capital, capitalType, temperature);
            }
        }
    }

    /*private static Array<DataBlock> GetStarsWithDirectPath(Array<DataBlock> stars, DataBlock fromStar)
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
    }*/

    public void GenerateNewMapSave_Star_Planets_A(DataBlock star)
    {
        DataBlock planetList = Data.AddData(star, "Planet_List", DefLibrary);
        if (star.ValueS == "Sol")
        {
            GenerateNewMapSave_Stars_Planets_Sol(planetList);
        }
        else
        {
            //GenerateNewMapSave_Stars_Planets_Random(planetList);
        }
    }

    public void GenerateNewMapSave_Star_Planets_B(DataBlock star, int level, bool capital, string capitalType, int temperature)
    {
        DataBlock planetList = Data.AddData(star, "Planet_List", DefLibrary);
        GenerateNewMapSave_Stars_Planets_Level(star.ValueS, planetList, level, capital, capitalType, temperature);
    }

    // --------------------------------------------------------------------------------------------------
    public void GenerateNewMapSave_Players(DataBlock map)
    {
        DataBlock playerList = Data.AddData(map, "Player_List", DefLibrary);

        if (Type == 'A')
        {
            GenerateNewMapSave_Players_A(map, playerList);
        }
        else if (Type == 'B')
        {
            GenerateNewMapSave_Players_B(map, playerList);
        }
    }

    private void GenerateNewMapSave_Players_A(DataBlock map, DataBlock playerList)
    {
        // this has some bugs - only one players get's spawned... I think
        for (int n = 0; n < 10; n++)
        {
            bool human = false;
            if (n == 0) human = true;

            // empire info
            DataBlock empireInfo = DefLibrary.Empires[n];

            string startingStarName = empireInfo.GetSub("StartingStarName").ValueS;
            string startingPlanetType = empireInfo.GetSub("StartingPlanetType").ValueS;

            DataBlock startingPlanet;
            DataBlock startingStar;
            DataBlock startingStarPlanet;
            GenerateNewMapSave_Players_A_GetStartingPlanet(map/*, startingStarName*/, startingPlanetType, out startingPlanet, out startingStar, out startingStarPlanet);
            if (startingPlanet == null || startingStar == null) continue;

            startingStar.GetSub("Name").ValueS = startingStarName;

            DataBlock playerData = Data.AddData(playerList, "Player", "Player_" + n.ToString(), DefLibrary);
            if (human) Data.AddData(playerData, "Human", DefLibrary);


            GenerateNewMapSave_Players_Empire(playerData, empireInfo);
            GenerateNewMapSave_Players_Resources(playerData);
            GenerateNewMapSave_Players_Status(playerData);
            GenerateNewMapSave_Players_Civics(playerData);
            GenerateNewMapSave_Players_Bonuses(playerData);

            GenerateNewMapSave_Players_Ship_Designs(playerData);

            //GenerateNewMapSave_Players_StartingStar();
            //GenerateNewMapSave_Players_StartingSystem(playerData, empireInfo, startingStar);
            //GenerateNewMapSave_Players_StartingStaton(playerData, startingStar);

            //GenerateNewMapSave_Players_StartingShip(playerData, startingStar);
        }
    }

    private void GenerateNewMapSave_Players_A_GetStartingPlanet(DataBlock mapData/*, string customName*/, string type, out DataBlock planet, out DataBlock star, out DataBlock starPlanet)
    {
        DataBlock starList = mapData.GetSub("Star_List");
        Array<DataBlock> stars = starList.GetSubs("Star");

        /*for (int starIdx = 0; starIdx < stars.Count; starIdx++)
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
                    starPlanet = planets[0];
                    return;
                }
            }
        }*/

        for (int starIdx = 0; starIdx < stars.Count; starIdx++)
        {
            DataBlock planetList = stars[starIdx].GetSub("Planet_List");
            Array<DataBlock> planets = planetList.GetSubs("Planet");
            for (int planetIdx = 0; planetIdx < planets.Count; planetIdx++)
            {
                DataBlock planetType = planets[planetIdx].GetSub("Type", false);
                DataBlock player = planets[planetIdx].GetLink("Link:Player");
                if (planetType != null && planetType.ValueS == type && player == null)
                {
                    star = stars[starIdx];
                    planet = planets[planetIdx];
                    starPlanet = planets[0];
                    return;
                }
            }
        }

        star = null;
        planet = null;
        starPlanet = null;
    }

    private void GenerateNewMapSave_Players_B(DataBlock map, DataBlock playerList)
    {
        int playableID = 0;

        DataBlock playablePlayers = FromFile_B_PlayersData.GetSub("Playable");
        DataBlock fixedPlayers = FromFile_B_PlayersData.GetSub("Fixed");
        for (int idx = 0; idx < fixedPlayers.GetSubs().Count; idx++)
        {
            DataBlock playerFixed = fixedPlayers.GetSubs()[idx];

            bool human = false;
            if (idx == 0) human = true;

            string empireName = playerFixed.ValueS;
            if (playerFixed.ValueS == "Playable")
            {
                empireName = playablePlayers.Subs[playableID].Name;
                playableID++;
            }

            DataBlock empireInfo = DefLibrary.GetEmpire(empireName);

            string startingPlanetType = empireInfo.GetSub("StartingPlanetType").ValueS;

            DataBlock capitalStar = GenerateNewMapSave_Players_B_GetCapitalStar(map, empireName);
            Array<DataBlock> otherStars = GenerateNewMapSave_Players_B_GetOtherStars(map, empireName);

            DataBlock playerData = Data.AddData(playerList, "Player", empireName, DefLibrary);
            if (human) Data.AddData(playerData, "Human", DefLibrary);

            GenerateNewMapSave_Players_Empire(playerData, empireInfo);
            GenerateNewMapSave_Players_Resources(playerData);
            GenerateNewMapSave_Players_Status(playerData);
            GenerateNewMapSave_Players_Civics(playerData);
            GenerateNewMapSave_Players_Bonuses(playerData);

            GenerateNewMapSave_Players_Ship_Designs(playerData);
            GenerateNewMapSave_Players_StartingShip(playerData, capitalStar);

            DataBlock systemsList = Data.AddData(playerData, "Systems_List", DefLibrary);
            GenerateNewMapSave_Players_StartingSystem(playerData, systemsList, empireInfo, capitalStar);
            for (int colonyIdx = 0; colonyIdx < otherStars.Count; colonyIdx++)
            {
                GenerateNewMapSave_Players_StartingSystem(playerData, systemsList, empireInfo, otherStars[colonyIdx]);
            }
        }
    }

    private DataBlock GenerateNewMapSave_Players_B_GetCapitalStar(DataBlock mapData, string empireName)
    {
        DataBlock starList = mapData.GetSub("Star_List");
        Array<DataBlock> stars = starList.GetSubs("Star");

        for (int starIdx = 0; starIdx < stars.Count; starIdx++)
        {
            if (stars[starIdx].HasSub("Init_Player"))
            {
                string initPlayer = stars[starIdx].GetSub("Init_Player").ValueS;
                bool capital = stars[starIdx].HasSub("Init_Capital");
                if (initPlayer == empireName && capital)
                {
                    return stars[starIdx];
                }
            }
        }

        return null;
    }


    private Array<DataBlock> GenerateNewMapSave_Players_B_GetOtherStars(DataBlock mapData, string empireName)
    {
        Array<DataBlock> otherStars = new Array<DataBlock>();

        DataBlock starList = mapData.GetSub("Star_List");
        Array<DataBlock> stars = starList.GetSubs("Star");

        for (int starIdx = 0; starIdx < stars.Count; starIdx++)
        {
            if (stars[starIdx].HasSub("Init_Player"))
            {
                string initPlayer = stars[starIdx].GetSub("Init_Player").ValueS;
                bool capital = stars[starIdx].HasSub("Init_Capital");
                if (initPlayer == empireName && capital == false)
                {
                    otherStars.Add(stars[starIdx]);
                }
            }
        }

        return otherStars;
    }

    // --------------------------------------------------------------------------------------------------
    private void GenerateNewMapSave_Players_StartingSystem(DataBlock playerData, DataBlock systemsList, DataBlock empireInfo, DataBlock star)
    {
        DataBlock system = Data.AddData(systemsList, "System", star.ValueS, DefLibrary);
        GenerateNewMapSave_Players_StartingColony_SystemResources(system);

        Data.AddData(system, "Link:Star", star.ValueS, DefLibrary); // no StarData yet
        Data.AddData(star, "Link:Player:Sector:System", playerData.ValueS + ":" + system.ValueS, DefLibrary); // no SystemData yet

        if (star.HasSub("Init_Capital")) Data.AddData(system, "Capital", DefLibrary);

        DataBlock planetList = star.GetSub("Planet_List");
        DataBlock colonyList = Data.AddData(system, "Colony_List", DefLibrary);
        int development = star.GetSub("Init_Development").ValueI;

        switch (development)
        {
            case 1:
                {
                    DataBlock biggestHabitablePlanet = GetHabitablePlanet(planetList);
                    if (biggestHabitablePlanet != null) GenerateNewMapSave_Players_StartingColony(colonyList, star, biggestHabitablePlanet, playerData, system, 1);
                    break;
                }
            case 2:
                {
                    DataBlock biggestHabitablePlanet = GetHabitablePlanet(planetList);
                    if (biggestHabitablePlanet != null) GenerateNewMapSave_Players_StartingColony(colonyList, star, biggestHabitablePlanet, playerData, system, 2);
                    break;
                }
            case 3:
                {
                    DataBlock biggestHabitablePlanet = GetHabitablePlanet(planetList);
                    if (biggestHabitablePlanet != null) GenerateNewMapSave_Players_StartingColony(colonyList, star, biggestHabitablePlanet, playerData, system, 3);

                    DataBlock firstGasGiant = GetGasGiantPlanet(planetList);
                    if (firstGasGiant != null) GenerateNewMapSave_Players_StartingColony(colonyList, star, firstGasGiant, playerData, system, 1);

                    DataBlock asteroids = GetAsteroidsPlanet(planetList);
                    if (asteroids != null) GenerateNewMapSave_Players_StartingColony(colonyList, star, asteroids, playerData, system, 1);

                    break;
                }
            case 4:
                {
                    DataBlock biggestHabitablePlanet = GetHabitablePlanet(planetList);
                    DataBlock secondHabitablePlanet = GetHabitablePlanet(planetList);
                    DataBlock thirdHabitablePlanet = GetHabitablePlanet(planetList);
                    if (biggestHabitablePlanet != null) GenerateNewMapSave_Players_StartingColony(colonyList, star, biggestHabitablePlanet, playerData, system, 4);
                    if (secondHabitablePlanet != null) GenerateNewMapSave_Players_StartingColony(colonyList, star, secondHabitablePlanet, playerData, system, 3);
                    if (thirdHabitablePlanet != null) GenerateNewMapSave_Players_StartingColony(colonyList, star, thirdHabitablePlanet, playerData, system, 2);

                    DataBlock firstGasGiant = GetGasGiantPlanet(planetList);
                    DataBlock secondGasGiant = GetGasGiantPlanet(planetList);
                    if (firstGasGiant != null) GenerateNewMapSave_Players_StartingColony(colonyList, star, firstGasGiant, playerData, system, 1);
                    if (secondGasGiant != null) GenerateNewMapSave_Players_StartingColony(colonyList, star, secondGasGiant, playerData, system, 1);

                    DataBlock asteroids = GetAsteroidsPlanet(planetList);
                    if (asteroids != null) GenerateNewMapSave_Players_StartingColony(colonyList, star, asteroids, playerData, system, 1);

                    DataBlock firstOutpostPlanet = GetGasGiantPlanet(planetList);
                    if (firstOutpostPlanet != null) GenerateNewMapSave_Players_StartingColony(colonyList, star, firstOutpostPlanet, playerData, system, 1);

                    break;
                }
            case 5:
                {
                    DataBlock biggestHabitablePlanet = GetHabitablePlanet(planetList);
                    DataBlock secondHabitablePlanet = GetHabitablePlanet(planetList);
                    DataBlock thirdHabitablePlanet = GetHabitablePlanet(planetList);
                    if (biggestHabitablePlanet != null) GenerateNewMapSave_Players_StartingColony(colonyList, star, biggestHabitablePlanet, playerData, system, 5);
                    if (secondHabitablePlanet != null) GenerateNewMapSave_Players_StartingColony(colonyList, star, secondHabitablePlanet, playerData, system, 4);
                    if (thirdHabitablePlanet != null) GenerateNewMapSave_Players_StartingColony(colonyList, star, thirdHabitablePlanet, playerData, system, 3);

                    DataBlock firstGasGiant = GetGasGiantPlanet(planetList);
                    DataBlock secondGasGiant = GetGasGiantPlanet(planetList);
                    DataBlock thirdGasGiant = GetGasGiantPlanet(planetList);
                    if (firstGasGiant != null) GenerateNewMapSave_Players_StartingColony(colonyList, star, firstGasGiant, playerData, system, 1);
                    if (secondGasGiant != null) GenerateNewMapSave_Players_StartingColony(colonyList, star, secondGasGiant, playerData, system, 1);
                    if (thirdGasGiant != null) GenerateNewMapSave_Players_StartingColony(colonyList, star, thirdGasGiant, playerData, system, 1);

                    DataBlock asteroids = GetAsteroidsPlanet(planetList);
                    if (asteroids != null) GenerateNewMapSave_Players_StartingColony(colonyList, star, asteroids, playerData, system, 1);

                    DataBlock firstOutpostPlanet = GetGasGiantPlanet(planetList);
                    DataBlock secondOutpostPlanet = GetGasGiantPlanet(planetList);
                    DataBlock thirdOutpostPlanet = GetGasGiantPlanet(planetList);
                    if (firstOutpostPlanet != null) GenerateNewMapSave_Players_StartingColony(colonyList, star, firstOutpostPlanet, playerData, system, 1);
                    if (secondOutpostPlanet != null) GenerateNewMapSave_Players_StartingColony(colonyList, star, secondOutpostPlanet, playerData, system, 1);
                    if (thirdOutpostPlanet != null) GenerateNewMapSave_Players_StartingColony(colonyList, star, thirdOutpostPlanet, playerData, system, 1);
                    break;
                }
        }

        // actions
        //GenerateNewMapSave_Players_StartingColony_SectorCampaign(sector, system, colony);
        //GenerateNewMapSave_Players_StartingColony_SectorConstruction(sector, system, colony);
    }

    private void GenerateNewMapSave_Players_StartingColony(DataBlock colonyList, DataBlock star, DataBlock planet, DataBlock player, DataBlock system, int level)
    {
        DataBlock colony = Data.AddData(colonyList, "Colony", planet.ValueS, DefLibrary);

        if (planet.HasSub("Habitable"))
        {
            Data.AddData(colony, "Type", "Urban_World", DefLibrary);

            DataBlock pops = Data.AddData(colony, "Pops", DefLibrary);
            Data.AddData(pops, "Public", planet.GetSub("Size").ValueI * 4 * level, DefLibrary);
            Data.AddData(pops, "Private", planet.GetSub("Size").ValueI * 6 * level, DefLibrary);
            Data.AddData(pops, "Uncontrolled", 0, DefLibrary);

            Data.AddData(colony, "Factories", 5 * level, DefLibrary);
            Data.AddData(colony, "Bases", level, DefLibrary);

            //GenerateNewMapSave_Players_StartingColony_Resources(colony);
        }
        else if (planet.HasSub("Uninhabitable"))
        {
            Data.AddData(colony, "Type", "Mining_Outposts", DefLibrary);
        }
        else if (planet.HasSub("Asteroids"))
        {
            Data.AddData(colony, "Type", "Asteroid_Mines", DefLibrary);
        }
        else if (planet.HasSub("Gas_Giant"))
        {
            Data.AddData(colony, "Type", "Reserch_Stations", DefLibrary);
        }

        Data.AddData(colony, "Link:Star:Planet", star.ValueS + ":" + planet.ValueS, DefLibrary); // no PlanetData yet
        Data.AddData(planet, "Link:Player:System:Colony", player.ValueS + ":" + system.ValueS + ":" + colony.ValueS, DefLibrary); // no ColonyData yet
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
    private DataBlock GetHabitablePlanet(DataBlock planetList, DataBlock exception_1 = null, DataBlock exception_2 = null)
    {
        DataBlock planet = null;
        int maxSize = 0;
        for (int idx = 0; idx < planetList.GetSubs().Count; idx++)
        {
            if (planetList.Subs[idx] == exception_1) continue;
            if (planetList.Subs[idx] == exception_2) continue;
            if (planetList.Subs[idx].HasSub("Habitable") && planetList.Subs[idx].GetSub("Size").ValueI > maxSize)
            {
                planet = planetList.Subs[idx];
                maxSize = planetList.Subs[idx].GetSub("Size").ValueI;
            }
        }
        return planet;
    }

    private DataBlock GetUninhabitablePlanet(DataBlock planetList, DataBlock exception_1 = null, DataBlock exception_2 = null)
    {
        DataBlock planet = null;
        bool hasBonus = false;
        for (int idx = 0; idx < planetList.GetSubs().Count; idx++)
        {
            if (planetList.Subs[idx] == exception_1) continue;
            if (planetList.Subs[idx] == exception_2) continue;
            if (planetList.Subs[idx].HasSub("Uninhabitable") && (planet == null || (planetList.Subs[idx].HasSub("Bonus") && hasBonus == false)))
                {
                    planet = planetList.Subs[idx];
                    hasBonus = planetList.Subs[idx].HasSub("Bonus");
                }
        }
        return planet;
    }

    private DataBlock GetGasGiantPlanet(DataBlock planetList, DataBlock exception_1 = null, DataBlock exception_2 = null)
    {
        DataBlock planet = null;
        bool hasBonus = false;
        for (int idx = 0; idx < planetList.GetSubs().Count; idx++)
        {
            if (planetList.Subs[idx] == exception_1) continue;
            if (planetList.Subs[idx] == exception_2) continue;
            if (planetList.Subs[idx].HasSub("Gas_Giant") && (planet == null || (planetList.Subs[idx].HasSub("Bonus") && hasBonus == false)))
            {
                planet = planetList.Subs[idx];
                hasBonus = planetList.Subs[idx].HasSub("Bonus");
            }
        }
        return planet;
    }

    private DataBlock GetAsteroidsPlanet(DataBlock planetList)
    {
        for (int idx = 0; idx < planetList.GetSubs().Count; idx++)
        {
            if (planetList.Subs[idx].HasSub("Asteroids"))
            {
                return planetList.Subs[idx];
            }
        }
        return null;
    }

    // --------------------------------------------------------------------------------------------------
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
