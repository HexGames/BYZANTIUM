using Godot;
using Godot.Collections;
using System.ComponentModel;

//[Tool]
public partial class UIColonyActionBuild : Control
{
    [Export]
    public ProgressBar Progress = null;
    [Export]
    public Label CurrentAction = null;

    [Export]
    public Button SwitchBtn = null;

    [Export]
    public Control ActionsBg = null;
    [Export]
    public Array<Button> Actions = new Array<Button>();

    [Export]
    public ColonyData _ColonyData = null;

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
        Actions[0].Pressed += () => { OnActionChosen(0); };
        Visible = false;
    }

    public void Refresh(ColonyData colony)
    {
        // delete surplus
        // for
        {
            // Actions[idx].visible= false;
        }
        Visible = true;

        if (colony.ActionBuild != null)
        {
            int progress = colony.ActionBuild.GetSub("Progress").ValueI;
            int progress_max = colony.ActionBuild.GetSub("Progress_Max").ValueI;
            string building = colony.ActionBuild.GetSub("Building").ValueS;

            Progress.MaxValue = progress_max;
            Progress.Value = progress;

            CurrentAction.Text = "Building " + building;

            SwitchBtn.Visible = true;
            ActionsBg.Visible = false;
        }
        else
        {
            Progress.Value = 0;
            CurrentAction.Text = "Choose a Build Action";

            SwitchBtn.Visible = false;
            ActionsBg.Visible = true;

            _ColonyData = colony;

            // grow
            while (Actions.Count < colony.ActionsBuildPossible.Count)
            {
                Button newAction = Actions[0].Duplicate(7) as Button;
                Actions[0].GetParent().AddChild(newAction);
                int id = Actions.Count;
                newAction.Pressed += () => { OnActionChosen(id); };
                Actions.Add(newAction);
            }

            for (int idx = 0; idx < Actions.Count; idx++)
            {
                if (idx < colony.ActionsBuildPossible.Count)
                {
                    Actions[idx].Text = colony.ActionsBuildPossible[idx].TargetInfo._Data.ValueS;
                    Actions[idx].TooltipText = GetBuldingTooltip(colony.ActionsBuildPossible[idx].TargetInfo._Data);
                    Actions[idx].Visible = true;
                }
                else
                {
                    Actions[idx].Visible = false;
                }
            }
        }
    }

    private void OnActionChosen(int idx)
    {
        GD.Print("Presed Button " + idx.ToString());

        _ColonyData.ActionsBuildPossible[idx].Start();

        Refresh(_ColonyData);
    }

    static private string GetBuldingTooltip(DataBlock building)
    {
        string text = "";

        DataBlock turns = building.GetSub("Turns");
        DataBlock cost = building.GetSub("Cost");
        DataBlock benefit = building.GetSub("Benefit");
        if (turns != null)
        {
            text += turns.ToToolTipString();
            text += "\n";
        }
        if (cost != null)
        {
            text += cost.ToToolTipString();
            text += "\n";
        }
        if (benefit != null)
        {
            text += benefit.ToToolTipString();
        }

        return text;
    }
}