using Godot;
using Godot.Collections;
using System.Linq;

// Editor
public partial class UIProgress : Control
{
    [Export]
    public ProgressBar ProgressCurrent = null;
    [Export]
    public ProgressBar ProgressNextTurn = null;

    public void Set(int current, int nextTurn, int max)
    {
        ProgressCurrent.MaxValue = max;
        ProgressCurrent.Value = current;
        ProgressNextTurn.MaxValue = max;
        ProgressNextTurn.Value = nextTurn;
    }
}
