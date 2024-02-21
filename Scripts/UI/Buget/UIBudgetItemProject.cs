using Godot;
using Godot.Collections;
using System.Collections.Generic;

//[Tool]
public partial class UIBudgetItemProject : Control
{
    // is beeing duplicated
    private UIBudget Parent = null;

    private RichTextLabel NameLabel = null;
    private string NameLabel_Original;
    private RichTextLabel ValueLabel = null;
    private string ValueLabel_Original;
    //private ColorRect ValueBar = null;

    private Array<Button> Add = new Array<Button>();
    private Array<Button> Pip = new Array<Button>();

    [ExportCategory("Runtime")]
    [Export]
    public DataBlock _Data;
    public int Locked = 0;
    public int Pips = 0;
    public int PipsRemaining = 0;
    public int Value = 0;
    public int ValueMax = 0;

    Game Game;

    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            Game = GetNode<Game>("/root/Main/Game");

            Parent = GetNode<UIBudget>("../../../../../../../");

            NameLabel = GetNode<RichTextLabel>("Color_2/Name");
            NameLabel_Original = NameLabel.Text;
            ValueLabel = GetNode<RichTextLabel>("Color_3/Value");
            ValueLabel_Original = ValueLabel.Text;
            //ValueBar = GetNode<ColorRect>("Color_2/Icon/Bar");

            Add.Clear();
            for (int id = 1; id <= 10; id++)
            {
                Add.Add(GetNode<Button>("Main/VBoxContainer/VBoxContainer/Add_" + id.ToString()));
            }
            Pip.Clear();
            for (int id = 1; id <= 10; id++)
            {
                Pip.Add(GetNode<Button>("Main/VBoxContainer/VBoxContainer/Pip_" + id.ToString()));
            }

            Visible = false;
        }
    }

    public void Refresh(DataBlock data, int pipsRemaining, int value, int valueMax)
    {
        _Data = data;
        Locked = _Data.GetSub("Locked").ValueI;
        Pips = _Data.GetSub("Value").ValueI;

        PipsRemaining = pipsRemaining;
        Value = value;
        ValueMax = valueMax;

        NameLabel.Text = NameLabel_Original.Replace("$project", _Data.ValueS );
        ValueLabel.Text = ValueLabel_Original.Replace( "$value", value.ToString() );

        //ValueBar.CustomMinimumSize = new Vector2(ValueBar.CustomMinimumSize.X, 112 * value / valueMax);

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

        Visible = true;
    }

    public void OnClick()
    {
    }
}