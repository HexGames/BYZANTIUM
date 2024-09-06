using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

[Tool]
public partial class GameArgs : Node
{
    [ExportCategory("Links")]
    [Export]
    public DefLibrary Def;
    [Export]
    public MapGenerator MapGen;

    [ExportCategory("Args")]
    [Export]
    public bool GameStartup_DetDefsFromDownloads;
    [Export]
    public bool GameStartup_ReimportDefs;
    [Export]
    public bool GameStartup_Regenerate;


    [ExportCategory("Run Game Args")]
    [Export]
    public bool RunAllGameArgs
    {
        get => false;
        set
        {
            if (value)
            {
                ReadyArgs();
            }
        }
    }

    public void ReadyArgs()
    {
        DefLibrary.self = Def;

        Step_1_LoadAllDefs();
        Step_2_RegenerateMap();
    }

    private void Step_1_LoadAllDefs()
    {
        if (GameStartup_DetDefsFromDownloads)
        {
            string[] allFiles = Directory.GetFiles("C:\\Users\\Vlad\\Downloads");

            List<string> csvFiles = new List<string>();
            for (int idx = 0; idx < allFiles.Length; idx++)
            {
                if (allFiles[idx].EndsWith(".csv"))
                {
                    csvFiles.Add(allFiles[idx]);
                }
            }

            string path = "D:\\_OtherProjects\\_Byzantium\\Byzantium_Godot\\Defs_Mod\\";

            // buildings table
            MoveAndRenameFile(SearchForMostRecentFile(csvFiles, "Building"), path + "Buildings.table");

            // factions table
            MoveAndRenameFile(SearchForMostRecentFile(csvFiles, "Factions"), path + "Factions.table");

            // features table
            MoveAndRenameFile(SearchForMostRecentFile(csvFiles, "Features"), path + "Features.table");

            // shipparts table
            MoveAndRenameFile(SearchForMostRecentFile(csvFiles, "ShipParts"), path + "ShipParts.table");
        }

        if (GameStartup_ReimportDefs)
        {
            Def.LoadBuildingsDefFunc();
            Def.LoadEmpiresDefFunc();
            Def.LoadEthicsDefFunc();
            Def.LoadFactionsDefFunc();
            Def.LoadFeaturesDefFunc();
            Def.LoadPlanetsDefFunc();
            Def.LoadPlanetNamesFunc();
            Def.LoadShipPartsDefFunc();
            Def.LoadSpeciesDefFunc();

            Def.SaveBuildingsDef();
            Def.SaveFactionsDef();
            Def.SaveFeaturesDef();
            Def.SaveShipPartsDef();
        }
    }
    private void Step_2_RegenerateMap()
    {
        if (GameStartup_Regenerate)
        {
            MapGen.GenerateMapFunc();
        }
    }

    private string SearchForMostRecentFile(List<string> files, string subName)
    {
        string file = null;
        
        for (int idx = 0; idx < files.Count; idx++)
        {
            if (files[idx].Contains(subName))
            {
                if (file == null || File.GetCreationTime(files[idx]) > File.GetCreationTime(file))
                {
                    file = files[idx];
                }
            }
        }
        return file;
    }

    private void MoveAndRenameFile(string originalFile, string destination)
    {
        if (!File.Exists(originalFile))
        {
            GD.Print("File " + originalFile + " not found!");
            return;
        }

        if (File.Exists(destination))
        {
            File.Delete(destination);
        }

        File.Copy(originalFile, destination);

        GD.Print("Moved " + originalFile + " to " + destination);
    }
}
