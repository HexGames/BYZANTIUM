using Godot;
using Godot.Collections;
using System.Linq;

// Editor
public partial class UIProgress : Control
{
    private ProgressBar ProgressCurrent = null;
    private ProgressBar ProgressNextTurn = null;

    public override void _Ready()
    {
        ProgressCurrent = GetNode<ProgressBar>("Panel/Progress");
        ProgressNextTurn = GetNode<ProgressBar>("Panel/NextTurn");
    }

    public void SetProgress(int current, int nextTurn, int max)
    {
        ProgressCurrent.MaxValue = max;
        ProgressCurrent.Value = current;
        ProgressNextTurn.MaxValue = max;
        ProgressNextTurn.Value = nextTurn;
    }
}
