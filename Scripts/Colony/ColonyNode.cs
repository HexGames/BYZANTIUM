using Godot;
using System;

// Generated
[Tool]
public partial class ColonyNode : Node
{
    [ExportCategory("Generated")]
    [Export]
    public ColonyData Data = null;

    [ExportCategory("Runtime")]
    [Export]
    public Game Game = null;


    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            Game = GetNode<Game>("/root/Main/Game");
        }
    }

    public void Select()
    {
        ActionColonyBuild.RefreshColonyActions(Data, Game.Def);
    }

    public void Deselect()
    {
        Data.ActionsBuildPossible.Clear();
    }
}
