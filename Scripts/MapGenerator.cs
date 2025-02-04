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
        //Camera.MoveLimitX = 8.6666f * 3.8f * FromFile_Size;
        //Camera.MoveLimitY = 15.0f * 2.2f * FromFile_Size;
    }

    // --------------------------------------------------------------------------------------------------
    public DataBlock GenerateNewMapSave()
    {
        DataBlock mapData = new DataBlock();
        mapData.Type = DefLibrary.GetDBType("_Map", Data.BaseType.NONE);

        GenerateNewMapSave_GameStats(mapData);

        GenerateNewMapSave_Star(mapData);

        GenerateNewMapSave_Players(mapData);

        GenerateNewMapSave_Relations(mapData);

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
        var file = FileAccess.Open("res:///Map/" + MapTypeFile + ".map", FileAccess.ModeFlags.Read);
        if (file == null)
        {
            file = FileAccess.Open("Map/" + MapTypeFile + ".map", FileAccess.ModeFlags.Read);
        }
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

                Data.AddData(starData, "Visibility", DefLibrary);
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
                string empireLetter = "";
                if (FromFile_Stars[idx][0] == 'x')
                {
                    level = FromFile_Stars[idx][1].ToString().ToInt();
                    Data.AddData(starData, "Init_Level", FromFile_Stars[idx][1], DefLibrary);
                }
                else
                {
                    empireLetter = FromFile_Stars[idx][0].ToString();
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
                    int pops = FromFile_Stars[idx][1].ToString().ToInt();
                    Data.AddData(starData, "Init_PopsLevel", pops, DefLibrary);
                    int infrastructure = FromFile_Stars[idx][2].ToString().ToInt();
                    Data.AddData(starData, "Init_Infrastructure", infrastructure, DefLibrary);
                    level = FromFile_Stars[idx][3].ToString().ToInt();
                    Data.AddData(starData, "Init_Level", level, DefLibrary);
                    if (FromFile_Stars[idx].Length > 4 && FromFile_Stars[idx][4] == 'c')
                    {
                        capital = true;
                        Data.AddData(starData, "Init_Capital", DefLibrary);
                    }
                }

                GenerateNewMapSave_Star_Planets_B(starData, level, capital, capitalType, temperature, empireLetter);

                Data.AddData(starData, "Visibility", DefLibrary);
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

    public void GenerateNewMapSave_Star_Planets_B(DataBlock star, int level, bool capital, string capitalType, int temperature, string empireLetter)
    {
        DataBlock planetList = Data.AddData(star, "Planet_List", DefLibrary);
        if (empireLetter == "A" && capital)
        {
            star.ValueS = "Sol";
            GenerateNewMapSave_Stars_Planets_Sol(planetList);
        }
        else
        {
            GenerateNewMapSave_Stars_Planets_Level(star.ValueS, planetList, level, capital, capitalType, temperature);
        }
    }

    // --------------------------------------------------------------------------------------------------
    public void GenerateNewMapSave_Players(DataBlock map)
    {
        DataBlock playerList = Data.AddData(map, "Player_List", DefLibrary);

        //if (Type == 'A')
        //    GenerateNewMapSave_Players_A(map, playerList); // no longle in files - check archive

        GenerateNewMapSave_Players_B(map, playerList);
    }

    // --------------------------------------------------------------------------------------------------
    public void GenerateNewMapSave_Relations(DataBlock map)
    {
        DataBlock playerList = Data.AddData(map, "Relations", DefLibrary);
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
            GenerateNewMapSave_Players_Stockpiles(playerData);
            GenerateNewMapSave_Players_Status(playerData);
            GenerateNewMapSave_Players_Civics(playerData);
            GenerateNewMapSave_Players_Bonuses(playerData);

            GenerateNewMapSave_Players_Ship_Designs(playerData);

            DataBlock systemsList = Data.AddData(playerData, "Systems_List", DefLibrary);
            int popsLevel = capitalStar.GetSub("Init_PopsLevel").ValueI;
            int infrastructure = capitalStar.GetSub("Init_Infrastructure").ValueI;
            DataBlock system = SystemRaw.CreateNewSystem(playerData, capitalStar, DefLibrary, true);
            SystemRaw.GrowSystem(playerData, system, capitalStar, popsLevel, infrastructure, DefLibrary);
            GenerateNewMapSave_Players_StartingShip(playerData, capitalStar, system);
            //GenerateNewMapSave_Players_StartingSystem(playerData, systemsList, empireInfo, capitalStar);
            for (int colonyIdx = 0; colonyIdx < otherStars.Count; colonyIdx++)
            {
                popsLevel = otherStars[colonyIdx].GetSub("Init_PopsLevel").ValueI;
                infrastructure = otherStars[colonyIdx].GetSub("Init_Infrastructure").ValueI;
                system = SystemRaw.CreateNewSystem(playerData, otherStars[colonyIdx], DefLibrary);
                SystemRaw.GrowSystem(playerData, system, otherStars[colonyIdx], popsLevel, infrastructure, DefLibrary);
                //GenerateNewMapSave_Players_StartingSystem(playerData, systemsList, empireInfo, otherStars[colonyIdx]);
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
