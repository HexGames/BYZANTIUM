using Godot;
using Godot.Collections;

public partial class UI3DSelectors : Node
{
    [Export]
    private Node3D Fleet_Hover = null;
    [Export]
    private Node3D Fleet_Select = null;
    [Export]
    private Node3D Planet_Hover = null;
    [Export]
    private Node3D Planet_Select = null;

    public override void _Ready()
    {
        Fleet_Hover.Visible = false;
        Fleet_Select.Visible = false;
        Planet_Hover.Visible = false;
        Planet_Select.Visible = false;
    }

    public void FleetHover(Array<FleetData> fleets)
    {
        if (fleets.Count == 0)
            return;

        if (fleets[0].StarAt_PerTurn._Node.GFX.ShipFriendly._Fleets.Contains(fleets[0]))
        {
            Fleet_Hover.Position = fleets[0].StarAt_PerTurn._Node.GFX.ShipFriendly.GlobalPosition;
        }
        else
        {
            Fleet_Hover.Position = fleets[0].StarAt_PerTurn._Node.GFX.ShipOther.GlobalPosition;
        }
        Fleet_Hover.Visible = true;
    }

    public void FleetDehover()
    {
        Fleet_Hover.Visible = false;
    }

    public void FleetSelect(FleetData fleet)
    {
        if (fleet.StarAt_PerTurn._Node.GFX.ShipFriendly._Fleets.Contains(fleet))
        {
            Fleet_Select.Position = fleet.StarAt_PerTurn._Node.GFX.ShipFriendly.GlobalPosition;
        }
        else
        {
            Fleet_Select.Position = fleet.StarAt_PerTurn._Node.GFX.ShipOther.GlobalPosition;
        }
        Fleet_Select.Visible = true;
    }

    public void FleetSelect(Array<FleetData> fleets)
    {
        if (fleets.Count == 0)
            return;

        if (fleets[0].StarAt_PerTurn._Node.GFX.ShipFriendly._Fleets.Contains(fleets[0]))
        {
            Fleet_Select.Position = fleets[0].StarAt_PerTurn._Node.GFX.ShipFriendly.GlobalPosition;
        }
        else
        {
            Fleet_Select.Position = fleets[0].StarAt_PerTurn._Node.GFX.ShipOther.GlobalPosition;
        }
        Fleet_Select.Visible = true;
    }

    public void FleetDeselect()
    {
        Fleet_Select.Visible = false;
    }

    public void PlanetHover(PlanetData planet)
    {
        Planet_Hover.Position = planet.GFX.Planet.GlobalPosition;
        if (planet.Data.HasSub("Size"))
        {
            Planet_Hover.Scale = (0.24f + planet.Data.GetSub("Size").ValueI * 0.066f) * Vector3.One;
        }
        else
        {
            Planet_Hover.Scale = 0.306f * Vector3.One;
        }
        Planet_Hover.Visible = true;
    }

    public void PlanetDehover()
    {
        Planet_Hover.Visible = false;
    }

    public void PlanetSelect(PlanetData planet)
    {
        Planet_Select.Position = planet.GFX.Planet.GlobalPosition;
        if (planet.Data.HasSub("Size"))
        {
            Planet_Select.Scale = (0.24f + planet.Data.GetSub("Size").ValueI * 0.066f) * Vector3.One;
        }
        else
        {
            Planet_Select.Scale = 0.306f * Vector3.One;
        }
        Planet_Select.Visible = true;
    }

    public void PlanetDeselect()
    {
        Planet_Select.Visible = false;
    }
}