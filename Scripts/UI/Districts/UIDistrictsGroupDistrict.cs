using Godot;

public partial class UIDistrictsGroupDistrict : Control
{
    // is beeing duplicated
    private TextureRect TopSlice;
    private TextureRect LeftSlice;
    private TextureRect RightSlice;
    private TextureRect PlanetIcon;
    private TextureRect Feature;
    private UITooltipTrigger FeatureToolTip;
    private UIText Level;
    private UITooltipTrigger LevelToolTip;
    private UIText MaxPops;
    private UITooltipTrigger MaxPopsToolTip;
    private UIText Pops;
    private UITooltipTrigger PopsToolTip;
    private UIText Private;
    private UITooltipTrigger PrivateToolTip;
    private UIText ExtraPops;
    private UITooltipTrigger ExtraPopsToolTip;
    private Control ProductionBg;
    private UIText Production;
    private UITooltipTrigger ProductionToolTip;

    private UIDistrictsPlanetDistrictPops PopsDetail = null;
    private Control EvenGap = null;

    // runtime
    public PlanetData _Planet = null;
    public DistrictData _District = null;

    public override void _Ready()
    {
        string prefix = "";
        if (HasNode("District"))
        {
            prefix = "District/";
            PopsDetail = GetNode<UIDistrictsPlanetDistrictPops>("Pops");
            EvenGap = GetNode<Control>("Gap_Even");
        }
        else
        {
            PopsDetail = null;
            EvenGap = null;
        }

        TopSlice = GetNode<TextureRect>(prefix + "Bg/Top");
        LeftSlice = GetNode<TextureRect>(prefix + "Bg/Left");
        RightSlice = GetNode<TextureRect>(prefix + "Bg/Right");
        PlanetIcon = GetNode<TextureRect>(prefix + "Bg/Round/BoxContainer/Planet");
        Feature = GetNode<TextureRect>(prefix + "Info/Feature");
        FeatureToolTip = GetNode<UITooltipTrigger>(prefix + "Info/Feature/ToolTip");
        Level = GetNode<UIText>(prefix + "Info/Level/Level");
        LevelToolTip = GetNode<UITooltipTrigger>(prefix + "Info/Level/ToolTip");
        MaxPops = GetNode<UIText>(prefix + "Info/MaxPops/MaxPops");
        MaxPopsToolTip = GetNode<UITooltipTrigger>(prefix + "Info/MaxPops/ToolTip");
        Pops = GetNode<UIText>(prefix + "Info/Pops/Pops");
        PopsToolTip = GetNode<UITooltipTrigger>(prefix + "Info/Pops/ToolTip");
        Private = GetNode<UIText>(prefix + "Info/Private/Private");
        PrivateToolTip = GetNode<UITooltipTrigger>(prefix + "Info/Private/ToolTip");
        ExtraPops = GetNode<UIText>(prefix + "Info/ExtraPops/ExtraPops");
        ExtraPopsToolTip = GetNode<UITooltipTrigger>(prefix + "Info/ExtraPops/ToolTip");
        ProductionBg = GetNode<Control>(prefix + "Info/ProductionBg");
        Production = GetNode<UIText>(prefix + "Info/Production");
        ProductionToolTip = GetNode<UITooltipTrigger>(prefix + "Info/Production/ToolTip");
    }

    public void Refresh(DistrictData district, bool selectedPlanet)
    {
        _District = district;
        _Planet = _District._Colony.Planet;

        TopSlice.Visible = true;
        TopSlice.SelfModulate = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        LeftSlice.Visible = true;

        RightSlice.Visible = true;
        if (district.DistrictDef.Type == "Rural_District")
        {
            RightSlice.SelfModulate = new Color("005c03");

            Level.Visible = false;
            LevelToolTip.Visible = false;
            MaxPops.Visible = true;
            MaxPops.SetTextWithReplace("$", district._Colony.GetPopsMax().ToString());
            MaxPopsToolTip.Visible = true;

            TopSlice.SelfModulate = new Color("005c03");

            Private.Visible = false;
            PrivateToolTip.Visible = false;
            ExtraPops.Visible = true;
            ExtraPops.SetTextWithReplace("$", "+" + (district._Colony.GetPopsMax() - district._Colony.GetPopsCurrent()).ToString());
            ExtraPopsToolTip.Visible = true;
        }
        else
        {
            RightSlice.SelfModulate = new Color("ff8000");

            MaxPops.Visible = false;
            MaxPopsToolTip.Visible = false;
            Level.Visible = true;
            Level.SetTextWithReplace("$", district.GetLevel().ToString());
            LevelToolTip.Visible = true;

            TopSlice.SelfModulate = new Color("fbe52d", 0.0f);

            Private.Visible = false;
            PrivateToolTip.Visible = false;
            ExtraPops.Visible = false;
            ExtraPopsToolTip.Visible = false;
        }

        RefreshPlanetIcon();
        PlanetIcon.SelfModulate = new Color(1.0f, 1.0f, 1.0f, 0.75f);

        if (_Planet.Features.Count > 0)
        {
            Feature.Visible = true;
            //Feature.m
        }
        else
        {
            Feature.Visible = false;
        }

        Pops.Visible = true;
        Pops.SetTextWithReplace("$", district.Pops.Count.ToString());
        PopsToolTip.Visible = true;

        ProductionBg.Visible = true;
        Production.Visible = true;
        Production.SetTextWithReplace("$value", "  " + district.Economy_PerTurn.ToString_Short(24));
    }

    public void Refresh(PlanetData planet)
    {
        _District = null;
        _Planet = planet;

        TopSlice.Visible = false;
        LeftSlice.Visible = false;

        if (planet.IsHabitable())
        {
            RightSlice.Visible = true;
            RightSlice.SelfModulate = new Color("005c03");

            Level.Visible = false;
            LevelToolTip.Visible = false;
            MaxPops.Visible = true;
            MaxPops.SetTextWithReplace("$", PlanetRaw.GetBaseMaxPops(planet.Data, Game.self.Def).ToString());
            MaxPopsToolTip.Visible = true;
        }
        else
        {
            RightSlice.Visible = false;

            Level.Visible = false;
            LevelToolTip.Visible = false;
            MaxPops.Visible = false;
            MaxPopsToolTip.Visible = false;
        }

        RefreshPlanetIcon();
        PlanetIcon.SelfModulate = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        if (_Planet.Features.Count > 0)
        {
            Feature.Visible = true;
            //Feature.m
        }
        else
        {
            Feature.Visible = false;
        }

        Pops.Visible = false;
        PopsToolTip.Visible = false;
        Private.Visible = false;
        PrivateToolTip.Visible = false;
        ExtraPops.Visible = false;
        ExtraPopsToolTip.Visible = false;
        ProductionBg.Visible = false;
        Production.Visible = false;
    }

    private void RefreshPlanetIcon()
    {
        PlanetIcon.Texture = Game.self.Def.AssetLib.GetTexture2D_Planet(_Planet.Data.GetSubValueS("Type") + ".png");
        if (_Planet.Data.HasSub("Size"))
        {
            PlanetIcon.CustomMinimumSize = new Vector2(_Planet.Data.GetSubValueI("Size") * 8 + TopSlice.Size.X - 80, _Planet.Data.GetSubValueI("Size") * 8 + TopSlice.Size.Y - 80);
        }
        else
        {
            PlanetIcon.CustomMinimumSize = TopSlice.Size;
        }
    }

    public void RefreshPops(bool even)
    {
        if (_District != null)
        {
            PopsDetail.Visible = true;
            PopsDetail.Refresh(_District);
        }
        else
        {
            PopsDetail.Visible = false;
        }
        EvenGap.Visible = even;
    }
}