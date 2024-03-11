using Godot;
using Godot.Collections;
using System.ComponentModel;

public partial class UIFocusList : Control
{
    [ExportCategory("Links")]
    [Export]
    private RichTextLabel TitleLabel = null;
    private string TitleLabel_Original = null;
    [Export]
    public UIFocusListItem Energy = null;
    [Export]
    public UIFocusListItem Minerals = null;
    [Export]
    public UIFocusListItem Production = null;
    [Export]
    public UIFocusListItem Shipbuilding = null;
    [Export]
    public UIFocusListItem Trade = null;
    [Export]
    public UIFocusListItem TechPoints = null;
    [Export]
    public UIFocusListItem CulturePoints = null;
    [Export]
    public UIFocusListItem Authority = null;
    [Export]
    public UIFocusListItem Influence = null;
    [Export]
    public UIFocusListItem BC = null;
    [Export]
    public UIFocusListItem All = null;
    [Export]
    public Control All_Energy = null;
    [Export]
    public Control All_Minerals = null;
    [Export]
    public Control All_Production = null;
    [Export]
    public Control All_Shipbuilding = null;
    [Export]
    public Control All_Trade = null;
    [Export]
    public Control All_TechPoints = null;
    [Export]
    public Control All_CulturePoints = null;
    [Export]
    public Control All_Authority = null;
    [Export]
    public Control All_Influence = null;
    [Export]
    public Control All_BC = null;

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
    }

    public void Refresh(ColonyData colony)
    {
        TitleLabel.Text = TitleLabel_Original.Replace("$name", colony.ColonyName).Replace("$value", Helper.ResValueToString(colony.Resources_PerTurn.Get("Pops").Value_2, 1000));

        bool energy = Energy.Refresh(colony.Jobs_PerTurn.Get("Energy"));
        bool minerals = Minerals.Refresh(colony.Jobs_PerTurn.Get("Minerals"));
        bool prod = Production.Refresh(colony.Jobs_PerTurn.Get("Production"));
        bool ship = Shipbuilding.Refresh(colony.Jobs_PerTurn.Get("Shipbuilding"));
        bool trade = Trade.Refresh(colony.Jobs_PerTurn.Get("Trade"));
        bool tech = TechPoints.Refresh(colony.Jobs_PerTurn.Get("TechPoints"));
        bool culture = CulturePoints.Refresh(colony.Jobs_PerTurn.Get("CulturePoints"));
        bool authority = Authority.Refresh(colony.Jobs_PerTurn.Get("Authority"));
        bool influence = Influence.Refresh(colony.Jobs_PerTurn.Get("Influence"));
        bool bc = BC.Refresh(colony.Jobs_PerTurn.Get("BC"));

        All.Refresh(colony.Jobs_PerTurn.AllFocusValue, colony.Jobs_PerTurn.AllFocusChange);

        All_Energy.Visible = !energy;
        All_Minerals.Visible = !minerals;
        All_Production.Visible = !prod;
        All_Shipbuilding.Visible = !ship;
        All_Trade.Visible = !trade;
        All_TechPoints.Visible = !tech;
        All_CulturePoints.Visible = !culture;
        All_Authority.Visible = !authority;
        All_Influence.Visible = !influence;
        All_BC.Visible = !bc;
    }
}