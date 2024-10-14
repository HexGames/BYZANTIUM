using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UISelectedPlanetItem : Control
{
    // is beeing duplicated
    private Button IconBtn;
    private TextureRect Icon;
    private RichTextLabel IconText;
    private static string IconText_Original = "";
    private RichTextLabel ItemName;
    private static string ItemName_Original = "";
    private RichTextLabel Description;
    private static string Description_Original = "";
    private Panel Working = null;
    private RichTextLabel Queue = null;
    private static string Queue_Original = "";
    private RichTextLabel Turns = null;
    private static string Turns_Original = "";

    private UITooltipTrigger Tooltip;

    [ExportCategory("Runtime")]
    [Export]
    public PlanetData _Planet = null;
    [Export]
    public DataBlock _Feature = null;
    [Export]
    public DataBlock _District = null;
    // possbile district
    public DistrictData _PossibleDistrict = null;

    public override void _Ready()
    {
        IconBtn = GetNode<Button>("MarginContainer/HBoxContainer/Icon/Button");
        Icon = GetNode<TextureRect>("MarginContainer/HBoxContainer/Icon/Icon");
        IconText = GetNode<RichTextLabel>("MarginContainer/HBoxContainer/Icon/IconText");
        if (IconText_Original.Length == 0) IconText_Original = IconText.Text;
        ItemName = GetNode<RichTextLabel>("MarginContainer/HBoxContainer/VBoxContainer/Name");
        if (ItemName_Original.Length == 0) ItemName_Original = ItemName.Text;
        Description = GetNode<RichTextLabel>("MarginContainer/HBoxContainer/VBoxContainer/Description");
        if (Description_Original.Length == 0) Description_Original = Description.Text;
        if (HasNode("MarginContainer/HBoxContainer/Icon/TextBox"))
        {
            Working = GetNode<Panel>("MarginContainer/HBoxContainer/Icon/TextBox");
        }
        if (HasNode("MarginContainer/HBoxContainer/Queued"))
        { 
            Queue = GetNode<RichTextLabel>("MarginContainer/HBoxContainer/Queued");
            if (Queue_Original.Length == 0) Queue_Original = Queue.Text;
        }
        if (HasNode("MarginContainer/HBoxContainer/Turns"))
        {
            Turns = GetNode<RichTextLabel>("MarginContainer/HBoxContainer/Turns");
            if (Turns_Original.Length == 0) Turns_Original = Turns.Text;
        }
    }

    public void RefreshPlanet(PlanetData planet)
    {
        _Planet = planet;
        _Feature = null;
        _District = null;
        _PossibleDistrict = null;

        IconBtn.Visible = false;
        Icon.Visible = true;
        Icon.Texture = Game.self.Assets.GetTexture2D_Planet(_Planet.Data.GetSub("Type").ValueS + ".png");
        IconText.Visible = false;
        ItemName.Text = ItemName_Original.Replace("$name", _Planet.Data.GetSub("Type").ValueS);
        if (_Planet.Data.HasSub("Habitable"))
        {
            Description.Text = Description_Original.Replace("$description", "Habitable");
        }
        else
        {
            Description.Text = Description_Original.Replace("$description", "Uninhabitable");
        }

        if (Working != null) Working.Visible = false;
        if (Queue != null) Queue.Visible = false;
        if (Turns != null) Turns.Visible = false;
    }

    public void RefreshFeature(DataBlock feature)
    {
        _Planet = null;
        _Feature = feature;
        _District = null;
        _PossibleDistrict = null;

        DefFeatureWrapper featureInfo = Game.self.Def.GetFeatureInfo(feature.Name);

        IconBtn.Visible = false;
        Icon.Texture = null; //Game.self.Assets.GetPlanetTexture2D(_Feature.Name + ".png");
        if (Icon.Texture == null)
        {
            if (featureInfo.Icon != "") IconText.Text = IconText_Original.Replace("$$", Helper.GetIcon(featureInfo.Icon, 64));
            else IconText.Text = IconText_Original.Replace("$$", _Feature.Name.Substr(0, 2));
            IconText.Visible = true;
            Icon.Visible = false;
        }
        else
        {
            IconText.Visible = false;
            Icon.Visible = true;
        }
        ItemName.Text = ItemName_Original.Replace("$name", _Feature.Name);
        if (featureInfo != null && featureInfo._Data.HasSub("Benefit"))
        {
            Description.Text = Description_Original.Replace("$description", featureInfo._Data.GetSub("Benefit").ToUIDscriptionShort());
        }
        else
        {
            Description.Text = Description_Original.Replace("$description", "");
        }
        
        if (Working != null) Working.Visible = false;
        if (Queue != null) Queue.Visible = false;
        if (Turns != null) Turns.Visible = false;
    }

    public void RefreshDistrict(DataBlock district)
    {
        _Planet = null;
        _Feature = null;
        _District = district;
        _PossibleDistrict = null;

        DefDistrictWrapper districtInfo = null;
        if (_District.ValueS != "")
        {
            districtInfo = Game.self.Def.GetDistrictInfo(_District.ValueS);
        }

        if (districtInfo != null)
        {
            // existing district
            //Icon.Texture = Game.self.Assets.GetTexture2D_District(_District.ValueS + ".png");
            Icon.Texture = Game.self.Def.AssetLib.GetTexture2D_Symbols(districtInfo.Icon + ".png");
            if (Icon.Texture == null)
            {
                if (districtInfo.Icon != "") IconText.Text = IconText_Original.Replace("$$", Helper.GetIcon(districtInfo.Icon, 64));
                else IconText.Text = IconText_Original.Replace("$$", _District.Name.Substr(0, 2));
                IconText.Visible = true;
                Icon.Visible = false;
            }
            else
            {
                IconText.Visible = false;
                Icon.Visible = true;
            }

            ItemName.Text = ItemName_Original.Replace("$name", _District.ValueS);
            if (districtInfo._Data.HasSub("Benefit"))
            {
                Description.Text = Description_Original.Replace("$description", districtInfo._Data.GetSub("Benefit").ToUIDscriptionShort());
            }
            else
            {
                Description.Text = Description_Original.Replace("$description", "");
            }

            if (_District.HasSub("InQueue"))
            {
                if (Working != null) Working.Visible = true;
                int positionInQueue = _District.GetSub("InQueue").ValueI;
                if (Queue != null)
                {
                    Queue.Visible = true;
                    Queue.Text = Queue_Original.Replace("$q", (positionInQueue + 1).ToString());
                }
                if (Turns != null) Turns.Visible = false;
                IconBtn.Visible = true;
            }
            else
            {
                if (Working != null) Working.Visible = false;
                if (Queue != null) Queue.Visible = false;
                if (Turns != null) Turns.Visible = false;
                IconBtn.Visible = false;
            }
        }
        else
        {
            // no district || no colony
            IconBtn.Visible = true;
            IconText.Text = IconText_Original.Replace("$$", "+");
            IconText.Visible = true;
            Icon.Visible = false;
            ItemName.Text = ItemName_Original.Replace("$name", "Construct " + _District.Name);
            Description.Text = Description_Original.Replace("$description", "");

            if (Working != null) Working.Visible = false;
            if (Queue != null) Queue.Visible = false;
            if (Turns != null) Turns.Visible = false;
        }
    }

    public void RefreshDistrict(PlanetData planet)
    {
        _Planet = null;
        _Feature = null;
        _District = null;
        _PossibleDistrict = null;

        IconBtn.Visible = true;
        IconText.Text = IconText_Original.Replace("$$", "+");
        IconText.Visible = true;
        Icon.Visible = false;
        ItemName.Text = ItemName_Original.Replace("$name", "Construct " + planet.Data.GetSub("SlotType").ValueS);
        Description.Text = Description_Original.Replace("$description", "");

        if (Queue != null) Queue.Visible = false;
        if (Turns != null) Turns.Visible = false;
    }

    public void RefreshPossibleDistrict(DistrictData possibleDistrict)
    {
        _Planet = null;
        _Feature = null;
        _District = null;
        _PossibleDistrict = possibleDistrict;

        IconBtn.Visible = true;
        //Icon.Texture = Game.self.Assets.GetTexture2D_District(_PossibleDistrict.DistrictDef.Name + ".png");
        Icon.Texture = Game.self.Assets.GetTexture2D_Symbols(_PossibleDistrict.DistrictDef. Icon + ".png");
        if (Icon.Texture == null)
        {
            IconText.Text = IconText_Original.Replace("$$", _PossibleDistrict.DistrictDef.Name.Substr(0, 2));
            IconText.Visible = true;
            Icon.Visible = false;
        }
        else
        {
            IconText.Visible = false;
            Icon.Visible = true;
        }

        ItemName.Text = ItemName_Original.Replace("$name", _PossibleDistrict.DistrictDef.Name);
        if (_PossibleDistrict.DistrictDef._Data.HasSub("Benefit"))
        {
            Description.Text = Description_Original.Replace("$description", _PossibleDistrict.DistrictDef._Data.GetSub("Benefit").ToUIDscriptionShort());
        }
        else
        {
            Description.Text = Description_Original.Replace("$description", "");
        }

        if (Queue != null) Queue.Visible = false;
        if (Turns != null)
        {
            Turns.Visible = true;
            Turns.Text = Turns_Original.Replace("$t", "99");
        }
    }

    public void OnSelect()
    {
        if (_PossibleDistrict != null)
        {
            // possible district
            Game.self.GalaxyUI.PlanetInfo.HideBuildDistrictSelector();

            ActionBuildDistrict.AddToQueue(Game.self, _PossibleDistrict);

            Game.self.GalaxyUI.PlanetInfo.Refresh(_PossibleDistrict._Planet);
        }
        else if (_Planet != null) // nope
        {
            // planet
        }
        else if (_Feature != null) // nope
        {
            // feature
        }
        else if (_District != null && _District.ValueS != "")
        {
            // exiting district

            if (false)
            {
                // in construction
            }
        }
        else if (_District != null)
        {
            // empty district slot
            Game.self.GalaxyUI.PlanetInfo.ShowBuildDistrictSelector();
        }
        else
        {
            // no colony
            Game.self.GalaxyUI.PlanetInfo.ShowBuildDistrictSelector();
        }
    }
}