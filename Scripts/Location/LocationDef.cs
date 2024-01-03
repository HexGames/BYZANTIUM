using Godot;
using System;

// Generated
[GlobalClass]
[Tool]
public partial class LocationDef : Resource
{
    [Export]
    public string LocationName = "Bucharest";
    [Export]
    public string Province = "Ilfov";
    [Export]
    public Vector2 Positon = new Vector2( 0, 0 );
    [Export]
    public int Population = 10;

    // workaround
    [Export]
    public string FilePath = ""; // ?HEX? 

    public LocationDef() {}
}
