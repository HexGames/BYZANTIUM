using Godot;
using System;

// Generated
public partial class PlayerInput : Node
{
    [Export]
    public LocationNode SelectedLocation = null;
   
    public void SelectLocation( LocationNode newSelectedLocation )
    {
        if (SelectedLocation == newSelectedLocation)
        {
            // on reselect
            GD.Print("PlayerInput - reselecteed " + SelectedLocation.Def.LocationName);

            return;
        }

        if (SelectedLocation != null)
        {
            DeselectLocation();
        }
        SelectedLocation = newSelectedLocation;

        // on select
        GD.Print("PlayerInput - selected " + SelectedLocation.Def.LocationName);
    }

    public void DeselectLocation()
    {
        // on deselect
        GD.Print("PlayerInput - deselected " + SelectedLocation.Def.LocationName);

        SelectedLocation = null;
    }
}
