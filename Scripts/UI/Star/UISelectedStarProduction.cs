using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UISelectedStarProduction : Control
{
    [ExportCategory("Links")]

    // --- Districts --- Focus
    [Export]
    public Control ColonizeBg = null;
    [Export]
    public RichTextLabel ColonizeFocus = null;
    private static string ColonizeFocus_Original = "";
    [Export]
    public Control ColonizeFocusPip = null;
    [Export]
    public UITooltipTrigger ColonizeFocusToolTip = null;

    // --- Districts ---
    [Export]
    public Control DistrictsProject = null;
    [Export]
    public TextureRect DistrictsPlanetTex = null;
    //[Export]
    //public RichTextLabel DistrictsPlanetName = null;
    //private static string DistrictsPlanetName_Original = "";
    [Export]
    public TextureRect DistrictsBuildingTex = null;
    [Export]
    public RichTextLabel DistrictsBuildingName = null;
    private static string DistrictsBuildingName_Original = ""; 
    [Export]
    private ProgressBar DistrictsProgressCurrent = null;
    [Export]
    private ProgressBar DistrictsProgressNextTurn = null;
    [Export]
    public RichTextLabel DistrictsProgressValue = null;
    private static string DistrictsProgressValue_Original = "";
    [Export]
    public UITooltipTrigger DistrictsToolTip = null;
    [Export]
    public Control DistrictsAlert = null;
    [Export]
    public Control DistrictsAlertPlanet = null;
    [Export]
    public Control DistrictsAlertProject = null;

    // --- Districts --- Bonus
    [Export]
    public Control ColonizeBonusBg = null;
    [Export]
    public RichTextLabel ColonizeBonus = null;
    private static string ColonizeBonus_Original = "";
    [Export]
    public UITooltipTrigger ColonizeBonusToolTip = null;

    // --- Factories --- Focus
    [Export]
    public Control FactoriesBg = null;
    [Export]
    public Button FactoriesFocusBtn = null;
    [Export]
    public RichTextLabel FactoriesFocus = null;
    private static string FactoriesFocus_Original = "";
    [Export]
    public Control FactoriesFocusPip = null;
    [Export]
    public UITooltipTrigger FactoriesFocusToolTip = null;

    // --- Factories ---
    [Export]
    public RichTextLabel FactoriesResults = null;
    private static string FactoriesResults_Original = "";
    [Export]
    public UIText FactoriesMax = null;
    //public RichTextLabel FactoriesMax = null;
    //private static string FactoriesMax_Original = "";
    [Export]
    public UITooltipTrigger FactoriesToolTip = null;

    // --- Factories --- Bonus
    [Export]
    public Control FactoriesBonusBg = null;
    [Export]
    public RichTextLabel FactoriesBonus = null;
    private static string FactoriesBonus_Original = "";
    [Export]
    public UITooltipTrigger FactoriesBonusToolTip = null;

    // --- Research --- Focus
    [Export]
    public Control ResearchBg = null;
    [Export]
    public RichTextLabel ResearchFocus = null;
    private static string ResearchFocus_Original = "";
    [Export]
    public Control ResearchFocusPip = null;
    [Export]
    public UITooltipTrigger ResearchFocusToolTip = null;

    // --- Research ---
    [Export]
    public RichTextLabel ResearchResults = null;
    private static string ResearchResults_Original = "";
    [Export]
    public UITooltipTrigger ResearchToolTip = null;

    // --- Research --- Bonus
    [Export]
    public Control ResearchBonusBg = null;
    [Export]
    public RichTextLabel ResearchBonus = null;
    private static string ResearchBonus_Original = "";
    [Export]
    public UITooltipTrigger ResearchBonusToolTip = null;

    //[Export]
    //public Control CultureBg = null;
    //[Export]
    //public RichTextLabel CultureFocus = null;
    //private static string CultureFocus_Original = "";
    //[Export]
    //public RichTextLabel CultureResults = null;
    //private static string CultureResults_Original = "";
    //[Export]
    //public Control CultureBonusBg = null;
    //[Export]
    //public RichTextLabel CultureBonus = null;
    //private static string CultureBonus_Original = "";

    // --- Ships --- Focus
    [Export]
    public Control ShipsBg = null;
    [Export]
    public RichTextLabel ShipsFocus = null;
    private static string ShipsFocus_Original = "";
    [Export]
    public Control ShipsFocusPip = null;
    [Export]
    public UITooltipTrigger ShipsFocusToolTip = null;
    // --- Ships --- 
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
    public UITooltipTrigger ShipsToolTip = null;
    // --- Ships --- Bonus
    [Export]
    public Control ShipsBonusBg = null;
    [Export]
    public RichTextLabel ShipsBonus = null;
    private static string ShipsBonus_Original = "";
    [Export]
    public UITooltipTrigger ShipsBonusToolTip = null;

    //[Export]
    //public Control BasesBg = null;
    //[Export]
    //public RichTextLabel BasesFocus = null;
    //private static string BasesFocus_Original = "";
    //[Export]
    //public RichTextLabel BasesResults = null;
    //private static string BasesResults_Original = "";
    //[Export]
    //public Control BasesBonusBg = null;
    //[Export]
    //public RichTextLabel BasesBonus = null;
    //private static string BasesBonus_Original = "";

    [ExportCategory("Runtime")]
    [Export]
    public SystemData _System = null;

    public override void _Ready()
    {
        if (ColonizeFocus_Original.Length == 0) ColonizeFocus_Original = ColonizeFocus.Text;
        //if (DistrictsPlanetName_Original.Length == 0) DistrictsPlanetName_Original = DistrictsPlanetName.Text;
        if (DistrictsBuildingName_Original.Length == 0) DistrictsBuildingName_Original = DistrictsBuildingName.Text;
        if (DistrictsProgressValue_Original.Length == 0) DistrictsProgressValue_Original = DistrictsProgressValue.Text;
        if (ColonizeBonus_Original.Length == 0) ColonizeBonus_Original = ColonizeBonus.Text;

        if (FactoriesFocus_Original.Length == 0) FactoriesFocus_Original = FactoriesFocus.Text;
        if (FactoriesResults_Original.Length == 0) FactoriesResults_Original = FactoriesResults.Text;
        if (FactoriesBonus_Original.Length == 0) FactoriesBonus_Original = FactoriesBonus.Text;

        if (ResearchFocus_Original.Length == 0) ResearchFocus_Original = ResearchFocus.Text;
        if (ResearchResults_Original.Length == 0) ResearchResults_Original = ResearchResults.Text;
        if (ResearchBonus_Original.Length == 0) ResearchBonus_Original = ResearchBonus.Text;

        //if (CultureFocus_Original.Length == 0) CultureFocus_Original = CultureFocus.Text;
        //if (CultureResults_Original.Length == 0) CultureResults_Original = CultureResults.Text;
        //if (CultureBonus_Original.Length == 0) CultureBonus_Original = CultureBonus.Text;

        if (ShipsFocus_Original.Length == 0) ShipsFocus_Original = ShipsFocus.Text;
        if (ShipsName_Original.Length == 0) ShipsName_Original = ShipsName.Text;
        if (ShipsProgressValue_Original.Length == 0) ShipsProgressValue_Original = ShipsProgressValue.Text;
        if (ShipsBonus_Original.Length == 0) ShipsBonus_Original = ShipsBonus.Text;

        //if (BasesFocus_Original.Length == 0) BasesFocus_Original = BasesFocus.Text;
        //if (BasesResults_Original.Length == 0) BasesResults_Original = BasesResults.Text;
        //if (BasesBonus_Original.Length == 0) BasesBonus_Original = BasesBonus.Text;
    }

    public void Refresh(SystemData system)
    {
        _System = system;

        // --- DISTRICTS ---
        var districtsIncome = _System.Resources_PerTurn.GetIncome("Districts");
        ColonizeFocus.Text = ColonizeFocus_Original.Replace("$v", districtsIncome.ToString_FocusTotal());
        ColonizeFocusPip.Visible = _System.Resources.HasSub("Districts*FocusChosen");
        ColonizeFocusToolTip.Row_2_Right = ColonizeFocusToolTip.Row_2_Right_Original.Replace("$v", districtsIncome.ToString_FocusTotal());
        ColonizeFocusToolTip.Row_3 = ColonizeFocusToolTip.Row_3_Original.Split('\n')[0];
        ColonizeFocusToolTip.Row_3_Right = ColonizeFocusToolTip.Row_3_Right_Original.Split('\n')[0].Replace("$b", districtsIncome.ToString_FocusBase());
        for (int idx = 0; idx < districtsIncome.FocusOther.Count; idx++)
        {
            ColonizeFocusToolTip.Row_3 += "\n" + "From " + districtsIncome.FocusOther[idx].Colony.ColonyName;
            ColonizeFocusToolTip.Row_3_Right += "\n" + ColonizeFocusToolTip.Row_3_Right_Original.Split('\n')[1].Replace("$c", districtsIncome.FocusOther[idx].ToString_AsFocus());
        }
        if (districtsIncome.FocusChosen != 0)
        {
            ColonizeFocusToolTip.Row_3 += "\n" + ColonizeFocusToolTip.Row_3_Original.Split('\n')[2];
            ColonizeFocusToolTip.Row_3_Right += "\n" + ColonizeFocusToolTip.Row_3_Right_Original.Split('\n')[2].Replace("$f", districtsIncome.ToString_FocusChosen());
        }

        DistrictQueueWrapper queue = _System.DistrictsQueue_PerTurn;
        if (queue.DistrictsInQueue.Count > 0)
        {
            DistrictsProject.Visible = true;
            DistrictsAlert.Visible = false;
            DistrictsAlertPlanet.Visible = false;
            DistrictsAlertProject.Visible = false;

            DistrictsPlanetTex.Texture = Game.self.Assets.GetTexture2D_Planet(queue.DistrictsInQueue[0].Planet.Data.GetSub("Type").ValueS + ".png");
            //DistrictsPlanetName.Text = DistrictsPlanetName_Original.Replace("$name", planet.PlanetName)
            DistrictsBuildingTex.Texture = Game.self.Def.AssetLib.GetTexture2D_Symbols(queue.DistrictsInQueue[0].District.DistrictDef.Icon + ".png");
            DistrictsBuildingName.Text = DistrictsBuildingName_Original.Replace("$name", queue.DistrictsInQueue[0].District.DistrictDef.Name);

            DistrictsProgressCurrent.MaxValue = queue.DistrictsInQueue[0].District.DistrictDef.Cost;
            DistrictsProgressCurrent.Value = queue.DistrictsInQueue[0].Progress;
            DistrictsProgressNextTurn.MaxValue = queue.DistrictsInQueue[0].District.DistrictDef.Cost;
            DistrictsProgressNextTurn.Value = queue.DistrictsInQueue[0].EstimatedProgressNextTurn;

            DistrictsProgressValue.Text = DistrictsProgressValue_Original.Replace("$value", queue.DistrictsInQueue[0].EstimatedTurns.ToString());

            DistrictsToolTip.Row_1 = DistrictsToolTip.Row_1_Original.Split('\n')[0];
            DistrictsToolTip.Row_1_Right = DistrictsToolTip.Row_1_Right_Original.Replace("$v", Helper.ResValueToString(queue.DistrictsInQueue[0].Progress)).Replace("$t", Helper.ResValueToString(queue.DistrictsInQueue[0].District.DistrictDef.Cost));
            DistrictsToolTip.Row_2 = DistrictsToolTip.Row_2_Original.Split('\n')[0];
            DistrictsToolTip.Row_2_Right = DistrictsToolTip.Row_2_Right_Original.Replace("$v", districtsIncome.ToString_IncomeTotal(_System));
            DistrictsToolTip.Row_3 = DistrictsToolTip.Row_3_Original.Split('\n')[0];
            DistrictsToolTip.Row_3_Right = DistrictsToolTip.Row_3_Right_Original.Split('\n')[0];
            DistrictsToolTip.Row_3 = "\n" + DistrictsToolTip.Row_3_Original.Split('\n')[1].Replace("$p", Helper.ResValueToString(_System.Resources_PerTurn.GetIncomeSystemFocus("Districts"), 100));
            DistrictsToolTip.Row_3_Right = "\n" + DistrictsToolTip.Row_3_Right_Original.Split('\n')[1].Replace("$f", districtsIncome.ToString_IncomeFromSystem(_System));
            for (int idx = 0; idx < districtsIncome.IncomeFixedOther.Count; idx++)
            {
                DistrictsToolTip.Row_3 += "\n" + "From " + districtsIncome.IncomeFixedOther[idx].Colony.ColonyName;
                DistrictsToolTip.Row_3_Right += "\n" + DistrictsToolTip.Row_3_Right_Original.Split('\n')[2].Replace("$c", districtsIncome.IncomeFixedOther[idx].ToString_AsIncome());
            }
            if (districtsIncome.BonusTotal() != 0)
            {
                DistrictsToolTip.Row_3 += "\n" + DistrictsToolTip.Row_3_Original.Split('\n')[3].Replace("$p", Helper.ResValueToString(_System.Resources_PerTurn.GetIncomeSystemFocus("Districts"), 100));
                DistrictsToolTip.Row_3_Right += "\n" + DistrictsToolTip.Row_3_Right_Original.Split('\n')[3].Replace("$b", districtsIncome.ToString_BonusTotal());
            }
        }
        else
        {
            DistrictsProject.Visible = false;
            DistrictsAlert.Visible = true;

            if (Game.self.Input.SelectedPlanet != null)
            {
                DistrictsAlertPlanet.Visible = false;
                DistrictsAlertProject.Visible = true;
            }
            else
            {
                DistrictsAlertPlanet.Visible = true;
                DistrictsAlertProject.Visible = false;
            }
        }

        if (districtsIncome.BonusTotal() != 0)
        {
            ColonizeBonusBg.Visible = true;
            ColonizeBonus.Text = ColonizeBonus_Original.Replace("$b", districtsIncome.ToString_BonusTotal());

            ColonizeBonusToolTip.Row_1 = "";
            ColonizeBonusToolTip.Row_1_Right = "";
            for (int idx = 0; idx < districtsIncome.BonusOther.Count; idx++)
            {
                ColonizeBonusToolTip.Row_1 += (idx > 0 ? "\n" : "") + "From " + districtsIncome.BonusOther[idx].Colony.ColonyName;
                ColonizeBonusToolTip.Row_1_Right += (idx > 0 ? "\n" : "") + ColonizeBonusToolTip.Row_1_Right_Original.Split('\n')[0].Replace("$c", districtsIncome.BonusOther[idx].ToString_AsBonus());
            }
        }
        else
        {
            ColonizeBonusBg.Visible = false;
        }

        // --- FACTORIES ---
        var factoriesIncome = _System.Resources_PerTurn.GetIncome("Factories");
        var specialIncome = _System.Resources_PerTurn.SpecialIncome;

        int remainingFactories = _System.Buildings_PerTurn.FactoriesMax - _System.Buildings_PerTurn.Factories;
        if (remainingFactories > 0)
        {
            FactoriesFocusBtn.Disabled = false;
            FactoriesFocus.Text = FactoriesFocus_Original.Replace("$v", factoriesIncome.ToString_FocusTotal());
            FactoriesFocusPip.Visible = _System.Resources.HasSub("Factories*FocusChosen");
            FactoriesFocusToolTip.Visible = true;
            FactoriesFocusToolTip.Row_2_Right = FactoriesFocusToolTip.Row_2_Right_Original.Replace("$v", factoriesIncome.ToString_FocusTotal());
            FactoriesFocusToolTip.Row_3 = FactoriesFocusToolTip.Row_3_Original.Split('\n')[0];
            FactoriesFocusToolTip.Row_3_Right = FactoriesFocusToolTip.Row_3_Right_Original.Split('\n')[0].Replace("$b", factoriesIncome.ToString_FocusBase());
            for (int idx = 0; idx < factoriesIncome.FocusOther.Count; idx++)
            {
                FactoriesFocusToolTip.Row_3 += "\n" + "From " + factoriesIncome.FocusOther[idx].Colony.ColonyName;
                FactoriesFocusToolTip.Row_3_Right += "\n" + FactoriesFocusToolTip.Row_3_Right_Original.Split('\n')[1].Replace("$c", factoriesIncome.FocusOther[idx].ToString_AsFocus());
            }
            if (factoriesIncome.FocusChosen != 0)
            {
                FactoriesFocusToolTip.Row_3 += "\n" + FactoriesFocusToolTip.Row_3_Original.Split('\n')[2];
                FactoriesFocusToolTip.Row_3_Right += "\n" + FactoriesFocusToolTip.Row_3_Right_Original.Split('\n')[2].Replace("$f", factoriesIncome.ToString_FocusChosen());
            }

            int totalFactoriesIncome = factoriesIncome.IncomeAllTotal(_System);
            int factoriesPerTurn = Mathf.Min(10 * totalFactoriesIncome / specialIncome.FactoryCost, 10 * remainingFactories);
            FactoriesResults.Visible = true;
            FactoriesResults.Text = FactoriesResults_Original.Replace("$v", Helper.ResValueToString(factoriesPerTurn, 10));
            FactoriesToolTip.Row_1 = FactoriesToolTip.Row_1_Original.Split('\n')[0];
            FactoriesToolTip.Row_1_Right = FactoriesToolTip.Row_1_Right_Original.Replace("$v", factoriesIncome.ToString_IncomeTotal(_System));
            FactoriesToolTip.Row_2 = FactoriesToolTip.Row_2_Original.Split('\n')[0];
            FactoriesToolTip.Row_2_Right = FactoriesToolTip.Row_2_Right_Original.Split('\n')[0];
            FactoriesToolTip.Row_2 = "\n" + FactoriesToolTip.Row_2_Original.Split('\n')[1].Replace("$p", Helper.ResValueToString(_System.Resources_PerTurn.GetIncomeSystemFocus("Factories"), 100));
            FactoriesToolTip.Row_2_Right = "\n" + FactoriesToolTip.Row_2_Right_Original.Split('\n')[1].Replace("$f", factoriesIncome.ToString_IncomeFromSystem(_System));
            for (int idx = 0; idx < factoriesIncome.IncomeFixedOther.Count; idx++)
            {
                //(FactoriesFocusToolTip.Row_2.Length > 0 ? "\n" : "");
                FactoriesToolTip.Row_2 += "\n" + "From " + factoriesIncome.IncomeFixedOther[idx].Colony.ColonyName;
                FactoriesToolTip.Row_2_Right += "\n" + FactoriesToolTip.Row_2_Right_Original.Split('\n')[2].Replace("$c", factoriesIncome.IncomeFixedOther[idx].ToString_AsIncome());
            }
            if (factoriesIncome.BonusTotal() != 0)
            {
                FactoriesToolTip.Row_2 += "\n" + FactoriesToolTip.Row_2_Original.Split('\n')[3].Replace("$p", Helper.ResValueToString(_System.Resources_PerTurn.GetIncomeSystemFocus("Factories"), 100));
                FactoriesToolTip.Row_2_Right += "\n" + FactoriesToolTip.Row_2_Right_Original.Split('\n')[3].Replace("$b", factoriesIncome.ToString_BonusTotal());
            }
            FactoriesMax.Visible = false;
        }
        else
        {
            FactoriesFocusBtn.Disabled = true;
            FactoriesFocus.Text = FactoriesFocus_Original.Replace("$v", "0");
            FactoriesFocusToolTip.Visible = false;

            FactoriesResults.Visible = false;
            FactoriesMax.Visible = true;
        }

        if (factoriesIncome.BonusTotal() != 0)
        {
            FactoriesBonusBg.Visible = true;
            FactoriesBonus.Text = FactoriesBonus_Original.Replace("$b", factoriesIncome.ToString_BonusTotal());

            FactoriesBonusToolTip.Row_1 = "";
            FactoriesBonusToolTip.Row_1_Right = "";
            for (int idx = 0; idx < factoriesIncome.BonusOther.Count; idx++)
            {
                FactoriesBonusToolTip.Row_1 += (idx > 0 ? "\n" : "") + "From " + factoriesIncome.BonusOther[idx].Colony.ColonyName;
                FactoriesBonusToolTip.Row_1_Right += (idx > 0 ? "\n" : "") + FactoriesBonusToolTip.Row_1_Right_Original.Split('\n')[0].Replace("$c", factoriesIncome.BonusOther[idx].ToString_AsBonus());
            }
        }
        else
        {
            FactoriesBonusBg.Visible = false;
        }

        // --- RESEARCH ---
        var researchIncome = _System.Resources_PerTurn.GetIncome("Research");
        ResearchFocus.Text = ResearchFocus_Original.Replace("$v", researchIncome.ToString_FocusTotal());
        ResearchFocusPip.Visible = _System.Resources.HasSub("Research*FocusChosen");
        ResearchFocusToolTip.Row_2_Right = ResearchFocusToolTip.Row_2_Right_Original.Replace("$v", researchIncome.ToString_FocusTotal());
        ResearchFocusToolTip.Row_3 = ResearchFocusToolTip.Row_3_Original.Split('\n')[0];
        ResearchFocusToolTip.Row_3_Right = ResearchFocusToolTip.Row_3_Right_Original.Split('\n')[0].Replace("$b", researchIncome.ToString_FocusBase());
        for (int idx = 0; idx < researchIncome.FocusOther.Count; idx++)
        {
            ResearchFocusToolTip.Row_3 += "\n" + "From " + researchIncome.FocusOther[idx].Colony.ColonyName;
            ResearchFocusToolTip.Row_3_Right += "\n" + ResearchFocusToolTip.Row_3_Right_Original.Split('\n')[1].Replace("$c", researchIncome.FocusOther[idx].ToString_AsFocus());
        }
        if (researchIncome.FocusChosen != 0)
        {
            ResearchFocusToolTip.Row_3 += "\n" + ResearchFocusToolTip.Row_3_Original.Split('\n')[2];
            ResearchFocusToolTip.Row_3_Right += "\n" + ResearchFocusToolTip.Row_3_Right_Original.Split('\n')[2].Replace("$f", researchIncome.ToString_FocusChosen());
        }

        ResearchResults.Text = ResearchResults_Original.Replace("$v", researchIncome.ToString_IncomeTotal(_System));
        ResearchToolTip.Row_1 = ResearchToolTip.Row_1_Original.Split('\n')[0];
        ResearchToolTip.Row_1_Right = ResearchToolTip.Row_1_Right_Original.Replace("$v", researchIncome.ToString_IncomeTotal(_System));
        ResearchToolTip.Row_2 = ResearchToolTip.Row_2_Original.Split('\n')[0];
        ResearchToolTip.Row_2_Right = ResearchToolTip.Row_2_Right_Original.Split('\n')[0];
        ResearchToolTip.Row_2 = "\n" + ResearchToolTip.Row_2_Original.Split('\n')[1].Replace("$p", Helper.ResValueToString(_System.Resources_PerTurn.GetIncomeSystemFocus("Research"), 100));
        ResearchToolTip.Row_2_Right = "\n" + ResearchToolTip.Row_2_Right_Original.Split('\n')[1].Replace("$f", researchIncome.ToString_IncomeFromSystem(_System));
        for (int idx = 0; idx < researchIncome.IncomeFixedOther.Count; idx++)
        {
            //(ResearchFocusToolTip.Row_2.Length > 0 ? "\n" : "");
            ResearchToolTip.Row_2 += "\n" + "From " + researchIncome.IncomeFixedOther[idx].Colony.ColonyName;
            ResearchToolTip.Row_2_Right += "\n" + ResearchToolTip.Row_2_Right_Original.Split('\n')[2].Replace("$c", researchIncome.IncomeFixedOther[idx].ToString_AsIncome());
        }
        if (researchIncome.BonusTotal() != 0)
        {
            ResearchToolTip.Row_2 += "\n" + ResearchToolTip.Row_2_Original.Split('\n')[3].Replace("$p", Helper.ResValueToString(system.Resources_PerTurn.GetIncomeSystemFocus("Research"), 100));
            ResearchToolTip.Row_2_Right += "\n" + ResearchToolTip.Row_2_Right_Original.Split('\n')[3].Replace("$b", researchIncome.ToString_BonusTotal());
        }


        if (researchIncome.BonusTotal() != 0)
        {
            ResearchBonusBg.Visible = true;
            ResearchBonus.Text = ResearchBonus_Original.Replace("$b", researchIncome.ToString_BonusTotal());

            ResearchBonusToolTip.Row_1 = "";
            ResearchBonusToolTip.Row_1_Right = "";
            for (int idx = 0; idx < researchIncome.BonusOther.Count; idx++)
            {
                ResearchBonusToolTip.Row_1 += (idx > 0 ? "\n" : "") + "From " + researchIncome.BonusOther[idx].Colony.ColonyName;
                ResearchBonusToolTip.Row_1_Right += (idx > 0 ? "\n" : "") + ResearchBonusToolTip.Row_1_Right_Original.Split('\n')[0].Replace("$c", researchIncome.BonusOther[idx].ToString_AsBonus());
            }
        }
        else
        {
            ResearchBonusBg.Visible = false;
        }

        // ---
        //CultureFocus.Text = CultureFocus_Original.Replace("$v", "10");
        //CultureResults.Text = CultureResults_Original.Replace("$val", "13");
        //if (false)
        //{
        //    CultureBonusBg.Visible = true;
        //    CultureBonus.Text = CultureBonus_Original.Replace("$b", "33");
        //}
        //else
        //{
        //    CultureBonusBg.Visible = false;
        //}

        // --- SHIPBUILDING
        var shipsIncome = _System.Resources_PerTurn.GetIncome("Shipbuilding");
        ShipsFocus.Text = ShipsFocus_Original.Replace("$v", shipsIncome.ToString_FocusTotal());
        ShipsFocusPip.Visible = _System.Resources.HasSub("Shipbuilding*FocusChosen");
        ShipsFocusToolTip.Row_2_Right = ShipsFocusToolTip.Row_2_Right_Original.Replace("$v", shipsIncome.ToString_FocusTotal());
        ShipsFocusToolTip.Row_3 = ShipsFocusToolTip.Row_3_Original.Split('\n')[0];
        ShipsFocusToolTip.Row_3_Right = ShipsFocusToolTip.Row_3_Right_Original.Split('\n')[0].Replace("$b", shipsIncome.ToString_FocusBase());
        for (int idx = 0; idx < shipsIncome.FocusOther.Count; idx++)
        {
            ShipsFocusToolTip.Row_3 += "\n" + "From " + shipsIncome.FocusOther[idx].Colony.ColonyName;
            ShipsFocusToolTip.Row_3_Right += "\n" + ShipsFocusToolTip.Row_3_Right_Original.Split('\n')[1].Replace("$c", shipsIncome.FocusOther[idx].ToString_AsFocus());
        }
        if (shipsIncome.FocusChosen != 0)
        {
            ShipsFocusToolTip.Row_3 += "\n" + ShipsFocusToolTip.Row_3_Original.Split('\n')[2];
            ShipsFocusToolTip.Row_3_Right += "\n" + ShipsFocusToolTip.Row_3_Right_Original.Split('\n')[2].Replace("$f", shipsIncome.ToString_FocusChosen());
        }

        //ShipsTex.Texture = ;
        ShipsName.Text = ShipsName_Original.Replace("$name", _System.Shipbuilding_PerTurn.Design.DesignName);
        ShipsProgressCurrent.MaxValue = _System.Shipbuilding_PerTurn.Design.Cost;
        ShipsProgressCurrent.Value = _System.Shipbuilding_PerTurn.Progress;
        ShipsProgressNextTurn.MaxValue = _System.Shipbuilding_PerTurn.Design.Cost;
        ShipsProgressNextTurn.Value = _System.Shipbuilding_PerTurn.EstimatedProgressNextTurn;
        ShipsProgressValue.Text = ShipsProgressValue_Original.Replace("$value", _System.Shipbuilding_PerTurn.EstimatedTurns.ToString());

        ShipsToolTip.Row_1 = ShipsToolTip.Row_1_Original.Split('\n')[0];
        ShipsToolTip.Row_1_Right = ShipsToolTip.Row_1_Right_Original.Replace("$t", Helper.ResValueToString(_System.Shipbuilding_PerTurn.Design.Cost)).Replace("$v", Helper.ResValueToString(_System.Shipbuilding_PerTurn.Progress));
        ShipsToolTip.Row_2 = ShipsToolTip.Row_2_Original.Split('\n')[0];
        ShipsToolTip.Row_2_Right = ShipsToolTip.Row_2_Right_Original.Replace("$v", shipsIncome.ToString_IncomeTotal(_System));
        ShipsToolTip.Row_3 = ShipsToolTip.Row_3_Original.Split('\n')[0];
        ShipsToolTip.Row_3_Right = ShipsToolTip.Row_3_Right_Original.Split('\n')[0];
        ShipsToolTip.Row_3 = "\n" + ShipsToolTip.Row_3_Original.Split('\n')[1].Replace("$p", Helper.ResValueToString(_System.Resources_PerTurn.GetIncomeSystemFocus("Shipbuilding"), 100));
        ShipsToolTip.Row_3_Right = "\n" + ShipsToolTip.Row_3_Right_Original.Split('\n')[1].Replace("$f", shipsIncome.ToString_IncomeFromSystem(_System));
        for (int idx = 0; idx < shipsIncome.IncomeFixedOther.Count; idx++)
        {
            //(ShipsFocusToolTip.Row_2.Length > 0 ? "\n" : "");
            ShipsToolTip.Row_3 += "\n" + "From " + shipsIncome.IncomeFixedOther[idx].Colony.ColonyName;
            ShipsToolTip.Row_3_Right += "\n" + ShipsToolTip.Row_3_Right_Original.Split('\n')[2].Replace("$c", shipsIncome.IncomeFixedOther[idx].ToString_AsIncome());
        }
        if (shipsIncome.BonusTotal() != 0)
        {
            ShipsToolTip.Row_3 += "\n" + ShipsToolTip.Row_3_Original.Split('\n')[3].Replace("$p", Helper.ResValueToString(_System.Resources_PerTurn.GetIncomeSystemFocus("Shipbuilding"), 100));
            ShipsToolTip.Row_3_Right += "\n" + ShipsToolTip.Row_3_Right_Original.Split('\n')[3].Replace("$b", shipsIncome.ToString_BonusTotal());
        }
        //ShipsBonusToolTip.

        if (shipsIncome.BonusTotal() != 0)
        {
            ShipsBonusBg.Visible = true;
            ShipsBonus.Text = ShipsBonus_Original.Replace("$b", shipsIncome.ToString_BonusTotal());

            ShipsBonusToolTip.Row_1 = "";
            ShipsBonusToolTip.Row_1_Right = "";
            for (int idx = 0; idx < shipsIncome.BonusOther.Count; idx++)
            {
                ShipsBonusToolTip.Row_1 += (idx > 0 ? "\n" : "") + "From " + shipsIncome.BonusOther[idx].Colony.ColonyName;
                ShipsBonusToolTip.Row_1_Right += (idx > 0 ? "\n" : "") + ShipsBonusToolTip.Row_1_Right_Original.Split('\n')[0].Replace("$c", shipsIncome.BonusOther[idx].ToString_AsBonus());
            }
        }
        else
        {
            ShipsBonusBg.Visible = false;
        }

        // ---
        //BasesBg.Visible = false;
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
        ActionFocus.SetFocusOn(_System, "Districts");
    }

    public void OnFocusFactories()
    {
        ActionFocus.SetFocusOn(_System, "Factories");
    }

    public void OnFocusResearch()
    {
        ActionFocus.SetFocusOn(_System, "Research");
    }

    public void OnFocusCulture()
    {
    }

    public void OnFocusShips()
    {
        ActionFocus.SetFocusOn(_System, "Shipbuilding");
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