using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Transactions;
using System.Xml;

// Editor
public partial class MapGenerator : Node
{
    private void GenerateNewMapSave_Stars_Planets_Sol(DataBlock planetList)
    {
        DataBlock star = Data.AddData(planetList, "Planet", "Star", DefLibrary);
        Data.AddData(star, "Star_Type", "Main_Sequence", DefLibrary);
        Data.AddData(star, "Features", DefLibrary);

        DataBlock planet = Data.AddData(planetList, "Planet", "Mercury", DefLibrary);
        Data.AddData(planet, "Type", "Barren", DefLibrary);
        Data.AddData(planet, "Size", 1, DefLibrary);
        Data.AddData(planet, "Temperature", 5, DefLibrary);
        Data.AddData(planet, "Uninhabitable", DefLibrary);
        Data.AddData(planet, "SlotType", "Outpost", DefLibrary);

        Data.AddData(planet, "Features", DefLibrary);

        // ---
        planet = Data.AddData(planetList, "Planet", "Venus", DefLibrary);
        Data.AddData(planet, "Type", "Toxic", DefLibrary);
        Data.AddData(planet, "Size", 3, DefLibrary);
        Data.AddData(planet, "Temperature", 4, DefLibrary);
        Data.AddData(planet, "Uninhabitable", DefLibrary);
        Data.AddData(planet, "SlotType", "Outpost", DefLibrary);

        DataBlock featuresData = Data.AddData(planet, "Features", DefLibrary);
        if (RNG.RandiRange(0,1) == 0) Data.AddData(featuresData, "Refueling_Gasses", DefLibrary);

        // ---
        planet = Data.AddData(planetList, "Planet", "Terra", DefLibrary);
        Data.AddData(planet, "Type", "Continents", DefLibrary);
        Data.AddData(planet, "Size", 3, DefLibrary);
        Data.AddData(planet, "Temperature", 3, DefLibrary);
        Data.AddData(planet, "SlotType", "District", DefLibrary);

        featuresData = Data.AddData(planet, "Features", DefLibrary);
        Data.AddData(featuresData, "Medium", DefLibrary);
        if (RNG.RandiRange(0, 1) == 0) Data.AddData(featuresData, "Wet", DefLibrary);
        else Data.AddData(featuresData, "Fertile", DefLibrary);

        // ---
        planet = Data.AddData(planetList, "Planet", "Moon", DefLibrary);
        Data.AddData(planet, "Type", "Barren", DefLibrary);
        Data.AddData(planet, "Size", 1, DefLibrary);
        Data.AddData(planet, "Temperature", 3, DefLibrary);
        Data.AddData(planet, "SlotType", "Outpost", DefLibrary);

        Data.AddData(planet, "Features", DefLibrary);
        Data.AddData(planet, "Moon", DefLibrary);

        // ---
        planet = Data.AddData(planetList, "Planet", "Mars", DefLibrary);
        Data.AddData(planet, "Type", "Desert", DefLibrary);
        Data.AddData(planet, "Size", 1, DefLibrary);
        Data.AddData(planet, "Temperature", 3, DefLibrary);
        Data.AddData(planet, "SlotType", "District", DefLibrary);

        featuresData = Data.AddData(planet, "Features", DefLibrary);
        Data.AddData(featuresData, "Small", DefLibrary);
        Data.AddData(featuresData, "Desert", DefLibrary);

        // ---
        planet = Data.AddData(planetList, "Planet", "Asteroids", DefLibrary);
        Data.AddData(planet, "Type", "Asteroids", DefLibrary);
        Data.AddData(planet, "Temperature", 3, DefLibrary);
        Data.AddData(planet, "SlotType", "Asteroid_Base", DefLibrary);

        featuresData = Data.AddData(planet, "Features", DefLibrary);
        if (RNG.RandiRange(0, 1) == 0) Data.AddData(featuresData, "Rich_Minerals", DefLibrary);

        // ---
        planet = Data.AddData(planetList, "Planet", "Jupiter", DefLibrary);
        Data.AddData(planet, "Type", "Gas_Giant", DefLibrary);
        Data.AddData(planet, "Size", 6, DefLibrary);
        Data.AddData(planet, "Temperature", 2, DefLibrary);
        Data.AddData(planet, "SlotType", "Space_Station", DefLibrary);

        featuresData = Data.AddData(planet, "Features", DefLibrary);
        if (RNG.RandiRange(0, 1) == 0) Data.AddData(featuresData, "High_Enegy_Particles", DefLibrary);

        // ---
        planet = Data.AddData(planetList, "Planet", "Ganymede", DefLibrary);
        Data.AddData(planet, "Type", "Barren", DefLibrary);
        Data.AddData(planet, "Size", 1, DefLibrary);
        Data.AddData(planet, "Temperature", 2, DefLibrary);
        Data.AddData(planet, "SlotType", "Outpost", DefLibrary);

        featuresData = Data.AddData(planet, "Features", DefLibrary);
        Data.AddData(planet, "Moon", DefLibrary);

        // ---
        planet = Data.AddData(planetList, "Planet", "Saturn", DefLibrary);
        Data.AddData(planet, "Type", "Gas_Giant", DefLibrary);
        Data.AddData(planet, "Size", 5, DefLibrary);
        Data.AddData(planet, "Temperature", 1, DefLibrary);
        Data.AddData(planet, "SlotType", "Space_Station", DefLibrary);

        featuresData = Data.AddData(planet, "Features", DefLibrary);
        if (RNG.RandiRange(0, 1) == 0) Data.AddData(featuresData, "High_Enegy_Particles", DefLibrary);
        Data.AddData(planet, "Rings", DefLibrary);

        // ---
        planet = Data.AddData(planetList, "Planet", "Titan", DefLibrary);
        Data.AddData(planet, "Type", "Frozen", DefLibrary);
        Data.AddData(planet, "Size", 1, DefLibrary);
        Data.AddData(planet, "Temperature", 1, DefLibrary);
        Data.AddData(planet, "SlotType", "Outpost", DefLibrary);

        featuresData = Data.AddData(planet, "Features", DefLibrary);
        if (RNG.RandiRange(0, 1) == 0) Data.AddData(featuresData, "High_Enegy_Particles", DefLibrary);
        Data.AddData(planet, "Moon", DefLibrary);
    }
}
