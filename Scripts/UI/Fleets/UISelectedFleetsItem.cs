using Godot;
using Godot.Collections;

//[Tool]
public partial class UISelectedFleetsItem : Control
{
    // is beeing duplicated
    private Panel Unselected = null;
    private Panel Selected = null;
    private UIText FleetName = null;
    private Button FleetPrevious = null;
    private Button FleetNext = null;
    private Button FleetClose = null;
    private UIText FleetType = null;
    private UIText FleetFromSystem = null;
    //private Control FleetExtraBg = null;
    //private UIText FleetExtra = null;
    private Control ShipsTotalBg = null;
    private UIText ShipsTotal = null;
    private UIText ShipsTotalValue = null;
    private Button ShipDetailsBtn = null;
    private TextureRect ShipDetailsArrow = null;
    private Control ShipsBg = null;
    private Array<UISelectedFleetsItemShips> Ships = new Array<UISelectedFleetsItemShips>();
    private UIText FleetStatus = null;

    [ExportCategory("Runtime")]
    [Export]
    public FleetData _Fleet = null;

    public override void _Ready()
    {
        Unselected = GetNode<Panel>("MarginContainer/VBoxContainer/Title/Unselected");
        Selected = GetNode<Panel>("MarginContainer/VBoxContainer/Title/Selected");
        FleetName = GetNode<UIText>("MarginContainer/VBoxContainer/Title/MarginContainer/Control/Name");
        FleetPrevious = GetNode<Button>("MarginContainer/VBoxContainer/Title/MarginContainer/Control/Previous");
        FleetNext = GetNode<Button>("MarginContainer/VBoxContainer/Title/MarginContainer/Control/Next");
        FleetClose = GetNode<Button>("MarginContainer/VBoxContainer/Title/MarginContainer/Control/Close");
        FleetType = GetNode<UIText>("MarginContainer/VBoxContainer/FleetType/MarginContainer/FleetType");
        FleetFromSystem = GetNode<UIText>("MarginContainer/VBoxContainer/FleetType/MarginContainer/FleetType/From");
        //FleetExtraBg = GetNode<Control>("MarginContainer/VBoxContainer/Extra");
        //FleetExtra = GetNode<UIText>("MarginContainer/VBoxContainer/Extra/MarginContainer/Extra");
        ShipsTotalBg = GetNode<Control>("MarginContainer/VBoxContainer/ShipsTotal");
        ShipsTotal = GetNode<UIText>("MarginContainer/VBoxContainer/ShipsTotal/MarginContainer/TotalBg/TotalShips");
        ShipsTotalValue = GetNode<UIText>("MarginContainer/VBoxContainer/ShipsTotal/MarginContainer/TotalBg/TotalPower");
        ShipDetailsBtn = GetNode<Button>("MarginContainer/VBoxContainer/ShipsTotal/MarginContainer/Control/Details");
        ShipDetailsArrow = GetNode<TextureRect>("MarginContainer/VBoxContainer/ShipsTotal/MarginContainer/Control/Details/TextureRect");
        ShipsBg = GetNode<Control>("MarginContainer/VBoxContainer/VBoxContainer");
        Ships.Clear();
        int i = 0;
        while (HasNode("MarginContainer/VBoxContainer/VBoxContainer/Ship_" + i.ToString()))
        { 
            Ships.Add(GetNode<UISelectedFleetsItemShips>("MarginContainer/VBoxContainer/VBoxContainer/Ship_" + i.ToString()));
            i++;
        }
        FleetStatus = GetNode<UIText>("MarginContainer/VBoxContainer/Status/MarginContainer/Control/Status");
    }

    public void Refresh(FleetData fleet, bool only, bool selected, bool details)
    {
        _Fleet = fleet;

        Unselected.Visible = !selected;
        Selected.Visible = selected;
        
        if (only)
        {
            FleetPrevious.Visible = true;
            FleetNext.Visible = true;
            FleetClose.Visible = true;

            StarData star = _Fleet.StarAt_PerTurn;
            var fleetGroup = _Fleet.StarAt_PerTurn.GetFriendlyFleets(Game.self.HumanPlayer);
            if (fleetGroup.Contains(_Fleet) == false) fleetGroup = _Fleet.StarAt_PerTurn.GetNeutralFleets(Game.self.HumanPlayer);
            if (fleetGroup.Contains(_Fleet) == false) fleetGroup = _Fleet.StarAt_PerTurn.GetEnemyFleets(Game.self.HumanPlayer);

            if (fleetGroup.Contains(_Fleet))
            {
                int idx = fleetGroup.IndexOf(_Fleet);
                FleetPrevious.Disabled = idx <= 0;
                FleetNext.Disabled = idx >= fleetGroup.Count - 1;
            }
        }
        else
        {
            FleetPrevious.Visible = false;
            FleetNext.Visible = false;
            FleetClose.Visible = false;
        }

        string fleetType = _Fleet.Data.GetSubValueS("FleetType");
        if (fleetType == "Main")
        {
            FleetName.SetTextWithReplace("$id", Helper.GetColorPrefix_FleetMain() + _Fleet.FleetName + Helper.GetColorSufix(), "$name", _Fleet.GetLongName(), "Fleet.png", "Fleet.png");
            FleetType.SetTextWithReplace("$type", "Main Fleet");
        }
        else if (fleetType == "Colony")
        {
            FleetName.SetTextWithReplace("$id", "", "$name", _Fleet.GetLongName(), "Fleet.png", "FleetColony.png");
            FleetType.SetTextWithReplace("$type", "Colony Ship");
        }
        else if (fleetType == "Defence")
        {
            FleetName.SetTextWithReplace("$id", "", "$name", _Fleet.GetLongName(), "Fleet.png", "FleetDefensive.png");
            FleetType.SetTextWithReplace("$type", "Defence Ships");
        }

        if (fleetType == "Main")
        {
            //FleetExtraBg.Visible = false;
            FleetFromSystem.Visible = false;

            ShipsTotalBg.Visible = true;
            ShipsTotal.SetTextWithReplace("$value", _Fleet.GetTotalShips().ToString(), "$type", _Fleet.Ships.Count > 1 ? "Ships" : _Fleet.Ships[0].Data.GetSubValueS("Design"));
            ShipsTotalValue.SetTextWithReplace("$value", (10 * _Fleet.Ships.Count).ToString());
             
            if (details && _Fleet.Ships.Count > 1)
            {
                ShipsBg.Visible = true;
                ShipDetailsBtn.Visible = false;
                ShipDetailsArrow.FlipV = false;

                // grow
                while (Ships.Count < _Fleet.Ships.Count)
                {
                    UISelectedFleetsItemShips newItem = Ships[0].Duplicate(7) as UISelectedFleetsItemShips;
                    Ships[0].GetParent().AddChild(newItem);
                    Ships.Add(newItem);
                }

                for (int idx = 0; idx < Ships.Count; idx++)
                {
                    if (idx < _Fleet.Ships.Count)
                    {
                        Ships[idx].Refresh(_Fleet.Ships[idx], _Fleet.Ships.Count > 1);
                        Ships[idx].Visible = true;
                    }
                    else
                    {
                        Ships[idx].Visible = false;
                    }
                }
            }
            else
            {
                ShipsBg.Visible = false;
                ShipDetailsBtn.Visible = false;
            }
        }
        else if (fleetType == "Defence")
        {
            //FleetExtraBg.Visible = false;
            FleetFromSystem.Visible = false;

            ShipsTotalBg.Visible = true;
            ShipsTotal.SetTextWithReplace("$value", _Fleet.GetTotalShips().ToString(), "$type", _Fleet.Ships.Count > 1 ? "Ships" : _Fleet.Ships[0].Data.GetSubValueS("Design"));
            ShipsTotalValue.SetTextWithReplace("$value", (10 * _Fleet.Ships.Count).ToString());

            if (details && _Fleet.Ships.Count > 1)
            {
                ShipsBg.Visible = true;
                ShipDetailsBtn.Visible = false;
                ShipDetailsArrow.FlipV = false;

                // grow
                while (Ships.Count < _Fleet.Ships.Count)
                {
                    UISelectedFleetsItemShips newItem = Ships[0].Duplicate(7) as UISelectedFleetsItemShips;
                    Ships[0].GetParent().AddChild(newItem);
                    Ships.Add(newItem);
                }

                for (int idx = 0; idx < Ships.Count; idx++)
                {
                    if (idx < _Fleet.Ships.Count)
                    {
                        Ships[idx].Refresh(_Fleet.Ships[idx], _Fleet.Ships.Count > 1);
                        Ships[idx].Visible = true;
                    }
                    else
                    {
                        Ships[idx].Visible = false;
                    }
                }
            }
            else
            {
                ShipsBg.Visible = false;
                ShipDetailsBtn.Visible = false;
            }
        }
        else if (fleetType == "Colony")
        {
            ShipsTotalBg.Visible = false;
            ShipsBg.Visible = false;
            //FleetExtraBg.Visible = true;

            //FleetExtra.Text = FleetExtra.Original.Replace("$name", "Migration from [b]" + _Fleet.Data.GetSubValueS("FromSystem") + "[/b]");
            FleetFromSystem.Visible = false;
            //FleetFromSystem.Text = FleetFromSystem.Original.Replace("$name", _Fleet.Data.GetSubValueS("FromSystem"));
        }

        if (fleetType != "Defence")
        {
            if (_Fleet.ActionMoveData != null)
            {
                StarData star = Data.GetLinkStarData(_Fleet.ActionMoveData, Game.self.Map.Data);
                FleetStatus.SetTextWithReplace("$value", Helper.GetColorPrefix_Action() + "Jump to " + star.StarName + " in " + _Fleet.GetMoveActionTurns() + Helper.GetIcon("Turn") + Helper.GetColorSufix());
            }
            else
            {
                FleetStatus.SetTextWithReplace("$value", "Idle");
            }
        }
        else
        {
            FleetStatus.SetTextWithReplace("$value", "Defence (cannot move)");
        }
    }

    public void OnDeselect()
    {
        //Game.self.Input.OnDeselectFleet(_Fleet);
        Game.self.UI.Deselect();
    }

    public void OnShowDetails()
    {
        if (Game.self.UIGalaxy.FleetsSelected.ShowDetails == false)
        {
            Game.self.UIGalaxy.FleetsSelected.ShowDetails = true;
        }
        else
        {
            Game.self.UIGalaxy.FleetsSelected.ShowDetails = false;
        }
        Refresh(_Fleet, true, true, true);
    }

    //public void OnNextFleet()
    //{
    //    StarData star = _Fleet.StarAt_PerTurn;
    //    var fleetGroup = _Fleet.StarAt_PerTurn.GetFriendlyFleets(Game.self.HumanPlayer);
    //    if (fleetGroup.Contains(_Fleet) == false) fleetGroup = _Fleet.StarAt_PerTurn.GetNeutralFleets(Game.self.HumanPlayer);
    //    if (fleetGroup.Contains(_Fleet) == false) fleetGroup = _Fleet.StarAt_PerTurn.GetEnemyFleets(Game.self.HumanPlayer);
    //
    //    if (fleetGroup.Contains(_Fleet))
    //    {
    //        int idx = fleetGroup.IndexOf(_Fleet);
    //        idx++;
    //        Game.self.UI.Select(fleetGroup[idx]);
    //        Game.self.UIGalaxy.FleetsSelected.Refresh(null, fleetGroup[idx]);
    //    }
    //}
    //public void OnPreviousFleet()
    //{
    //    StarData star = _Fleet.StarAt_PerTurn;
    //    var fleetGroup = _Fleet.StarAt_PerTurn.GetFriendlyFleets(Game.self.HumanPlayer);
    //    if (fleetGroup.Contains(_Fleet) == false) fleetGroup = _Fleet.StarAt_PerTurn.GetNeutralFleets(Game.self.HumanPlayer);
    //    if (fleetGroup.Contains(_Fleet) == false) fleetGroup = _Fleet.StarAt_PerTurn.GetEnemyFleets(Game.self.HumanPlayer);
    //
    //    if (fleetGroup.Contains(_Fleet))
    //    {
    //        int idx = fleetGroup.IndexOf(_Fleet);
    //        idx--;
    //        Game.self.UI.Select(fleetGroup[idx]);
    //        Game.self.UIGalaxy.FleetsSelected.Refresh(null, fleetGroup[idx]);
    //    }
    //}
}