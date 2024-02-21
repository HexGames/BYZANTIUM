using Godot;
using Godot.Collections;
using System.Collections.Generic;

//[Tool]
public partial class UIBudgetItemBuilding : Control // UNUSED !!!
{
    // is beeing duplicated
    private UIBudget Parent = null;

    private ColorRect IconBg = null;
    private TextureRect Icon = null;
    private ColorRect ValueBg = null;
    private RichTextLabel ValueLabel = null;
    private ColorRect ValueBarBg = null;

    private Array<Button> Add = new Array<Button>();
    private Array<Button> Pip = new Array<Button>();

    [ExportCategory("Runtime")]
    [Export]
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

            IconBg = GetNode<ColorRect>("Main/VBoxContainer/Color_1");
            Icon = GetNode<TextureRect>("Main/VBoxContainer/Color_1/ResourceIcon");
            ValueBg = GetNode<ColorRect>("Color_2");
            ValueLabel = GetNode<RichTextLabel>("Color_2/Value");
            ValueBarBg = GetNode<ColorRect>("Color_2/ColorBar");

            Add.Clear();
            for (int id = 1; id <= 6; id++)
            {
                Add.Add( GetNode<Button>("Main/VBoxContainer/VBoxContainer/Add_" + id.ToString()) );
            }
            Pip.Clear();
            for (int id = 1; id <= 6; id++)
            {
                Pip.Add(GetNode<Button>("Main/VBoxContainer/VBoxContainer/Pip_" + id.ToString()));
            }
            
            Visible = false;
        }
    }

    public void Refresh(int pips, int pipsRemaining, int value, int valueMax)
    {
        Pips = pips;
        PipsRemaining = pipsRemaining;
        Value = value;
        ValueMax = valueMax;

        Visible = true;
    }

    public void OnClick()
    {
    }
}