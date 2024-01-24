using Godot;
using Godot.Collections;
using System.Linq;

// Editor
public partial class UILibrary : Node
{
    public Godot.Color GetPlayerColor(string playerName)
    {
        switch (playerName[playerName.Length - 1])
        {
            case '0': return new Godot.Color("bb0000");
            case '1': return new Godot.Color("00bb00");
            case '2': return new Godot.Color("00bb00");
            case '3': return new Godot.Color("aaaa00");
            case '4': return new Godot.Color("aa00aa");
            case '5': return new Godot.Color("00aaaa");
            case '6': return new Godot.Color("996633");
            case '7': return new Godot.Color("669933");
            case '8': return new Godot.Color("339966");
            case '9': return new Godot.Color("336699");
        }

        return new Godot.Color("666666");
    }
}
