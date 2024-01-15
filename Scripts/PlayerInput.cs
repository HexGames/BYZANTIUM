using Godot;
using System;

// Generated
public partial class PlayerInput : Node
{
    [ExportCategory("Runtime")]
    [Export]
    public UISystem SystemUI = null;
    [Export]
    public LocationNode SelectedLocation = null;

    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            SystemUI = GetNode<UISystem>("/root/Main/SystemUI");
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
    }

    public void SelectLocation( LocationNode newSelectedLocation )
    {
        if (SelectedLocation == newSelectedLocation)
        {
            // on reselect
            GD.Print("PlayerInput - reselecteed " + SelectedLocation.Data.System.ValueS);

            return;
        }

        if (SelectedLocation != null)
        {
            DeselectLocation();
        }
        SelectedLocation = newSelectedLocation;

        SystemUI.Refresh(SelectedLocation.Data);

        // on select
        if (SelectedLocation != null) GD.Print("PlayerInput - selected " + SelectedLocation.Data.System.ValueS);
    }

    public void DeselectLocation()
    {
        // on deselect
        if (SelectedLocation != null) GD.Print("PlayerInput - deselected " + SelectedLocation.Data.System.ValueS);

        SelectedLocation = null;

        SystemUI.Refresh(null);
    }
}
