using Godot;
using Godot.Collections;

// Generated
public partial class ConceptData : Node
{
    [Export]
    public string Name = "";
    [Export]
    public int Value = 0;
    [Export]
    public Array<ConceptData> Concepts = new Array<ConceptData>();
    [Export]
    public Array<PropertyData> Properties = new Array<PropertyData>();

    [Export]
    public LocationData Location = null;
    [Export]
    public PawnData Pawn = null;
}
