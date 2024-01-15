using Godot;
using System;

// Generated
[GlobalClass]
[Tool]
public partial class LocationDef : Resource
{
    [Export]
    public Vector2 Positon = new Vector2( 0, 0 );

    // workaround
    [Export]
    public string FilePath = ""; // ?HEX? 

    public LocationDef() {}
}
