using Godot;
using Godot.Collections;
using System.Linq;

// Editor
public partial class UIVisualMask : Panel
{
    private StyleBoxFlat Stylebox = null;
    private Color ColorFull;
    private Color ColorHidden;

    public override void _Ready()
    {
        Stylebox = GetThemeStylebox("Panel") as StyleBoxFlat;

        ColorFull = Stylebox.BgColor;
        ColorFull.A = 0.75f;

        ColorHidden = Stylebox.BgColor;
        ColorHidden.A = 0.0f;
    }

    public void OnHoverEnter()
    {
        Stylebox.BgColor = ColorFull;
    }

    public void OnHoverExit()
    {
        Stylebox.BgColor = ColorHidden;
    }
}
