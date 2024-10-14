using Godot;
using Godot.Collections;
using System.Data;

public partial class UI3DPlanet : Control
{
    // is beeing duplicated
    Panel PipIconBg = null;
    TextureRect PipIcon = null;
    Panel OnlyIconBg = null;
    TextureRect OnlyIcon = null;
    RichTextLabel OnlyIconQueue = null;
    VBoxContainer HabitableBg = null;
    TextureRect Arrow = null;
    PanelContainer Row_1_Bg = null;
    //PanelContainer Row_2_Bg = null;
    //PanelContainer Row_3_Bg = null;
    RichTextLabel Row_1 = null;
    TextureRect Icon = null;
    //RichTextLabel Row_2 = null;
    //RichTextLabel Row_3 = null;

    [Export]
    public GFXStarOrbit GFX = null;

    public override void _Ready()
    {
        PipIconBg = GetNode<Panel>("Pip");
        PipIcon = GetNode<TextureRect>("Pip/PipIcon");
        OnlyIconBg = GetNode<Panel>("Panel");
        OnlyIcon = GetNode<TextureRect>("Panel/Panel/OnlyIcon");
        OnlyIconQueue = GetNode<RichTextLabel>("Panel/Queue"); 
        HabitableBg = GetNode<VBoxContainer>("VBoxContainer");
        Arrow = GetNode<TextureRect>("VBoxContainer/ArrowBg/Arrow");
        Row_1_Bg = GetNode<PanelContainer>("VBoxContainer/PanelContainer_1");
        //Row_2_Bg = GetNode<PanelContainer>("VBoxContainer/PanelContainer_2");
        //Row_3_Bg = GetNode<PanelContainer>("VBoxContainer/PanelContainer_3");
        Row_1 = GetNode<RichTextLabel>("VBoxContainer/PanelContainer_1/System");
        Icon = GetNode<TextureRect>("VBoxContainer/PanelContainer_1/IconBg/Icon");
        //Row_2 = GetNode<RichTextLabel>("VBoxContainer/PanelContainer_2/Colony");
        //Row_3 = GetNode<RichTextLabel>("VBoxContainer/PanelContainer_3/Extra");
    }

    public void Refresh()
    {
        if (GFX._Planet.Data.HasSub("Habitable"))
        {
            PipIconBg.Visible = false;
            OnlyIconBg.Visible = false;
            HabitableBg.Visible = true;

            Row_1.Text = GFX._Planet.PlanetName;
            switch (GFX._Planet.Data.GetSub("Size").ValueI)
            {
                case 1: Icon.Texture = Game.self.Def.AssetLib.GetTexture2D_District("Planet_1.png"); break;
                case 2: Icon.Texture = Game.self.Def.AssetLib.GetTexture2D_District("Planet_2.png"); break;
                default: Icon.Texture = Game.self.Def.AssetLib.GetTexture2D_District("Planet_3.png"); break;
            }

            if (GFX._Planet.Colony != null)
            {
                Arrow.SelfModulate = Game.self.UILib.GetPlayerColor(GFX._Planet.Colony._System._Player.PlayerID);
                Row_1_Bg.SelfModulate = Game.self.UILib.GetPlayerColor(GFX._Planet.Colony._System._Player.PlayerID);
                Icon.SelfModulate = Game.self.UILib.GetPlayerColor(GFX._Planet.Colony._System._Player.PlayerID);
            }
            else
            {
                Arrow.SelfModulate = new Color(0.75f, 0.75f, 0.75f, 0.75f);
                Row_1_Bg.SelfModulate = new Color(0.5f, 0.5f, 0.5f);
                Icon.SelfModulate = new Color(0.75f, 0.75f, 0.75f, 0.75f);
            }
        }
        else
        {
            HabitableBg.Visible = false;

            if (GFX._Planet.Colony != null)
            {
                PipIconBg.Visible = false;
                OnlyIconBg.Visible = true;
                OnlyIcon.Texture = Game.self.Def.AssetLib.GetTexture2D_Symbols(GFX._Planet.Colony.Districts[0].DistrictDef.Icon + ".png");
                if (GFX._Planet.Colony.Districts[0].Data.HasSub("InQueue"))
                {
                    OnlyIconBg.SelfModulate = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                    OnlyIconQueue.Visible = true;
                    OnlyIconQueue.Text = (GFX._Planet.Colony.Districts[0].Data.GetSub("InQueue").ValueI + 1).ToString();
                }
                else
                {
                    OnlyIconBg.SelfModulate = Game.self.UILib.GetPlayerColor(GFX._Planet.Colony._System._Player.PlayerID);
                    OnlyIconQueue.Visible = false;
                }
            }
            else
            {
                PipIconBg.Visible = true;
                OnlyIconBg.Visible = false;
                PipIcon.Texture = Game.self.Def.AssetLib.GetTexture2D_Symbols(GFX._Planet.Features[0].FeatureDef.Icon + ".png");
            }
        }
    }
}