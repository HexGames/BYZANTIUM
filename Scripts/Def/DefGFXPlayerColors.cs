using Godot;
using Godot.Collections;
using System;

[Tool]
public partial class DefGFXPlayerColors : Resource
{
    [Export]
    public Array<Color> PrimaryColors = new Array<Color>();
}