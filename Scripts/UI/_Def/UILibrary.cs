using Godot;
using Godot.Collections;
using System.Linq;

// Editor
public partial class UILibrary : Node
{
    [Export]
    public Array<Color> PlayersColors = new Array<Color>();
    public Color GetPlayerColor(int playerID)
    {
        return PlayersColors[playerID % PlayersColors.Count];
    }
}
