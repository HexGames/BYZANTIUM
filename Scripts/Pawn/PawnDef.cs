using Godot;
using System;

// Generated
[GlobalClass]
[Tool]
public partial class PawnDef : Resource
{
    [Export]
    public string PawnName = "Alexander";
    [Export]
    public int Age = 38;
    [Export]
    public int Power = 9;
    [Export]
    public string StartingLocation = "Pella";

    // workaround
    [Export]
    public string FilePath = ""; // ?HEX? 

    public PawnDef() {}
}
