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
    public MapNode MapData;
    [Export]
    public string MapTypeFile = "";
    [Export]
    public string StarGFXName = "";
    [Export]
    public Node LocationsNode = null;
    [Export]
    public Node PawnsNode = null;

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

    public void ClearMap()
    {
        while (LocationsNode.GetChildCount(true) > 0)
        {
            Node child = LocationsNode.GetChild(0, true);
            LocationsNode.RemoveChild(child);
            child.Free();
        }
    }

    public void GenerateMapFunc()
    {
        ClearMap();
        MapData.Systems.Clear();

        LoadMapFile();

        if (DefLibrary.Locations.Count == 0)
        {
            GD.PrintErr("Map gen error 01");
            return;
        }

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

                DataBlock system = null;
                if (idx == 22) system = GenerateSolarSystemCustom_Sol();
                else system = GenerateSolarSystem();

                LocationData locationData = new LocationData();
                locationData.System = system;
                locationData.Name = system.ValueS + "_Data";
                locationData.X = x;
                locationData.Y = y;

                MapData.Systems.Add(locationData);

                //node.AddChild(node.Data);
                //node.Data.Owner = GetTree().EditedSceneRoot;

                CreateLocationNode(x, y, locationData);
            }
        }

        // set camera
        MapCamera Camera = GetTree().EditedSceneRoot.GetNode<MapCamera>("Camera3D");
        Camera.MoveLimitX = 8.6666f * 2.0f * 2 * FromFile_Size;
        Camera.MoveLimitY = 15.0f * 2 * FromFile_Size;

        //for ( int idx = 0; idx < DefLibrary.Locations.Count; idx++ )
        //{
        //    LocationNode node = new LocationNode();
        //    node.Def = DefLibrary.Locations[idx];
        //    node.Name = node.Def.LocationName;
        //
        //    LocationsNode.AddChild(node, true, InternalMode.Back);
        //    node.Owner = GetTree().EditedSceneRoot;
        //
        //    node.Data = new LocationData();
        //    node.Data.Name = node.Name + "Data";
        //    node.Data.Population = node.Def.Population;
        //    node.Data.Prosperity = 15;
        //    node.AddChild(node.Data);
        //    node.Data.Owner = GetTree().EditedSceneRoot;
        //
        //    PackedScene gfxScene = GD.Load<PackedScene>("res:///3DPrefabs/" + SarGFXName + ".tscn");
        //    Node gfxNode = gfxScene.Instantiate();
        //    gfxNode.Name = node.Name + "GFX";
        //    node.AddChild(gfxNode);
        //    gfxNode.Owner = GetTree().EditedSceneRoot;
        //    gfxNode.GetParent().SetEditableInstance(gfxNode, true);
        //
        //    node.GFX = gfxNode as LocationGFX;
        //    node.GFX.Position = new Vector3( node.Def.Positon.X, 0.0f, node.Def.Positon.Y);
        //    node.GFX.SetLocationName(node.Name);
        //}

        //for ( int idx = 0; idx < DefLibrary.Pawns.Count; idx++ )
        //{
        //    PawnNode node = new PawnNode();
        //    node.Def = DefLibrary.Pawns[idx];
        //    node.Name = node.Def.PawnName;
        //
        //    PawnsNode.AddChild(node, true, InternalMode.Back);
        //    node.Owner = GetTree().EditedSceneRoot;
        //
        //    node.Data = new PawnData();
        //    node.Data.Name = node.Name + "Data";
        //    node.Data.Power = node.Def.Power;
        //    node.Data.Age = node.Def.Age;
        //    node.AddChild(node.Data);
        //    node.Data.Owner = GetTree().EditedSceneRoot;
        //
        //    PackedScene gfxScene = GD.Load<PackedScene>("res:///3DPrefabs/Pawn.tscn");
        //    Node gfxNode = gfxScene.Instantiate();
        //    gfxNode.Name = node.Name + "GFX";
        //    node.AddChild(gfxNode);
        //    gfxNode.Owner = GetTree().EditedSceneRoot;
        //    gfxNode.GetParent().SetEditableInstance(gfxNode, true);
        //
        //    node.GFX = gfxNode as PawnGFX;
        //
        //    // set in Location
        //    // node.Def.StartingLocation == somewhere in map
        //}
    }

    public void CreateLocationNode(int x, int y, LocationData locationData)
    {
        LocationNode node = new LocationNode();
        node.Def = DefLibrary.Locations[0];
        node.Name = "Loc_" + locationData.System.ValueS;
        node.Data = locationData;

        LocationsNode.AddChild(node, true, InternalMode.Back);
        node.Owner = GetTree().EditedSceneRoot;

        node.AddChild(locationData);
        locationData.Owner = GetTree().EditedSceneRoot;

        PackedScene gfxScene = GD.Load<PackedScene>("res:///3DPrefabs/" + StarGFXName + ".tscn");
        Node gfxNode = gfxScene.Instantiate();
        gfxNode.Name = locationData.System.ValueS + "_GFX";
        node.AddChild(gfxNode);
        gfxNode.Owner = GetTree().EditedSceneRoot;
        gfxNode.GetParent().SetEditableInstance(gfxNode, true);

        node.GFX = gfxNode as LocationGFX;
        node.GFX.Position = new Vector3(8.6666f * (2.0f * x - y), 0.0f, -y * 15.0f) - new Vector3(8.6666f * FromFile_Size, 0.0f, -FromFile_Size * 15.0f);
        node.GFX.SetLocationName(node.Name);
    }

    public void LoadMapFile()
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

    string[] GetWordsFromNextValidRow(string[] rows, ref int rowIdx)
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

    private DataBlock GenerateSolarSystem()
    {
        DataBlock system = new DataBlock();

        system.Type = DefLibrary.GetDBType("System", Data.BaseType.STRING);
        system.ValueS = "System_" + RNG.RandiRange(1, 99);

        DataBlock star = Data.AddData(system, "Planet", "Star", DefLibrary);
        int starSize = RNG.RandiRange(1, 7);
        Data.AddData(star, "Star_Size", starSize, DefLibrary);
        Data.AddData(star, "Star_Type", "Yellow_Dwarf", DefLibrary);


        int noOfPlanes = RNG.RandiRange(3 * (3 - Math.Abs(4 - starSize)), 6 + 2 * (3 - Math.Abs(4 - starSize)));
        int gasSystem = RNG.RandiRange(1, 2);
        int gasPlanets = RNG.RandiRange(0, noOfPlanes * 2 / 3);
        int currentTemp = 5;
        int parentPlanetSize = 8;
        int moons = 0;

        for (int n = 0; n < noOfPlanes; n++)
        {
            if (starSize < 3)
            {
                while (RNG.RandiRange(0, 99) < 66) currentTemp--;
            }
            else if (starSize < 6)
            {
                if (n > 0 && RNG.RandiRange(0, 99) < 66) currentTemp--;
            }
            else
            {
                if (n > 0 && RNG.RandiRange(0, 99) < 50) currentTemp--;
            }

            if (currentTemp < 1) currentTemp = 1;

            if (n == noOfPlanes - 1)
            {
                DataBlock planet = Data.AddData(system, "Planet", "Outer_System", DefLibrary);
                Data.AddData(planet, "Type", "Outer_System", DefLibrary);
            }
            else
            {
                if (moons == 0 && (( gasSystem == 1 && n < gasPlanets) || (gasSystem == 2 && n >= gasPlanets)))
                {
                    GenerateGasGiant(system, currentTemp, gasSystem == 2, out parentPlanetSize, out moons);

                }
                else
                {
                    GeneratePlanet(system, currentTemp, ref parentPlanetSize, ref moons);
                }
            }
        }

        return system;
    }

    private void GenerateGasGiant(DataBlock system, int currentTemp, bool maxSize, out int parentPlanetSize, out int moons)
    {
        DataBlock planet = Data.AddData(system, "Planet", "Gas Giant", DefLibrary);
        parentPlanetSize = RNG.RandiRange(6, 8 + (maxSize ? 1 : 0));
        Data.AddData(planet, "Size", parentPlanetSize, DefLibrary);
        Data.AddData(planet, "Type", "Gas_Giant", DefLibrary);
        Data.AddData(planet, "Temperature", currentTemp, DefLibrary);
        if (RNG.RandiRange(0, 99) < 40) Data.AddData(planet, "CH4", 1, DefLibrary);
        if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "He3", DefLibrary);
        if (RNG.RandiRange(0, 99) < 30) Data.AddData(planet, "Rings", DefLibrary);
        if (RNG.RandiRange(0, 99) < 20) Data.AddData(planet, "Magnetic_Field", DefLibrary);

        if (RNG.RandiRange(0, 99) < 5) moons = 3;
        else if (RNG.RandiRange(0, 99) < 15) moons = 2;
        else if (RNG.RandiRange(0, 99) < 50) moons = 1;
        else moons = 0;
    }

    private void GeneratePlanet(DataBlock system, int currentTemp, ref int parentPlanetSize, ref int moons)
    {
        List<string> possibleTypes = new List<string>();

        switch (currentTemp)
        {
            case 1:
                {
                    possibleTypes.Add("Barren");
                    possibleTypes.Add("Frozen");
                    break;
                }
            case 2:
                {
                    possibleTypes.Add("Barren");
                    possibleTypes.Add("Toxic");
                    possibleTypes.Add("Desert");
                    possibleTypes.Add("Arid");
                    possibleTypes.Add("Temperate");
                    possibleTypes.Add("Ocean");
                    possibleTypes.Add("Swamp");
                    possibleTypes.Add("Artic");
                    possibleTypes.Add("Frozen");
                    break;
                }
            case 3:
                {
                    possibleTypes.Add("Vulcanic");
                    possibleTypes.Add("Barren");
                    possibleTypes.Add("Toxic");
                    possibleTypes.Add("Desert");
                    possibleTypes.Add("Arid");
                    possibleTypes.Add("Temperate");
                    possibleTypes.Add("Ocean");
                    possibleTypes.Add("Swamp");
                    possibleTypes.Add("Jungle");
                    possibleTypes.Add("Artic");
                    break;
                }
            case 4:
                {
                    possibleTypes.Add("Lava");
                    possibleTypes.Add("Vulcanic");
                    possibleTypes.Add("Barren");
                    possibleTypes.Add("Toxic");
                    possibleTypes.Add("Desert");
                    possibleTypes.Add("Arid");
                    possibleTypes.Add("Temperate");
                    possibleTypes.Add("Ocean");
                    possibleTypes.Add("Swamp");
                    possibleTypes.Add("Jungle");
                    break;
                }
            case 5:
                {
                    possibleTypes.Add("Lava");
                    possibleTypes.Add("Vulcanic");
                    possibleTypes.Add("Barren");
                    break;
                }
        }

        string type = possibleTypes[RNG.RandiRange(0, possibleTypes.Count - 1)];
        DataBlock planet = Data.AddData(system, "Planet", type, DefLibrary);
        int size = RNG.RandiRange(1, 7);
        if (moons > 0)
        {
            size = RNG.RandiRange(1, parentPlanetSize / 2);
        }
        else
        {
            parentPlanetSize = size;
        }
        Data.AddData(planet, "Size", size, DefLibrary);

        Data.AddData(planet, "Type", type, DefLibrary);
        Data.AddData(planet, "Temperature", currentTemp, DefLibrary);
        GeneratePlanet_Atmomsphere(planet, type, size);
        GeneratePlanet_Core(planet, type, size, currentTemp);
        GeneratePlanet_WaterAndLife(planet, type, size, currentTemp);
        if (RNG.RandiRange(0, 99) < 5) Data.AddData(planet, "Rings", DefLibrary);

        if (moons > 0)
        {
            Data.AddData(planet, "Moon", DefLibrary);
            moons--;
        }
        else
        {
            if (RNG.RandiRange(0, 99) < 10) moons++;
            if (RNG.RandiRange(0, 99) < 10) moons++;
        }
    }

    private void GeneratePlanet_Atmomsphere(DataBlock planet, string planetType, int planetSize)
    {
        switch (planetType)
        {
            case "Lava":
                {
                    if (RNG.RandiRange(0, 99) < 10 * (planetSize - 1))
                    {
                        Data.AddData(planet, "Atmosphere", "CO2", DefLibrary);
                        if (RNG.RandiRange(0, 99) < 100 - 40 * (planetSize - 2)) Data.AddData(planet, "Low_Atmosphere", DefLibrary);
                        if (RNG.RandiRange(0, 99) < 40) Data.AddData(planet, "Toxic", DefLibrary);
                    }
                    break;
                }
            case "Vulcanic":
                {
                    if (RNG.RandiRange(0, 99) < 20 * (3 - planetSize))
                    {
                        break;
                    }
                    else
                    {
                        GeneratePlanet_Atmomsphere_Options(planet, planetSize, "CO2", "N", "O2");
                        if (RNG.RandiRange(0, 99) < 20 ) Data.AddData(planet, "Toxic", DefLibrary);
                    }
                    break;
                }
            case "Barren":
                {
                    if (RNG.RandiRange(0, 99) < 10 * (planetSize - 1))
                    {
                        Data.AddData(planet, "Atmosphere", "N", DefLibrary);
                        Data.AddData(planet, "Low_Atmosphere", DefLibrary);
                    }
                    break;
                }
            case "Toxic":
                {
                    GeneratePlanet_Atmomsphere_Options(planet, planetSize, "CO2", "N", "O2");
                    Data.AddData(planet, "Toxic", DefLibrary);
                    break;
                }
            case "Desert":
                {
                    GeneratePlanet_Atmomsphere_Options(planet, planetSize, "N", "CO2", "O2");
                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Toxic", DefLibrary);
                    break;
                }
            case "Arid":
                {
                    GeneratePlanet_Atmomsphere_Options(planet, planetSize, "N", "CO2", "O2");
                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Toxic", DefLibrary);
                    break;
                }
            case "Temperate":
                {
                    GeneratePlanet_Atmomsphere_Options(planet, planetSize, "N", "O2");
                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Toxic", DefLibrary);
                    break;
                }
            case "Ocean":
                {
                    GeneratePlanet_Atmomsphere_Options(planet, planetSize, "N", "O2");
                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Toxic", DefLibrary);
                    break;
                }
            case "Swamp":
                {
                    GeneratePlanet_Atmomsphere_Options(planet, planetSize, "N", "CO2", "O2");
                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Toxic", DefLibrary);
                    if (RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "CH4", 1, DefLibrary);
                    break;
                }
            case "Jungle":
                {
                    GeneratePlanet_Atmomsphere_Options(planet, planetSize, "N", "O2");
                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Toxic", DefLibrary);
                    break;
                }
            case "Artic":
                {
                    GeneratePlanet_Atmomsphere_Options(planet, planetSize, "N", "O2");
                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Toxic", DefLibrary);
                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "CH4", 1, DefLibrary);
                    break;
                }
            case "Frozen":
                {
                    if (RNG.RandiRange(0, 99) < 10 * (planetSize - 1))
                    {
                        Data.AddData(planet, "Atmosphere", "N", DefLibrary);
                        Data.AddData(planet, "Low_Atmosphere", DefLibrary);
                        if (RNG.RandiRange(0, 99) < 30) Data.AddData(planet, "CH4", 1, DefLibrary);
                    }
                    break;
                }
        }
    }

    private void GeneratePlanet_Atmomsphere_Options(DataBlock planet, int planetSize, string firstOption, string secondOption = "", string thirdOption = "")
    {
        if (RNG.RandiRange(0, 99) < 70 || secondOption == "")
        {
            Data.AddData(planet, "Atmosphere", firstOption, DefLibrary);
        }
        else if (RNG.RandiRange(0, 99) < 70 || thirdOption == "")
        {
            Data.AddData(planet, "Atmosphere", secondOption, DefLibrary);
        }
        else
        {
            Data.AddData(planet, "Atmosphere", thirdOption, DefLibrary);
        }
        if (RNG.RandiRange(0, 99) < 100 - 40 * (planetSize - 2)) Data.AddData(planet, "Low_Atmosphere", DefLibrary);
    }

    private void GeneratePlanet_Core(DataBlock planet, string planetType, int planetSize, int temperature)
    {
        int iradiated = 2;
        if (temperature <= 3) iradiated = 1;

        bool molternCore = false;
        if (RNG.RandiRange(0, 99) < 10 * (planetSize - 1)) molternCore = true;
        if (planetType == "Lava" || planetType == "Vulcanic") molternCore = true;

        bool magneticField = false;
        if (molternCore && RNG.RandiRange(0, 99) < 50) magneticField = true;

        if (planetType == "Lava" || planetType == "Vulcanic" || molternCore && RNG.RandiRange(0, 99) < 20)
        {
            Data.AddData(planet, "Vulcanic", DefLibrary);
        }
        else if (molternCore)
        {
            Data.AddData(planet, "Molten_Core", DefLibrary);
            if (magneticField) Data.AddData(planet, "Magnetic_Field", DefLibrary);
            if (RNG.RandiRange(0, 99) < 20)
            {
                if (planetType == "Ocean") Data.AddData(planet, "Islands", DefLibrary);
                else Data.AddData(planet, "Alpine", DefLibrary);
            }
            iradiated--;
        }

        if (iradiated == 2) Data.AddData(planet, "Radiation", "High_Radiation", DefLibrary);
        if (iradiated == 1) Data.AddData(planet, "Radiation", "Low_Radiation", DefLibrary);
    }

    private void GeneratePlanet_WaterAndLife(DataBlock planet, string planetType, int planetSize, int temperature)
    {

        switch (planetType)
        {
            case "Lava":
                {
                    if (planet.GetSub("Atmosphere") != null)
                    {
                        if (RNG.RandiRange(0, 99) < 5) Data.AddData(planet, "Water", "Vapor", DefLibrary);
                        if (RNG.RandiRange(0, 99) < 5) Data.AddData(planet, "Life", "Micorbial_Life", DefLibrary);
                    }
                    break;
                }
            case "Vulcanic":
                {
                    if (planet.GetSub("Atmosphere") != null)
                    {
                        if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Water", "Vapor", DefLibrary);
                        if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Life", "Micorbial_Life", DefLibrary);
                    }
                    break;
                }
            case "Barren":
                {
                    break;
                }
            case "Toxic":
                {
                    if (RNG.RandiRange(0, 99) < 20) Data.AddData(planet, "Water", "Vapor", DefLibrary);
                    else if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Water", "Lakes", DefLibrary);

                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Life", "Micorbial_Life", DefLibrary);
                    else if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Life", "Simple_life", DefLibrary);

                    if (temperature == 2) Data.AddData(planet, "Ice", DefLibrary);
                    else if (temperature == 3 && RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Ice", DefLibrary);

                    break;
                }
            case "Desert":
                {
                    Data.AddData(planet, "Water", "Vapor", DefLibrary);

                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Life", "Complex_life", DefLibrary);
                    else if (RNG.RandiRange(0, 99) < 20) Data.AddData(planet, "Life", "Simple_life", DefLibrary);
                    else if(RNG.RandiRange(0, 99) < 30) Data.AddData(planet, "Life", "Micorbial_Life", DefLibrary);

                    if (temperature == 2) Data.AddData(planet, "Ice", DefLibrary);
                    else if (temperature == 3 && RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Ice", DefLibrary);

                    break;
                }
            case "Arid":
                {
                    Data.AddData(planet, "Water", "Lakes", DefLibrary);

                    if (RNG.RandiRange(0, 99) < 20) Data.AddData(planet, "Life", "Complex_life", DefLibrary);
                    else if (RNG.RandiRange(0, 99) < 30) Data.AddData(planet, "Life", "Simple_life", DefLibrary);
                    else if (RNG.RandiRange(0, 99) < 40) Data.AddData(planet, "Life", "Micorbial_Life", DefLibrary);

                    if (temperature == 2) Data.AddData(planet, "Ice", DefLibrary);
                    else if (temperature == 3 && RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Ice", DefLibrary);

                    break;
                }
            case "Temperate":
                {
                    if (RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Water", "Lakes", DefLibrary);
                    else Data.AddData(planet, "Water", "Oceans", DefLibrary);

                    if (RNG.RandiRange(0, 99) < 30) Data.AddData(planet, "Life", "Complex_life", DefLibrary);
                    else if (RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Life", "Simple_life", DefLibrary);
                    else Data.AddData(planet, "Life", "Micorbial_Life", DefLibrary);

                    if (temperature == 2) Data.AddData(planet, "Ice", DefLibrary);
                    else if (temperature == 3 && RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Ice", DefLibrary);

                    break;
                }
            case "Ocean":
                {
                    Data.AddData(planet, "Water", "Water_Covered", DefLibrary);

                    if (RNG.RandiRange(0, 99) < 30) Data.AddData(planet, "Life", "Complex_life", DefLibrary);
                    else if (RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Life", "Simple_life", DefLibrary);
                    else Data.AddData(planet, "Life", "Micorbial_Life", DefLibrary);

                    if (temperature == 2) Data.AddData(planet, "Ice", DefLibrary);
                    else if (temperature == 3 && RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Ice", DefLibrary);

                    break;
                }
            case "Swamp":
                {
                    Data.AddData(planet, "Water", "Lakes", DefLibrary);

                    if (RNG.RandiRange(0, 99) < 30) Data.AddData(planet, "Life", "Complex_life", DefLibrary);
                    else if (RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Life", "Simple_life", DefLibrary);
                    else Data.AddData(planet, "Life", "Micorbial_Life", DefLibrary);

                    if (temperature == 2) Data.AddData(planet, "Ice", DefLibrary);
                    else if (temperature == 3 && RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Ice", DefLibrary);

                    break;
                }
            case "Jungle":
                {
                    if (RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Water", "Lakes", DefLibrary);
                    else Data.AddData(planet, "Water", "Oceans", DefLibrary);

                    if (RNG.RandiRange(0, 99) < 80) Data.AddData(planet, "Life", "Complex_life", DefLibrary);
                    else Data.AddData(planet, "Life", "Simple_life", DefLibrary);

                    break;
                }
            case "Artic":
                {
                    if (RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Water", "Lakes", DefLibrary);
                    else if (RNG.RandiRange(0, 99) < 80) Data.AddData(planet, "Water", "Oceans", DefLibrary);
                    else Data.AddData(planet, "Water", "Water-Covered", DefLibrary);

                    if (RNG.RandiRange(0, 99) < 10) Data.AddData(planet, "Life", "Complex_life", DefLibrary);
                    else if (RNG.RandiRange(0, 99) < 20) Data.AddData(planet, "Life", "Simple_life", DefLibrary);
                    else Data.AddData(planet, "Life", "Micorbial_Life", DefLibrary);

                    Data.AddData(planet, "Ice", DefLibrary);

                    break;
                }
            case "Frozen":
                {
                    if (temperature == 1 && RNG.RandiRange(0, 99) < 10)
                    {
                        Data.AddData(planet, "Ice", DefLibrary);
                        if (RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Life", "Micorbial_Life", DefLibrary);
                    }
                    else if (temperature == 2 && RNG.RandiRange(0, 99) < 50)
                    {
                        Data.AddData(planet, "Ice", DefLibrary);
                        if (RNG.RandiRange(0, 99) < 50) Data.AddData(planet, "Life", "Micorbial_Life", DefLibrary);
                    }
                    break;
                }
        }

        DataBlock lifeData = planet.GetSub("Life");
        if (lifeData != null && lifeData.ValueS == "Complex_life")
        {
            if (RNG.RandiRange(0, 99) < 50)  Data.AddData(planet, "Hostile_Life", DefLibrary);
        }
    }

    private DataBlock GenerateSolarSystemCustom_Sol()
    {
        DataBlock system = new DataBlock();

        system.Type = DefLibrary.GetDBType("System", Data.BaseType.STRING);
        system.ValueS = "Sol";

        DataBlock star = Data.AddData(system, "Planet", "Star", DefLibrary);
        Data.AddData(star, "Star_Size", 4, DefLibrary);
        Data.AddData(star, "Star_Type", "Yellow_Dwarf", DefLibrary);

        {
            DataBlock planet = Data.AddData(system, "Planet", "Mercury", DefLibrary);
            Data.AddData(planet, "Size", 1, DefLibrary);
            Data.AddData(planet, "Type", "Barren", DefLibrary);
            Data.AddData(planet, "Temperature", 5, DefLibrary);
            Data.AddData(planet, "Radiation", "High_Radiation", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(system, "Planet", "Venus", DefLibrary);
            Data.AddData(planet, "Size", 4, DefLibrary);
            Data.AddData(planet, "Type", "Toxic", DefLibrary);
            Data.AddData(planet, "Temperature", 4, DefLibrary);
            Data.AddData(planet, "CO2", DefLibrary);
            Data.AddData(planet, "Toxic", DefLibrary);
            Data.AddData(planet, "Radiation", "Low_Radiation", DefLibrary);
            Data.AddData(planet, "Molten_Core", DefLibrary);
            Data.AddData(planet, "Magnetic_Field", DefLibrary);
            Data.AddData(planet, "Vulcanic", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(system, "Planet", "Terra", DefLibrary);
            Data.AddData(planet, "Custom", DefLibrary);
            Data.AddData(planet, "Size", 4, DefLibrary);
            Data.AddData(planet, "Type", "Temperate", DefLibrary);
            Data.AddData(planet, "Temperature", 3, DefLibrary);
            Data.AddData(planet, "O2", DefLibrary);
            Data.AddData(planet, "N", DefLibrary);
            Data.AddData(planet, "Molten_Core", DefLibrary);
            Data.AddData(planet, "Magnetic_Field", DefLibrary);
            Data.AddData(planet, "Water", "Oceans", DefLibrary);
            Data.AddData(planet, "Ice", DefLibrary);
            Data.AddData(planet, "Life", "Complex_Life", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(system, "Planet", "Moon", DefLibrary);
            Data.AddData(planet, "Size", 1, DefLibrary);
            Data.AddData(planet, "Type", "Barren", DefLibrary);
            Data.AddData(planet, "Temperature", 3, DefLibrary);
            Data.AddData(planet, "Radiation", "Low_Radiation", DefLibrary);
            Data.AddData(planet, "Moon", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(system, "Planet", "Mars", DefLibrary);
            Data.AddData(planet, "Size", 3, DefLibrary);
            Data.AddData(planet, "Type", "Desert", DefLibrary);
            Data.AddData(planet, "Temperature", 2, DefLibrary);
            Data.AddData(planet, "CO2", DefLibrary);
            Data.AddData(planet, "Low_Atmosphere", DefLibrary);
            Data.AddData(planet, "Radiation", "Low_Radiation", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(system, "Planet", "Asteroid_Belt", DefLibrary);
            Data.AddData(planet, "Size", 3, DefLibrary);
            Data.AddData(planet, "Type", "Asteroid_Belt", DefLibrary);
            Data.AddData(planet, "Temperature", 2, DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(system, "Planet", "Jupiter", DefLibrary);
            Data.AddData(planet, "Size", 9, DefLibrary);
            Data.AddData(planet, "Type", "Gas_Giant", DefLibrary);
            Data.AddData(planet, "Temperature", 1, DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(system, "Planet", "Ganymede", DefLibrary);
            Data.AddData(planet, "Size", 2, DefLibrary);
            Data.AddData(planet, "Type", "Frozen", DefLibrary);
            Data.AddData(planet, "Temperature", 1, DefLibrary);
            Data.AddData(planet, "Forzen_Ocean", DefLibrary);
            Data.AddData(planet, "Moon", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(system, "Planet", "Saturn", DefLibrary);
            Data.AddData(planet, "Size", 8, DefLibrary);
            Data.AddData(planet, "Type", "Gas_Giant", DefLibrary);
            Data.AddData(planet, "Temperature", 1, DefLibrary);
            Data.AddData(planet, "CH4", 1, DefLibrary);
            Data.AddData(planet, "Rings", DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(system, "Planet", "Titan", DefLibrary);
            Data.AddData(planet, "Size", 1, DefLibrary);
            Data.AddData(planet, "Type", "Frozen", DefLibrary);
            Data.AddData(planet, "Temperature", 1, DefLibrary);
            Data.AddData(planet, "CH4", 1, DefLibrary);
        }

        {
            DataBlock planet = Data.AddData(system, "Planet", "Outer_System", DefLibrary);
            Data.AddData(planet, "Type", "Outer_System", DefLibrary);
        }

        return system;
    }
}
