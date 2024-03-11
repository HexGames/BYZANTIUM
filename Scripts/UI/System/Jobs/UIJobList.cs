using Godot;
using Godot.Collections;
using System.ComponentModel;

public partial class UIJobList : Control
{
    [ExportCategory("Links")]
    [Export]
    private RichTextLabel TitleLabel = null;
    private string TitleLabel_Original = null;
    //[Export]
    //public Array<UIJobListItem> Properties = new Array<UIJobListItem>();
    [Export]
    public UIJobListItem Energy = null;
    [Export]
    public UIJobListItem Minerals = null;
    [Export]
    public UIJobListItem Production = null;
    [Export]
    public UIJobListItem Shipbuilding = null;
    [Export]
    public UIJobListItem Trade = null;
    [Export]
    public UIJobListItem TechPoints = null;
    [Export]
    public UIJobListItem CulturePoints = null;
    [Export]
    public UIJobListItem Authority = null;
    [Export]
    public UIJobListItem Influence = null;
    [Export]
    public UIJobListItem BC = null;

    [Export]
    public RichTextLabel Next = null;
    private string Next_Original;

    [ExportCategory("Runtime")]
    [Export]
    public ColonyData _Colony = null;
    //[Export]
    //public DataBlock _Layout = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        TitleLabel_Original = TitleLabel.Text;
        Next_Original = Next.Text;
    }

    public void Refresh(ColonyData colony)
    {
        TitleLabel.Text = TitleLabel_Original.Replace("$name", colony.ColonyName).Replace("$value", Helper.ResValueToString(colony.Resources_PerTurn.Get("Pops").Value_2, 1000));

        Energy.Refresh(colony.Jobs_PerTurn.Get("Energy"));
        Minerals.Refresh(colony.Jobs_PerTurn.Get("Minerals"));
        Production.Refresh(colony.Jobs_PerTurn.Get("Production"));
        Shipbuilding.Refresh(colony.Jobs_PerTurn.Get("Shipbuilding"));
        Trade.Refresh(colony.Jobs_PerTurn.Get("Trade"));
        TechPoints.Refresh(colony.Jobs_PerTurn.Get("TechPoints"));
        CulturePoints.Refresh(colony.Jobs_PerTurn.Get("CulturePoints"));
        Authority.Refresh(colony.Jobs_PerTurn.Get("Authority"));
        Influence.Refresh(colony.Jobs_PerTurn.Get("Influence"));
        BC.Refresh(colony.Jobs_PerTurn.Get("BC"));
    }
}