using Godot;
using Godot.Collections;
using System.Linq;

// Editor
public partial class UIIconValue : Control
{
    [Export]
    public TextureRect Icon;
    [Export]
    public UIText Value;
    [Export]
    public Color None;
    [Export]
    public Color Low;
    [Export]
    public Color Medium;
    [Export]
    public Color High;

    public void Set(int value, int level = 0)
    {
        Value.SetTextWithReplace("$v", Value.ToString());

        if (level == 1) Icon.SelfModulate = Low;
        else if (level == 2) Icon.SelfModulate = Medium;
        else if (level == 3) Icon.SelfModulate = High;
        else Icon.SelfModulate = None;
    }
}
