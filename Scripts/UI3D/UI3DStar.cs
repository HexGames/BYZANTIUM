using Godot;
using Godot.Collections;
using System.Data;

public partial class UI3DStar : Control
{
    // is beeing duplicated
    //TextureRect Arrow = null;
    PanelContainer Row_1_Bg = null;
    //PanelContainer Row_2_Bg = null;
    //PanelContainer Row_3_Bg = null;
    RichTextLabel Row_1 = null;
    //RichTextLabel Row_2 = null;
    //RichTextLabel Row_3 = null;
    Array<TextureRect> Icons = new Array<TextureRect>();

    [Export]
    public GFXStar GFX = null;

    public override void _Ready()
    {
        //Arrow = GetNode<TextureRect>("VBoxContainer/TextureRect");
        Row_1_Bg = GetNode<PanelContainer>("VBoxContainer/PanelContainer_1");
        //Row_2_Bg = GetNode<PanelContainer>("VBoxContainer/PanelContainer_2");
        //Row_3_Bg = GetNode<PanelContainer>("VBoxContainer/PanelContainer_3");
        Row_1 = GetNode<RichTextLabel>("VBoxContainer/PanelContainer_1/System");
        //Row_2 = GetNode<RichTextLabel>("VBoxContainer/PanelContainer_2/Colony");
        //Row_3 = GetNode<RichTextLabel>("VBoxContainer/PanelContainer_3/Extra");
        Icons.Clear();
        Icons.Add(GetNode<TextureRect>("VBoxContainer/HBoxContainer/Icon_1"));
        Icons.Add(GetNode<TextureRect>("VBoxContainer/HBoxContainer/Icon_2"));
        Icons.Add(GetNode<TextureRect>("VBoxContainer/HBoxContainer/Icon_3"));
    }

    public void Refresh()
    {
        Row_1.Text = GFX._Star.StarName.Replace("_"," ");
        Row_1_Bg.Visible = true;

        if (GFX._Star.System != null)
        {
            //Arrow.SelfModulate = Game.self.UILib.GetPlayerColor(GFX._Star.System._Sector._Player.PlayerID);

            if (GFX._Star.System.Capital)
            {
                Row_1.Text = GFX._Star.StarName.Replace("_", " ") + "[img=24x24]Assets/UI/Symbols/Capital.png[/img]";
            }
            Row_1_Bg.SelfModulate = Game.self.UILib.GetPlayerColor(GFX._Star.System._Player.PlayerID);
            //for (int idx = 0; idx < Icons.Count; idx++)
            //{
            //    Icons[idx].SelfModulate = Game.self.UILib.GetPlayerColor(GFX._Star.System._Player.PlayerID);
            //    Icons[idx].Visible = false;
            //}

            //Row_2.Text = "3P 3BC";
            //Row_2_Bg.Visible = false;
            //Row_2_Bg.SelfModulate = Game.self.UILib.GetPlayerColor(GFX._Star.System._Player.PlayerID);
            //
            //Row_3.Text = "3P 3BC";
            //Row_3_Bg.Visible = false;
            //Row_3_Bg.SelfModulate = Game.self.UILib.GetPlayerColor(GFX._Star.System._Player.PlayerID);
        }
        else
        {
            //Arrow.SelfModulate = new Color(0.75f, 0.75f, 0.75f, 0.75f);
            Row_1_Bg.SelfModulate = new Color(0.5f, 0.5f, 0.5f);
            //for (int idx = 0; idx < Icons.Count; idx++)
            //{
            //    Icons[idx].SelfModulate = new Color(0.75f, 0.75f, 0.75f, 0.75f);
            //    Icons[idx].Visible = false;
            //}

            //Row_2_Bg.Visible = false;
            //Row_3_Bg.Visible = false;
        }

        int iconIdx = 0;
        for (int idx = 0; idx < Icons.Count; idx++)
        {
            Icons[idx].Visible = false;
        }
        for (int planetIdx = 0; planetIdx < GFX._Star.Planets.Count; planetIdx++)
        {
            if (GFX._Star.Planets[planetIdx].IsHabitable())
            {
                switch (GFX._Star.Planets[planetIdx].Data.GetSub("Size").ValueI)
                {
                    case 1: Icons[iconIdx].Texture = Game.self.Def.AssetLib.GetTexture2D_District("Planet_1.png"); break;
                    case 2: Icons[iconIdx].Texture = Game.self.Def.AssetLib.GetTexture2D_District("Planet_2.png"); break;
                    default: Icons[iconIdx].Texture = Game.self.Def.AssetLib.GetTexture2D_District("Planet_3.png"); break;
                }

                if (GFX._Star.Planets[planetIdx].Colony != null) Icons[iconIdx].SelfModulate = Game.self.UILib.GetPlayerColor(GFX._Star.System._Player.PlayerID);
                else Icons[iconIdx].SelfModulate = new Color(0.75f, 0.75f, 0.75f, 0.75f);

                Icons[iconIdx].Visible = true;
                iconIdx++;
                if (iconIdx >= Icons.Count)
                    break;

            }
        }
        
    }
}