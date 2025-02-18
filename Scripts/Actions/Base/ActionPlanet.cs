using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class ActionPlanet : ActionBase
{
    public SystemData System = null;
    public PlanetData Planet = null;

    public static bool HasActionForPlanet<T>(List<T> possibleActions, PlanetData planet) where T : ActionPlanet
    {
        for (int idx = 0; idx < possibleActions.Count; idx++)
        {
            if (possibleActions[idx].Planet == planet)
            {
                return true;
            }
        }
        return false;
    }
}