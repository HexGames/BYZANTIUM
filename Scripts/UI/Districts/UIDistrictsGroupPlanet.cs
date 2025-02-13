using Godot;
using System.Collections.Generic;
using System.Transactions;

public partial class UIDistrictsGroupPlanet : Control
{
    [Export]
    public bool SelectedPlanet = false; // for duplicated false is fine

    // is beeing duplicated
    private UIDistrictsGroupPlanetItem Shipyard;
    private UIDistrictsGroupPlanetItem Research;
    private UIDistrictsGroupPlanetItem Bank;
    private UIDistrictsGroupPlanetItem Culture;
    private UIDistrictsGroupPlanetItem Farm;
    private UIDistrictsGroupPlanetItem Industrial;
    private UIDistrictsGroupPlanetItem Urban;

    private Control PlanetPrivate;
    private Control PlanetState;
    private Control PlanetRural;
    private Control PlanetPopMax;
    private TextureRect PlanetIcon;
    private TextureRect PlanetFeature;
    private UITooltipTrigger PlanetFeatureTooltip;

    private Control PlanetResourceBg;
    private UIText PlanetResourceText;

    private Control PlanetPopsMaxBg;
    private UIText PlanetPopsMaxText;
    private UITooltipTrigger PlanetPopsMaxTooltip;

    private Control PlanetPopsTimeBg;
    private UIText PlanetPopsTimeText;
    private UITooltipTrigger PlanetPopsTimeTooltip;

    private Control PlanetLevelBg;
    private UIText PlanetLevelText;
    private UITooltipTrigger PlanetLevelTooltip;

    private Control PlanetLevelTimeBg;
    private UIText PlanetLevelTimeText;
    private UITooltipTrigger PlanetLevelTimeTooltip;

    private Control PlanetRuralBg;
    private UIText PlanetRuralText;
    private UITooltipTrigger PlanetRuralTooltip;

    private Button PlanetButton;
    private UIPulse PlanetButtonHighlight;
    private UIDistrictsGroupPlanetPops PlanetPopsList = null;

    // runtime
    public PlanetData _Planet = null;

    public override void _Ready()
    {
        Shipyard = GetNode<UIDistrictsGroupPlanetItem>("Shipyard");
        Research = GetNode<UIDistrictsGroupPlanetItem>("Research");
        Bank = GetNode<UIDistrictsGroupPlanetItem>("Bank");
        Culture = GetNode<UIDistrictsGroupPlanetItem>("Culture");
        Farm = GetNode<UIDistrictsGroupPlanetItem>("Farm");
        Industrial = GetNode<UIDistrictsGroupPlanetItem>("Industrial");
        Urban = GetNode<UIDistrictsGroupPlanetItem>("Urban");

        PlanetPrivate = GetNode<Control>("Planet/Panel/Private");
        PlanetState = GetNode<Control>("Planet/Panel/State");
        PlanetRural = GetNode<Control>("Planet/Panel/Rural");
        PlanetPopMax = GetNode<Control>("Planet/Panel/PopMax");
        PlanetIcon = GetNode<TextureRect>("Planet/Panel/Round/BoxContainer/Planet");
        PlanetFeature = GetNode<TextureRect>("Planet/Feature");
        //PlanetFeatureTooltip = GetNode<UITooltipTrigger>("Planet/Feature/ToolTip");
        PlanetFeatureTooltip = GetNode<UITooltipTrigger>("Planet/Button/FeatureToolTip");

        PlanetResourceBg = GetNode<Control>("Planet/Resource");
        PlanetResourceText = GetNode<UIText>("Planet/Resource/Resource");

        PlanetPopsMaxBg = GetNode<Control>("Planet/PopsMax");
        PlanetPopsMaxText = GetNode<UIText>("Planet/PopsMax/Pops");
        //PlanetPopsMaxTooltip = GetNode<UITooltipTrigger>("Planet/PopsMax/ToolTip");
        PlanetPopsMaxTooltip = GetNode<UITooltipTrigger>("Planet/Button/PlanetToolTip");

        PlanetPopsTimeBg = GetNode<Control>("Planet/PopsTime");
        PlanetPopsTimeText = GetNode<UIText>("Planet/PopsTime/PopsTime");
        //PlanetPopsTimeTooltip = GetNode<UITooltipTrigger>("Planet/PopsTime/ToolTip");
        PlanetPopsTimeTooltip = GetNode<UITooltipTrigger>("Planet/Button/PopsTimeToolTip");

        PlanetLevelBg = GetNode<Control>("Planet/Level");
        PlanetLevelText = GetNode<UIText>("Planet/Level/Level");
        //PlanetLevelTooltip = GetNode<UITooltipTrigger>("Planet/LevelToolTip");
        PlanetLevelTooltip = GetNode<UITooltipTrigger>("Planet/Button/LevelToolTip");

        PlanetLevelTimeBg = GetNode<Control>("Planet/LevelTime");
        PlanetLevelTimeText = GetNode<UIText>("Planet/LevelTime/LevelTime");
        //PlanetLevelTimeTooltip = GetNode<UITooltipTrigger>("Planet/LevelTime/ToolTip");
        PlanetLevelTimeTooltip = GetNode<UITooltipTrigger>("Planet/Button/LevelTimeToolTip");

        PlanetRuralBg = GetNode<Control>("Planet/Rural");
        PlanetRuralText = GetNode<UIText>("Planet/Rural/Rural");
        //PlanetRuralTooltip = GetNode<UITooltipTrigger>("Planet/RuralToolTip");
        PlanetRuralTooltip = GetNode<UITooltipTrigger>("Planet/Button/RuralToolTip");

        PlanetButton = GetNode<Button>("Planet/Button");
        PlanetButtonHighlight = GetNode<UIPulse>("Planet/Button/Highlight");
        if (HasNode("Planet/Pops"))
        {
            PlanetPopsList = GetNode<UIDistrictsGroupPlanetPops>("Planet/Pops");
        }
    }

    public void Refresh(PlanetData planet)
    {
        _Planet = planet;

        PlanetIcon.Texture = Game.self.Def.AssetLib.GetTexture2D_Planet(_Planet.Data.GetSubValueS("Type") + ".png");
        if (_Planet.Data.HasSub("Size"))
        {
            PlanetIcon.CustomMinimumSize = new Vector2(_Planet.Data.GetSubValueI("Size") * 8 + 32, _Planet.Data.GetSubValueI("Size") * 8 + 32);
        }
        else
        {
            PlanetIcon.CustomMinimumSize = new Vector2(80, 80);
        }

        PlanetFeature.Visible = false;

        if (_Planet.Colony != null)
        {
            if (_Planet.IsHabitable())
            {
                // habitable planet
                Shipyard.Visible = true;
                Shipyard.Refresh(_Planet.Colony.GetDistrictByName("Shipyard_District"));
                Research.Visible = true;
                Research.Refresh(_Planet.Colony.GetDistrictByName("Tech_District"));
                Bank.Visible = true;
                Bank.Refresh(_Planet.Colony.GetDistrictByName("Bank_District"));
                Culture.Visible = true;
                Culture.Refresh(_Planet.Colony.GetDistrictByName("Culture_District"));
                Farm.Visible = true;
                Farm.Refresh(_Planet.Colony.GetDistrictByName("Farm_District"));
                Industrial.Visible = true;
                Industrial.Refresh(_Planet.Colony.GetDistrictByName("Industrial_District"));
                Urban.Visible = true;
                Urban.Refresh(_Planet.Colony.GetDistrictByName("Urban_District"));

                PlanetPrivate.Visible = false;
                PlanetState.Visible = false;
                PlanetRural.Visible = true;
                PlanetPopMax.Visible = true;

                PlanetResourceBg.Visible = true;
                PlanetResourceText.SetTextWithReplace("$r", Helper.GetIcon("Growth"));

                PlanetPopsMaxBg.Visible = true;
                PlanetPopsMaxText.SetTextWithReplace("$", _Planet.Colony.GetPopsMax().ToString());

                PlanetPopsTimeBg.Visible = true;
                PlanetPopsTimeText.SetTextWithReplace("$", "oo");

                PlanetLevelBg.Visible = false;
                PlanetLevelTooltip.Visible = false;

                PlanetLevelTimeBg.Visible = false;

                DistrictData rural = _Planet.Colony.GetDistrictByName("Rural_District");
                PlanetRuralBg.Visible = true;
                PlanetRuralText.SetTextWithReplace("$", rural.Pops.Count.ToString());
                PlanetRuralTooltip.Visible = true;

                if (PlanetPopsList != null)
                {
                    PlanetPopsList.Visible = true;
                    PlanetPopsList.Refresh(rural);
                }
            }
            else
            {
                // outpost, stations or asteroid bases
                Shipyard.Visible = false;
                Research.Visible = false;
                Bank.Visible = false;
                Culture.Visible = false;
                Farm.Visible = false;
                Industrial.Visible = false;
                Urban.Visible = false;

                PlanetPrivate.Visible = _Planet.Colony.Districts[0].IsPrivate();
                PlanetState.Visible = !PlanetPrivate.Visible;
                PlanetRural.Visible = false;
                PlanetPopMax.Visible = false;

                PlanetResourceBg.Visible = true;
                PlanetResourceText.SetTextWithReplace("$r", Helper.GetIcon("BC"));

                PlanetPopsMaxBg.Visible = false;

                PlanetPopsTimeBg.Visible = false;

                PlanetLevelBg.Visible = true;
                PlanetLevelText.SetTextWithReplace("$", _Planet.Colony.Districts[0].GetLevel().ToString());
                PlanetLevelTooltip.Visible = true;

                PlanetLevelTimeBg.Visible = true;
                PlanetLevelTimeText.SetTextWithReplace("$", "oo");

                PlanetRuralBg.Visible = false;
                PlanetRuralTooltip.Visible = false;

                if (PlanetPopsList != null)
                {
                    PlanetPopsList.Visible = false;
                }
            }
        }
        else
        {
            Shipyard.Visible = false;
            Research.Visible = false;
            Bank.Visible = false;
            Culture.Visible = false;
            Farm.Visible = false;
            Industrial.Visible = false;
            Urban.Visible = false;

            PlanetPrivate.Visible = false;
            PlanetState.Visible = false;
            PlanetRural.Visible = false;

            PlanetResourceBg.Visible = false;

            if (_Planet.IsHabitable())
            {
                PlanetPopMax.Visible = true;
                PlanetPopsMaxBg.Visible = true;
                PlanetPopsMaxText.SetTextWithReplace("$", PlanetRaw.GetBaseMaxPops(_Planet.Data, Game.self.Def).ToString());
            }
            else
            {
                PlanetPopMax.Visible = false;
                PlanetPopsMaxBg.Visible = false;
            }

            PlanetPopsTimeBg.Visible = false;

            PlanetLevelBg.Visible = false;
            PlanetLevelTooltip.Visible = false;

            PlanetLevelTimeBg.Visible = false;

            PlanetRuralBg.Visible = false;
            PlanetRuralTooltip.Visible = false;

            if (PlanetPopsList != null)
            {
                PlanetPopsList.Visible = false;
            }
        }

        PlanetButton.Disabled = SelectedPlanet;
        PlanetButtonHighlight.Visible = false;
    }

    public List<ActionBase> PossibleActions = new List<ActionBase>();
    public void SetPossibleActions<T>(List<T> allPossibleActions) where T : ActionBase
    {
        if (allPossibleActions.Count == 0) return;

        PossibleActions.Clear();
        for (int idx = 0; idx < allPossibleActions.Count; idx++)
        {
            if (allPossibleActions[idx] is ActionEconomyColonize)
            {
                ActionEconomyColonize action = allPossibleActions[idx] as ActionEconomyColonize;
                if (action.Planet == _Planet)
                {
                    PossibleActions.Add(allPossibleActions[idx]);
                }
            }
        }

        if (PossibleActions.Count > 0)
        {
            PlanetButton.Disabled = false;
            PlanetButtonHighlight.Visible = true;
        }
        else
        {
            PlanetButton.Disabled = true;
            PlanetButtonHighlight.Visible = false;
        }
    }

    public void ClearPossibleActions()
    {
        PossibleActions.Clear();
        PlanetButton.Disabled = SelectedPlanet;
        PlanetButtonHighlight.Visible = false;
    }

    public void OnButton()
    {
        if (PossibleActions.Count > 0)
        {
            Game.self.Input.OnAction_Economy_SelectPlanet(PossibleActions);
        }
        else
        {
            Game.self.Input.OnHoverPlanet(_Planet);
            Game.self.Input.OnSelectPlanet(_Planet);
        }
    }
}