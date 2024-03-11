using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIEconomyInfoItem : Panel
{
    // is beeing duplicated
    private RichTextLabel ValueLabel;
    private string ValueLabel_Original;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        ValueLabel = GetNode<RichTextLabel>("Value");
        ValueLabel_Original = ValueLabel.Text;
    }

    public void Refresh(string value)
    {
        ValueLabel.Text = ValueLabel_Original.Replace("$value", value);
    }
}