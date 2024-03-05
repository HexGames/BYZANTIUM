using Godot;
using Godot.Collections;
using System.Collections.Generic;

//[Tool]
public partial class UIBudgetItemTreasury : Control
{
    // is beeing duplicated
    private UIBudget Parent = null;
    private RichTextLabel ValueLabel = null;
    private string ValueLabel_Original = null;
    //private ColorRect ValueBar = null;
    //private Control BarOverlay = null;

    private Array<Button> Add = new Array<Button>();
    private Array<Button> Pip = new Array<Button>();
    private Array<Button> Deficit = new Array<Button>();
    private Array<Button> AddDeficit = new Array<Button>();
    private Array<Control> Gap = new Array<Control>();

    [ExportCategory("Runtime")]
    [Export]
    public int Locked = 0;
    public int Pips = 0;
    public int PipsRemaining = 0;
    public int Value = 0;
    //public int ValueMax = 0;

    Game Game;

    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            Game = GetNode<Game>("/root/Main/Game");

            Parent = GetNode<UIBudget>("../../../../../../");

            ValueLabel = GetNode<RichTextLabel>("Color_3/Value");
            ValueLabel_Original = ValueLabel.Text;
            //ValueBar = GetNode<ColorRect>("Color_2/Icon/Bar");
            //BarOverlay = GetNode<ColorRect>("Color_2/Icon/Bar/DeficitOverlay");

            Add.Clear();
            for (int id = 1; id <= 5; id++)
            {
                Add.Add(GetNode<Button>("Main/VBoxContainer/VBoxContainer/Add_" + id.ToString()));
            }
            Pip.Clear();
            for (int id = 1; id <= 5; id++)
            {
                Pip.Add(GetNode<Button>("Main/VBoxContainer/VBoxContainer/Pip_" + id.ToString()));
            }
            Deficit.Clear();
            for (int id = 1; id <= 5; id++)
            {
                Deficit.Add(GetNode<Button>("Main/VBoxContainer/VBoxContainer/Deficit_" + id.ToString()));
            }
            AddDeficit.Clear();
            for (int id = 1; id <= 5; id++)
            {
                AddDeficit.Add(GetNode<Button>("Main/VBoxContainer/VBoxContainer/AddDeficit_" + id.ToString()));
            }
            Gap.Clear();
            for (int id = 1; id <= 5; id++)
            {
                Gap.Add(GetNode<Control>("Main/VBoxContainer/VBoxContainer/Gap_" + id.ToString()));
            }

            Visible = false;
        }
    }

    public void Refresh(int locked, int unlocked, int pipsRemaining, int value/*, int valueMax*/)
    {
        Locked = locked;
        Pips = unlocked;

        PipsRemaining = pipsRemaining;
        Value = value;
        //ValueMax = valueMax;

        ValueLabel.Text = ValueLabel_Original.Replace("$value", (value / 10).ToString());

        //ValueBar.CustomMinimumSize = new Vector2(ValueBar.CustomMinimumSize.X, 112 * Mathf.Abs(value) / valueMax);
        //BarOverlay.Visible = value < 0;
        if (value >= 0)
        {
            for (int idx = 0; idx < Pip.Count; idx++)
            {
                if (idx < Locked)
                {
                    Pip[idx].Disabled = true;
                    Pip[idx].Visible = true;
                }
                else if (idx < Locked + Pips)
                {
                    Pip[idx].Disabled = false;
                    Pip[idx].Visible = true;
                }
                else
                {
                    Pip[idx].Visible = false;
                }
            }

            for (int idx = 0; idx < Add.Count; idx++)
            {
                if (idx < pipsRemaining)
                {
                    Add[idx].Visible = true;
                }
                else
                {
                    Add[idx].Visible = false;
                }
            }
            for (int idx = 0; idx < Deficit.Count; idx++)
            {
                Deficit[idx].Visible = false;
            }
            for (int idx = 0; idx < Gap.Count; idx++)
            {
                Gap[idx].Visible = false;
            }
            for (int idx = 0; idx < AddDeficit.Count; idx++)
            {
                AddDeficit[idx].Visible = false;
            }
        }
        else
        {
            for (int idx = 0; idx < Pip.Count; idx++)
            {
                Pip[idx].Visible = false;
            }
            for (int idx = 0; idx < Add.Count; idx++)
            {
                Add[idx].Visible = false;
            }
            for (int idx = 0; idx < Deficit.Count; idx++)
            {
                if (idx < -Locked)
                {
                    Deficit[idx].Disabled = true;
                    Deficit[idx].Visible = true;
                }
                else if (idx < -Locked - Pips)
                {
                    Deficit[idx].Disabled = false;
                    Deficit[idx].Visible = true;
                }
                else
                {
                    Deficit[idx].Visible = false;
                }
            }

            for (int idx = 0; idx < AddDeficit.Count; idx++)
            {
                if (idx < pipsRemaining)
                {
                    AddDeficit[idx].Visible = true;
                }
                else
                {
                    AddDeficit[idx].Visible = false;
                }
            }

            for (int idx = 0; idx < Gap.Count; idx++)
            {
                if (idx >= pipsRemaining)
                {
                    Gap[idx].Visible = true;
                }
                else
                {
                    Gap[idx].Visible = false;
                }
            }
        }

        Visible = true;
    }

    public void OnClick()
    {
    }
}