using Godot;
using Godot.Collections;
using System.ComponentModel;

public partial class UIText : RichTextLabel
{
    [Export]
    public string Original = "";

    public override void _Ready()
    {
        if (Original == "") Original = Text;
    }
}