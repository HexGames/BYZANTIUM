using Godot;
using Godot.Collections;

//[Tool]
public partial class UISelectedFleetsItem : Control
{
    // is beeing duplicated
    private Array<UISelectedFleetsItemShips> Ships = new Array<UISelectedFleetsItemShips>();
    private RichTextLabel FleetNameNumber = null;
    private static string FleetName_Original = "";
    private RichTextLabel TotalShipsNumber = null;
    private static string TotalShipsNumber_Original = "";
    private RichTextLabel TotalShipsPower = null;
    private static string TotalShipsPower_Original = "";
    private Control SupplyBG = null;
    private RichTextLabel Supply = null;
    private static string Supply_Original = "";
    private Control ActionBG = null;
    private RichTextLabel Action = null;
    private static string Action_Original = "";

    [ExportCategory("Runtime")]
    [Export]
    public FleetData _Fleet = null;

    public override void _Ready()
    {
        FleetNameNumber = GetNode<RichTextLabel>("MarginContainer/VBoxContainer/Title/MarginContainer/Control/Name");
        if (FleetName_Original.Length == 0) FleetName_Original = FleetNameNumber.Text;
        TotalShipsNumber = GetNode<RichTextLabel>("MarginContainer/VBoxContainer/Faction/MarginContainer/TotalShips");
        if (TotalShipsNumber_Original.Length == 0) TotalShipsNumber_Original = TotalShipsNumber.Text;
        TotalShipsPower = GetNode<RichTextLabel>("MarginContainer/VBoxContainer/Faction/MarginContainer/TotalPower");
        if (TotalShipsPower_Original.Length == 0) TotalShipsPower_Original = TotalShipsPower.Text;
        SupplyBG = GetNode<Control>("MarginContainer/VBoxContainer/VBoxContainer/Supply");
        Supply = GetNode<RichTextLabel>("MarginContainer/VBoxContainer/VBoxContainer/Supply/MarginContainer/Supply");
        if (Supply_Original.Length == 0) Supply_Original = Supply.Text;
        ActionBG = GetNode<Control>("MarginContainer/VBoxContainer/VBoxContainer/Action");
        Action = GetNode<RichTextLabel>("MarginContainer/VBoxContainer/VBoxContainer/Action/MarginContainer/Control/Name");
        if (Action_Original.Length == 0) Action_Original = Action.Text;

        Ships.Clear();
        for (int i = 0; i < 4; i++)
        {
            Ships.Add(GetNode<UISelectedFleetsItemShips>("MarginContainer/VBoxContainer/VBoxContainer/Ship_" + i.ToString()));
        }
    }

    public void Refresh(FleetData fleet)
    {
        _Fleet = fleet;

        FleetNameNumber.Text = FleetName_Original.Replace("$id", _Fleet.FleetName).Replace("$name", _Fleet.GetLongName());
        TotalShipsNumber.Text = TotalShipsNumber_Original.Replace("$value", _Fleet.Ships.Count.ToString());
        TotalShipsPower.Text = TotalShipsPower_Original.Replace("$value", (10 * _Fleet.Ships.Count).ToString());

        for (int idx = 0; idx < Ships.Count; idx++)
        {
            Ships[idx].Visible = false;
        }


        Supply.Text = Supply_Original.Replace("$sup", fleet.Stats_PerTurn.Supply.ToString()).Replace("$max", fleet.Stats_PerTurn.SupplyMax.ToString());
        SupplyBG.Visible = true;

        if (fleet.ActionData != null)
        {
            StarData star = Data.GetLinkStarData(fleet.ActionData, Game.self.Map.Data);
            Action.Text = Action_Original.Replace("$value", "Jump to " + star.StarName + " in " + fleet.GetMoveActionTurns() + Helper.GetIcon("Turn"));
        }
        else
        {
            Action.Text = Action_Original.Replace("$value", "Idle");
        }
        ActionBG.Visible = true;
    }

    public void OnDeselect()
    {
        Game.self.Input.OnDeselectFleet(_Fleet);
    }
}