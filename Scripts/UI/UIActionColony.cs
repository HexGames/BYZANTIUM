using Godot;
using Godot.Collections;

//[Tool]
public partial class UIActionColony : Control
{
    //[Export]
    //public bool AutoLink
    //{
    //    get => false;
    //    set
    //    {
    //        if (value)
    //        {
    //            AutoLinkFunc();
    //        }
    //    }
    //}

    Game Game;

    //public void AutoLinkFunc()
    //{
    //   
    //}

    public override void _Ready()
    {
        if (Engine.IsEditorHint()) return;

        Game = GetNode<Game>("/root/Main/Game");
        Visible = false;
    }

    public void Refresh(Control planet)
    {
        Reparent(planet, false);
    }
}