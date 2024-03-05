using Godot;
using Godot.Collections;
using System.Collections.Generic;

//[Tool]
public partial class UIBudgetItemUpkeep : Control // not used !!!
{
    // is beeing duplicated
    private UIBudget Parent = null;

    private RichTextLabel ValueLabel = null;
    private string ValueLabel_Original = null;
    //private ColorRect ValueBar = null;

    [ExportCategory("Runtime")]
    [Export]
    public int Value = 0;
    public int ValueMax = 0;

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

            Visible = false;
        }
    }

    public void Refresh(int value, int valueMax)
    {
        Value = value;
        ValueMax = valueMax;

        ValueLabel.Text = ValueLabel_Original.Replace("$value", value.ToString());

        //ValueBar.CustomMinimumSize = new Vector2(ValueBar.CustomMinimumSize.X, 112 * value / valueMax);

        Visible = true;
    }

    public void OnClick()
    {
    }
}