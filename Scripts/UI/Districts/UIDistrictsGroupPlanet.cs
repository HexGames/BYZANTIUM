using Godot;
using System.Transactions;

public partial class UIDistrictsGroupPlanet : Control
{
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
    private UITooltipTrigger PlanetResourceTooltip;

    private Control PlanetPopsBg;
    private UIText PlanetPopsText;
    private UITooltipTrigger PlanetPopsTooltip;

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
        PlanetFeatureTooltip = GetNode<UITooltipTrigger>("Planet/Feature/ToolTip");

        PlanetResourceBg = GetNode<Control>("Planet/Resource");
        PlanetResourceText = GetNode<UIText>("Planet/Resource/Resource");
        PlanetResourceTooltip = GetNode<UITooltipTrigger>("Planet/Resource/ToolTip");

        PlanetPopsBg = GetNode<Control>("Planet/Pops");
        PlanetPopsText = GetNode<UIText>("Planet/Pops/Pops");
        PlanetPopsTooltip = GetNode<UITooltipTrigger>("Planet/Pops/ToolTip");

        PlanetPopsTimeBg = GetNode<Control>("Planet/PopsTime");
        PlanetPopsTimeText = GetNode<UIText>("Planet/PopsTime/PopsTime");
        PlanetPopsTimeTooltip = GetNode<UITooltipTrigger>("Planet/PopsTime/ToolTip");

        PlanetLevelBg = GetNode<Control>("Planet/Level");
        PlanetLevelText = GetNode<UIText>("Planet/Level/Level");
        PlanetLevelTooltip = GetNode<UITooltipTrigger>("Planet/Level/ToolTip");

        PlanetLevelTimeBg = GetNode<Control>("Planet/LevelTime");
        PlanetLevelTimeText = GetNode<UIText>("Planet/LevelTime/LevelTime");
        PlanetLevelTimeTooltip = GetNode<UITooltipTrigger>("Planet/LevelTime/ToolTip");

        PlanetRuralBg = GetNode<Control>("Planet/Rural");
        PlanetRuralText = GetNode<UIText>("Planet/Rural/Rural");
        PlanetRuralTooltip = GetNode<UITooltipTrigger>("Planet/Rural/ToolTip");

        PlanetButton = GetNode<Button>("Planet/Panel/Button");
        if (HasNode("Planet/PopsList"))
        {
            PlanetPopsList = GetNode<UIDistrictsGroupPlanetPops>("Planet/PopsList");
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
                Research.Visible = true;
                Bank.Visible = true;
                Culture.Visible = true;
                Farm.Visible = true;
                Industrial.Visible = true;
                Urban.Visible = true;

                PlanetPrivate.Visible = false;
                PlanetState.Visible = false;
                PlanetRural.Visible = true;
                PlanetPopMax.Visible = true;

                PlanetResourceBg.Visible = true;
                PlanetResourceText.SetTextWithReplace("$r", Helper.GetIcon("Growth"));

                PlanetPopsBg.Visible = true;
                PlanetPopsText.SetTextWithReplace("$", _Planet.Colony.GetPopsMax().ToString());

                PlanetPopsTimeBg.Visible = true;
                PlanetPopsTimeText.SetTextWithReplace("$", "oo");

                PlanetLevelBg.Visible = false;

                PlanetLevelTimeBg.Visible = false;

                PlanetRuralBg.Visible = true;
                PlanetRuralText.SetTextWithReplace("$", _Planet.Colony.Districts[0].Pops.Count.ToString());
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

                PlanetPopsBg.Visible = false;

                PlanetPopsTimeBg.Visible = false;

                PlanetLevelBg.Visible = true;
                PlanetLevelText.SetTextWithReplace("$", _Planet.Colony.Districts[0].GetLevel().ToString());

                PlanetLevelTimeBg.Visible = true;
                PlanetLevelTimeText.SetTextWithReplace("$", "oo");

                PlanetRuralBg.Visible = false;
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
                PlanetPopsBg.Visible = true;
                PlanetPopsText.SetTextWithReplace("$", PlanetRaw.GetBaseMaxPops(_Planet.Data, Game.self.Def).ToString());
            }
            else
            {
                PlanetPopMax.Visible = false;
                PlanetPopsBg.Visible = false;
            }

            PlanetPopsTimeBg.Visible = false;

            PlanetLevelBg.Visible = false;

            PlanetLevelTimeBg.Visible = false;

            PlanetRuralBg.Visible = false;
        }
    }
}