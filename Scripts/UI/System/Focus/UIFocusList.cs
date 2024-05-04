using Godot;

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
    public UIFocusListIcon All_Energy = null;
    [Export]
    public UIFocusListIcon All_Minerals = null;
    [Export]
    public UIFocusListIcon All_Production = null;
    [Export]
    public UIFocusListIcon All_Shipbuilding = null;
    [Export]
    public UIFocusListIcon All_Trade = null;
    [Export]
    public UIFocusListIcon All_TechPoints = null;
    [Export]
    public UIFocusListIcon All_CulturePoints = null;
    [Export]
    public UIFocusListIcon All_Authority = null;
    [Export]
    public UIFocusListIcon All_Influence = null;
    [Export]
    public UIFocusListIcon All_BC = null;
    [Export]
    public UITooltipTrigger All_Pops_Tooltip = null;
    private string All_Pops_Tooltip_Original = null;

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

        All_Pops_Tooltip_Original = All_Pops_Tooltip.Row_1_Right;
    }

    public void Refresh(ColonyData colony)
    {
        return;
        TitleLabel.Text = TitleLabel_Original.Replace("$name", colony.ColonyName).Replace("$value", Helper.ResValueToString(colony.Resources_PerTurn.Get("Pops").Value_2, 1000));

        var energy = colony.Jobs_PerTurn.Get("Energy");
        Energy.Refresh(energy);
        All_Energy.Refresh(energy);

        var minerals = colony.Jobs_PerTurn.Get("Minerals");
        Minerals.Refresh(minerals);
        All_Minerals.Refresh(minerals);

        var prod = colony.Jobs_PerTurn.Get("Production");
        Production.Refresh(prod);
        All_Production.Refresh(prod);

        var ship = colony.Jobs_PerTurn.Get("Shipbuilding");
        Shipbuilding.Refresh(ship);
        All_Shipbuilding.Refresh(ship);

        var trade = colony.Jobs_PerTurn.Get("Trade");
        Trade.Refresh(trade);
        All_Trade.Refresh(trade);

        var tech = colony.Jobs_PerTurn.Get("TechPoints");
        TechPoints.Refresh(tech);
        All_TechPoints.Refresh(tech);

        var culture = colony.Jobs_PerTurn.Get("CulturePoints");
        CulturePoints.Refresh(culture);
        All_CulturePoints.Refresh(culture);

        var authority = colony.Jobs_PerTurn.Get("Authority");
        Authority.Refresh(authority);
        All_Authority.Refresh(authority);

        var influence = colony.Jobs_PerTurn.Get("Influence");
        Influence.Refresh(influence);
        All_Influence.Refresh(influence);

        var bc = colony.Jobs_PerTurn.Get("BC");
        BC.Refresh(bc);
        All_BC.Refresh(bc);

        All.Refresh(colony.Jobs_PerTurn.AllFocusValue, colony.Jobs_PerTurn.AllFocusChange);

        All_Pops_Tooltip.Row_1_Right = All_Pops_Tooltip_Original.Replace("$value", Helper.ResValueToString(colony.Jobs_PerTurn.AllTotalPops, 1000));

        Visible = true;
    }
}