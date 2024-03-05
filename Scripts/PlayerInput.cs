using Godot;
using System;

// Generated
public partial class PlayerInput : Node
{
    [ExportCategory("Runtime")]
    [Export]
    public Game Game = null;
    [Export]
    public StarNode SelectedLocation = null;

    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            Game = GetNode<Game>("/root/Main/Game");
            //OnSelect += PlayerInput.SelectLocation;
        }
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventMouseButton mouseButtonEvent)
        {
            if (!mouseButtonEvent.IsPressed())
            {
                // on mouse button release
                if (mouseButtonEvent.ButtonIndex == MouseButton.Right)
                {
                    //GD.Print("You clicked on " + Label.Text);
                    DeselectLocation();
                }
            }
        }

        if (inputEvent is InputEventKey keyButtonEvent)
        {
            if (!keyButtonEvent.IsPressed())
            {
                // on key button release
                if (keyButtonEvent.Keycode == Key.S)
                {
                    Game.Map.SaveMap();
                }
                if (keyButtonEvent.Keycode == Key.D)
                {
                    Game.Map.LoadMap();
                    Game.Map.Data.GenerateGameFromData(Game.Def);
                }
            }
        }
    }

    public void SelectLocation( StarNode newSelectedLocation )
    {
        if (SelectedLocation == newSelectedLocation)
        {
            // on reselect
            GD.Print("PlayerInput - reselecteed " + SelectedLocation.Data.StarName);

            return;
        }

        if (SelectedLocation != null)
        {
            DeselectLocation();
        }
        SelectedLocation = newSelectedLocation;

        Game.SystemUI.Refresh(SelectedLocation.Data);

        // on select
        if (SelectedLocation != null) GD.Print("PlayerInput - selected " + SelectedLocation.Data.StarName);
    }

    public void DeselectLocation()
    {
        // on deselect
        if (SelectedLocation != null) GD.Print("PlayerInput - deselected " + SelectedLocation.Data.StarName);

        SelectedLocation = null;

        Game.SystemUI.Refresh(null);
    }
}
