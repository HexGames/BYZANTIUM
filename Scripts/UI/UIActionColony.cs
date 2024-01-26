using Godot;
using Godot.Collections;
using System.ComponentModel;

//[Tool]
public partial class UIActionColony : Control
{
    [Export]
    public ProgressBar Progress;
    [Export]
    public Label CurrentAction;

    [Export]
    public Button SwitchBtn;

    [Export]
    public Control ActionBg;
    [Export]
    public Array<Button> Actions;

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

    /*public void AutoLinkFunc()
    {
        Actions.Clear();

        Progress = GetTree().EditedSceneRoot.GetNode<ProgressBar>("InProgress/ProgressBar");
        CurrentAction = GetTree().EditedSceneRoot.GetNode<Label>("InProgress/Label");

        SwitchBtn = GetTree().EditedSceneRoot.GetNode<Button>("InProgress/SwitchBtn");

        ActionBg = GetTree().EditedSceneRoot.GetNode<Control>("PanelContainer");

        for (int n = 1; n < 12; n++)
        {
            Actions.Add(GetTree().EditedSceneRoot.GetNode<Button>("PanelContainer/VBoxContainer/Action_" + n.ToString()));
        }
    }*/

    public override void _Ready()
    {
        if (Engine.IsEditorHint()) return;

        Game = GetNode<Game>("/root/Main/Game");
        Visible = false;
    }

    public void Refresh(UISystemPlanet planet)
    {
        Reparent(planet, false);

        DataBlock actionData = planet._Data.GetSub("Action");

        if (actionData != null) 
        {
        }
        else
        {
            Progress.Value = 0;
            CurrentAction.Text = "Choose a Build Action";

            SwitchBtn.Visible = false;
            ActionBg.Visible = true;

            actionData.GetSubs("Possible");
        }
    }
}