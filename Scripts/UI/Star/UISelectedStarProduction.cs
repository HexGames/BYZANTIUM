using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UISelectedStarProduction : Control
{
    [ExportCategory("Links")]
    [Export]
    public Control ColonizeBg = null;
    [Export]
    public RichTextLabel ColonizeFocus = null;
    private static string ColonizeFocus_Original = "";
    [Export]
    public Control ColonizeProject = null;
    [Export]
    public TextureRect ColonizePlanetTex = null;
    [Export]
    public RichTextLabel ColonizePlanetName = null;
    private static string ColonizePlanetName_Original = "";
    [Export]
    public TextureRect ColonizeBuildingTex = null;
    [Export]
    public RichTextLabel ColonizeBuildingName = null;
    private static string ColonizeBuildingName_Original = "";
    [Export]
    private ProgressBar ColonizeProgressCurrent = null;
    [Export]
    private ProgressBar ColonizeProgressNextTurn = null;
    [Export]
    public RichTextLabel ColonizeProgressValue = null;
    private static string ColonizeProgressValue_Original = "";
    [Export]
    public Control ColonizeAlert = null;
    [Export]
    public Control ColonizeAlertPlanet = null;
    [Export]
    public Control ColonizeAlertProject = null;
    [Export]
    public Control ColonizeBonusBg = null;
    [Export]
    public RichTextLabel ColonizeBonus = null;
    private static string ColonizeBonus_Original = "";

    [Export]
    public Control FactoriesBg = null;
    [Export]
    public RichTextLabel FactoriesFocus = null;
    private static string FactoriesFocus_Original = "";
    [Export]
    public RichTextLabel FactoriesResults = null;
    private static string FactoriesResults_Original = "";
    [Export]
    public Control FactoriesBonusBg = null;
    [Export]
    public RichTextLabel FactoriesBonus = null;
    private static string FactoriesBonus_Original = "";

    [Export]
    public Control ResearchBg = null;
    [Export]
    public RichTextLabel ResearchFocus = null;
    private static string ResearchFocus_Original = "";
    [Export]
    public RichTextLabel ResearchResults = null;
    private static string ResearchResults_Original = "";
    [Export]
    public Control ResearchBonusBg = null;
    [Export]
    public RichTextLabel ResearchBonus = null;
    private static string ResearchBonus_Original = "";

    [Export]
    public Control CultureBg = null;
    [Export]
    public RichTextLabel CultureFocus = null;
    private static string CultureFocus_Original = "";
    [Export]
    public RichTextLabel CultureResults = null;
    private static string CultureResults_Original = "";
    [Export]
    public Control CultureBonusBg = null;
    [Export]
    public RichTextLabel CultureBonus = null;
    private static string CultureBonus_Original = "";

    [Export]
    public Control ShipsBg = null;
    [Export]
    public RichTextLabel ShipsFocus = null;
    private static string ShipsFocus_Original = "";
    [Export]
    public TextureRect ShipsTex = null;
    [Export]
    public RichTextLabel ShipsName = null;
    private static string ShipsName_Original = "";
    [Export]
    private ProgressBar ShipsProgressCurrent = null;
    [Export]
    private ProgressBar ShipsProgressNextTurn = null;
    [Export]
    public RichTextLabel ShipsProgressValue = null;
    private static string ShipsProgressValue_Original = "";
    [Export]
    public Control ShipsBonusBg = null;
    [Export]
    public RichTextLabel ShipsBonus = null;
    private static string ShipsBonus_Original = "";

    [Export]
    public Control BasesBg = null;
    [Export]
    public RichTextLabel BasesFocus = null;
    private static string BasesFocus_Original = "";
    [Export]
    public RichTextLabel BasesResults = null;
    private static string BasesResults_Original = "";
    [Export]
    public Control BasesBonusBg = null;
    [Export]
    public RichTextLabel BasesBonus = null;
    private static string BasesBonus_Original = "";

    [ExportCategory("Runtime")]
    [Export]
    public SystemData _System = null;

    public override void _Ready()
    {
        if (ColonizeFocus_Original.Length == 0) ColonizeFocus_Original = ColonizeFocus.Text;
        if (ColonizePlanetName_Original.Length == 0) ColonizePlanetName_Original = ColonizePlanetName.Text;
        if (ColonizeBuildingName_Original.Length == 0) ColonizeBuildingName_Original = ColonizeBuildingName.Text;
        if (ColonizeProgressValue_Original.Length == 0) ColonizeProgressValue_Original = ColonizeProgressValue.Text;
        if (ColonizeBonus_Original.Length == 0) ColonizeBonus_Original = ColonizeBonus.Text;

        if (FactoriesFocus_Original.Length == 0) FactoriesFocus_Original = FactoriesFocus.Text;
        if (FactoriesResults_Original.Length == 0) FactoriesResults_Original = FactoriesResults.Text;
        if (FactoriesBonus_Original.Length == 0) FactoriesBonus_Original = FactoriesBonus.Text;

        if (ResearchFocus_Original.Length == 0) ResearchFocus_Original = ResearchFocus.Text;
        if (ResearchResults_Original.Length == 0) ResearchResults_Original = ResearchResults.Text;
        if (ResearchBonus_Original.Length == 0) ResearchBonus_Original = ResearchBonus.Text;

        if (CultureFocus_Original.Length == 0) CultureFocus_Original = CultureFocus.Text;
        if (CultureResults_Original.Length == 0) CultureResults_Original = CultureResults.Text;
        if (CultureBonus_Original.Length == 0) CultureBonus_Original = CultureBonus.Text;

        if (ShipsFocus_Original.Length == 0) ShipsFocus_Original = ShipsFocus.Text;
        if (ShipsName_Original.Length == 0) ShipsName_Original = ShipsName.Text;
        if (ShipsProgressValue_Original.Length == 0) ShipsProgressValue_Original = ShipsProgressValue.Text;
        if (ShipsBonus_Original.Length == 0) ShipsBonus_Original = ShipsBonus.Text;

        if (BasesFocus_Original.Length == 0) BasesFocus_Original = BasesFocus.Text;
        if (BasesResults_Original.Length == 0) BasesResults_Original = BasesResults.Text;
        if (BasesBonus_Original.Length == 0) BasesBonus_Original = BasesBonus.Text;
    }

    public void Refresh(SystemData system)
    {
        _System = system;

        // ---
        ColonizeFocus.Text = ColonizeFocus_Original.Replace("$v", "10");
        if (false)
        {
            ColonizeBonusBg.Visible = true;
            ColonizeBonus.Text = ColonizeBonus_Original.Replace("$b", "33");
        }
        else
        {
            ColonizeBonusBg.Visible = false;
        }

        // ---
        FactoriesFocus.Text = FactoriesFocus_Original.Replace("$v", "10");
        FactoriesResults.Text = FactoriesResults_Original.Replace("$v", "1").Replace("$t", "2");
        if (false)
        {
            FactoriesBonusBg.Visible = true;
            FactoriesBonus.Text = FactoriesBonus_Original.Replace("$b", "33");
        }
        else
        {
            FactoriesBonusBg.Visible = false;
        }

        // ---
        ResearchFocus.Text = ResearchFocus_Original.Replace("$v", "10");
        ResearchResults.Text = ResearchResults_Original.Replace("$val", "13");
        if (false)
        {
            ResearchBonusBg.Visible = true;
            ResearchBonus.Text = ResearchBonus_Original.Replace("$b", "33");
        }
        else
        {
            ResearchBonusBg.Visible = false;
        }

        // ---
        CultureFocus.Text = CultureFocus_Original.Replace("$v", "10");
        CultureResults.Text = CultureResults_Original.Replace("$val", "13");
        if (false)
        {
            CultureBonusBg.Visible = true;
            CultureBonus.Text = CultureBonus_Original.Replace("$b", "33");
        }
        else
        {
            CultureBonusBg.Visible = false;
        }

        // ---
        ShipsFocus.Text = ShipsFocus_Original.Replace("$v", "10");
        if (false)
        {
            ShipsBonusBg.Visible = true;
            ShipsBonus.Text = ShipsBonus_Original.Replace("$b", "33");
        }
        else
        {
            ShipsBonusBg.Visible = false;
        }

        // ---
        BasesBg.Visible = false;
        //BasesFocus.Text = BasesFocus_Original.Replace("$v", "10");
        //BasesResults.Text = BasesResults_Original.Replace("$v", "1").Replace("$t", "2");
        //if (false)
        //{
        //    BasesBonusBg.Visible = true;
        //    BasesBonus.Text = BasesBonus_Original.Replace("$b", "33");
        //}
        //else
        //{
        //    BasesBonusBg.Visible = false;
        //}
    }

    public void OnFocusColonize()
    {
    }

    public void OnFocusFactories()
    {
    }

    public void OnFocusResearch()
    {
    }

    public void OnFocusCulture()
    {
    }

    public void OnFocusShips()
    {
    }

    public void OnChangeShipLeft()
    {
    }

    public void OnChangeShipRight()
    {
    }

    public void OnFocusBases()
    {
    }
}