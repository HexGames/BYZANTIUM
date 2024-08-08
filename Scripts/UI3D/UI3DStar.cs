using Godot;
using Godot.Collections;
using System.Data;

public partial class UI3DStar : Control
{
    // is beeing duplicated
    PanelContainer SystemBg = null;
    PanelContainer ColonyBg = null;
    PanelContainer ExtraBg = null;
    RichTextLabel SystemName = null;
    RichTextLabel ColonyName = null;
    RichTextLabel ExtraColonies = null;

    [Export]
    public GFXStar GFX = null;

    public override void _Ready()
    {
        SystemBg = GetNode<PanelContainer>("VBoxContainer/PanelContainer_1");
        ColonyBg = GetNode<PanelContainer>("VBoxContainer/PanelContainer_2");
        ExtraBg = GetNode<PanelContainer>("VBoxContainer/PanelContainer_3");
        SystemName = GetNode<RichTextLabel>("VBoxContainer/PanelContainer_1/System");
        ColonyName = GetNode<RichTextLabel>("VBoxContainer/PanelContainer_2/Colony");
        ExtraColonies = GetNode<RichTextLabel>("VBoxContainer/PanelContainer_3/Extra");
    }

    public void Refresh( StarNode star )
    {
        //_Star = star;
        //if (_Star == null) return;

        //TEMP01 _Star.GFX.HUD = this; 

        //SystemName.Text = _Star.Data.StarName;// + " " + _Star.Data.X.ToString() + ":" + _Star.Data.Y.ToString() + ":" + _Star.Data.Z.ToString();
        if (SystemName.Text.Contains("Sol"))
        {
            ColonyName.Text = "Colony";
            ColonyBg.Visible = true;
            ExtraColonies.Text = "+2";
            ExtraBg.Visible = true;
        }
        else
        {
            ColonyBg.Visible = false;
            ExtraBg.Visible = false;
        }
    }

    //public override void _Process(double delta)
    //{
        //Vector2 pos2D = Game.Camera.UnprojectPosition(_Star.GFX.Position + new Vector3(0.0f, 0.0f, 2.5f));
        //Position = pos2D;
    //}

    //public void OnHoverEnter()
    //{
    //    if (Parent.PlanetSelected != this) PlanetName.AnchorTop = 0.6f;
    //}
    //
    //public void OnHoverExit()
    //{
    //    PlanetName.AnchorTop = 0.8f;
    //}
    //
    //public void OnSelect()
    //{
    //    if (Selected) return;
    //
    //    CustomMinimumSize = new Vector2(256, 256);
    //    PlanetName.AnchorTop = 0.8f;
    //
    //    Selected = true;
    //    Parent.Select( this );
    //}
}