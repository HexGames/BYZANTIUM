using Godot;

//[Tool]
public partial class UISelectedFleetsItemShips : Control
{
    // is beeing duplicated
    private UIText ShipsType = null;
    private UIText ShipsPower = null;
    private Button AddShip = null;
    private Button RemoveShip = null;

    // runtime
    public ShipData _Ship = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        ShipsType = GetNode<UIText>("MarginContainer/ShipType");
        ShipsPower = GetNode<UIText>("MarginContainer/Power");

        AddShip = GetNode<Button>("MarginContainer/ShipType/Control/Add"); 
        RemoveShip = GetNode<Button>("MarginContainer/ShipType/Control/Remove");
    }

    public void Refresh(ShipData ship, bool showPower = true)
    {
        _Ship = ship;

        int number = 0;
        string type = ship.Data.GetSubValueS("Design");
        int power = 10;

        ShipsType.SetTextWithReplace("$value", number.ToString(), "$type", type);
        if (showPower)
        {
            ShipsPower.Visible = true;
            ShipsPower.SetTextWithReplace("$value", power.ToString());
        }
        else
        {
            ShipsPower.Visible = false;
        }

        RemoveShip.Visible = false;
        AddShip.Visible = false;
    }

    public void OnAddShip()
    {

    }

    public void OnRemoveShip()
    {

    }
}