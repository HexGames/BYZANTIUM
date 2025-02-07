using Godot;
using Godot.Collections;
using System.Linq;

// Editor
public partial class UIIconValue : Control
{
    public UIText Value;
    private static Color None = new Color("484848");
    private static Color Low = new Color("fbe52d");
    private static Color Medium = new Color("ff6600");
    private static Color High = new Color("ff2c2c");

    public override void _Ready()
    {
        Value = GetNode<UIText>("Value");
    }

    public void SetValue(int value, int level = 0)
    {
        Value.SetTextWithReplace("$v", value.ToString());

        if (level == 1) SelfModulate = Low;
        else if (level == 2) SelfModulate = Medium;
        else if (level == 3) SelfModulate = High;
        else SelfModulate = None;
    }
}
