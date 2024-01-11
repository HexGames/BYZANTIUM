using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Generated
[GlobalClass]
public partial class DataBlock : Resource
{
    [Export]
    public int Type = 0;
    [Export]
    public string Name = "";
    [Export]
    public int ValueI = 0;
    [Export]
    public string ValueS = "";
    [Export]
    public Array<DataBlock> Subs = new Array<DataBlock>();

    [Export]
    public LocationData Location = null;
    [Export]
    public PawnData Pawn = null;

    public string ValueToString()
    {
        Data.BaseType baseType = (Data.BaseType)(Type/10000);

        switch(baseType)
        {
            case Data.BaseType.INT: return ValueI.ToString();
            case Data.BaseType.STRING: return ValueS;
        }

        return "";
    }
}
