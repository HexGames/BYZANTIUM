using Godot;
using Godot.Collections;

//[Tool]
public partial class UISelectedFleets : Control
{
    /*
    // was duplicated in the past...
    [Export]
    public Button Fleet_1;
    [Export]
    public Button Fleet_2;
    [Export]
    public Button Fleet_3;
    [Export]
    public Button Fleet_4;
    [Export]
    public Button Fleet_5;
    [Export]
    public Button MoreFleets;
    [Export]
    public RichTextLabel MoreFleetsText = null;
    private static string MoreFleetsText_Original = "";*/
    // is beeing duplicated
    [Export]
    public Array<UISelectedFleetsItem> Fleets = new Array<UISelectedFleetsItem>();

    [ExportCategory("Runtime")]
    [Export]
    public Array<FleetData> _FleetList = null;
    public FleetData _Fleet = null;

    public bool ShowDetails = false;

    private bool Refreshed = false;
    public void NeedsRefresh()
    {
        Refreshed = false;
        if (Visible) Refresh(_FleetList, _Fleet);
    }

    //public void Refresh(StarData star)
    //{
    //    if (star == null) return;
    //    Visible = true;
    //    if (_Star == star && Refreshed) return;
    //
    //    _Star = star;
    //    _System = _Star.System;
    //    Refreshed = true;

    public void RefreshFleet(FleetData fleet)
    {
        if (fleet.StarAt_PerTurn._Node.GFX.ShipFriendly._Fleets.Contains(fleet))
        {
            Refresh(fleet.StarAt_PerTurn._Node.GFX.ShipFriendly._Fleets, fleet);
        }
        else
        {
            Refresh(fleet.StarAt_PerTurn._Node.GFX.ShipOther._Fleets, fleet);
        }
    }

    public void RefreshFleets(Array<FleetData> fleetList)
    {
        Refresh(fleetList, null);
    }

    //public void RefreshConflict(Array<FleetData> fleetList)
    //{

    //}

    private void Refresh(Array<FleetData> selectedFleetList, FleetData selectedFleet)
    {
        if (selectedFleetList != null && selectedFleetList.Count == 0) return;
        Visible = true;
        //if (_Fleet == selectedFleet && Refreshed) return;

        if (_Fleet != selectedFleet) ShowDetails = false;
        _FleetList = selectedFleetList;
        _Fleet = selectedFleet; 
        Refreshed = true;

        if (_FleetList != null)
        {
            // grow
            while (Fleets.Count < _FleetList.Count)
            {
                UISelectedFleetsItem newItem = Fleets[0].Duplicate(7) as UISelectedFleetsItem;
                Fleets[0].GetParent().AddChild(newItem);
                Fleets.Add(newItem);
            }

            for (int idx = 0; idx < Fleets.Count; idx++)
            {
                if (idx < _FleetList.Count)
                {
                    Fleets[idx].Refresh(_FleetList[idx], false, _FleetList[idx] == _Fleet, false);
                    Fleets[idx].Visible = true;
                }
                else
                {
                    Fleets[idx].Visible = false;
                }
            }
        }
        else
        {

            for (int idx = 0; idx < Fleets.Count; idx++)
            {
                if (idx == 0)
                {
                    Fleets[idx].Refresh(_Fleet, true, true, true);
                    Fleets[idx].Visible = true;
                }
                else
                {
                    Fleets[idx].Visible = false;
                }
            }
        }
    }
}