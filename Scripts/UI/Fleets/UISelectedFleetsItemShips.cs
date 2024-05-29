using Godot;

//[Tool]
public partial class UISelectedFleetsItemShips : Control
{
    // is beeing duplicated
    private RichTextLabel ShipsNumber = null;
    private static string ShipsNumber_Original = "";
    private RichTextLabel ShipsPower = null;
    private static string ShipsPower_Original = "";
    private Button AddShip = null;
    private Button RemoveShip = null;

    [ExportCategory("Runtime")]
    [Export]
    public DataBlock _Data = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        ShipsNumber = GetNode<RichTextLabel>("MarginContainer/Ships");
        if (ShipsNumber_Original.Length == 0) ShipsNumber_Original = ShipsNumber.Text;
        ShipsPower = GetNode<RichTextLabel>("MarginContainer/Power");
        if (ShipsPower_Original.Length == 0) ShipsPower_Original = ShipsPower.Text;

        AddShip = GetNode<Button>("MarginContainer/Ships/Control/Remove");
        RemoveShip = GetNode<Button>("MarginContainer/Ships/Control/Remove");
    }

    public void Refresh(DataBlock data)
    {
        _Data = data;
    }

    public void OnAddShip()
    {

    }

    public void OnRemoveShip()
    {

    }
}