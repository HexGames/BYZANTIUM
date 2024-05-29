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

    [ExportCategory("Runtime")]
    [Export]
    public FleetData _Fleet = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        FleetNameNumber = GetNode<RichTextLabel>("MarginContainer/VBoxContainer/Title/MarginContainer/Control/Name");
        if (FleetName_Original.Length == 0) FleetName_Original = FleetNameNumber.Text;
        TotalShipsNumber = GetNode<RichTextLabel>("MarginContainer/VBoxContainer/Faction/MarginContainer/TotalShips");
        if (TotalShipsNumber_Original.Length == 0) TotalShipsNumber_Original = TotalShipsNumber.Text;
        TotalShipsPower = GetNode<RichTextLabel>("MarginContainer/VBoxContainer/Faction/MarginContainer/TotalPower");
        if (TotalShipsPower_Original.Length == 0) TotalShipsPower_Original = TotalShipsPower.Text;

        Ships.Clear();
        for (int i = 0; i < 4; i++)
        {
            Ships.Add(GetNode<UISelectedFleetsItemShips>("MarginContainer/VBoxContainer/VBoxContainer/Ship_" + i.ToString()));
        }
    }

    public void Refresh(FleetData fleet)
    {
        _Fleet = fleet;

        FleetNameNumber.Text = FleetName_Original.Replace("$value", _Fleet.FleetName);
        TotalShipsNumber.Text = TotalShipsNumber_Original.Replace("$value", _Fleet.Ships.Count.ToString());
        TotalShipsPower.Text = TotalShipsPower_Original.Replace("$value", (10 * _Fleet.Ships.Count).ToString());


        for (int idx = 0; idx < Ships.Count; idx++)
        {
            Ships[idx].Visible = false;
        }
    }

    public void OnDeselect()
    {
        Game.Input.DeselectFleet(_Fleet);
    }
}