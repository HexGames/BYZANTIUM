using Godot;
using Godot.Collections;

public partial class UIGalaxy : Control
{
    [ExportCategory("Links")]
    [Export]
    public Array<UIGalaxySystem> Systems = new Array<UIGalaxySystem>();
    [Export]
    public Label CurrentTurn = null;

    [ExportCategory("Runtime")]
    [Export]
    public DataBlock _Data = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        Init();
    }
    public void Init()
    {
        // delete surplus
        while (Systems.Count > Mathf.Max(Game.Map.Data.Systems.Count, 1))
        {
            Node node = Systems[Systems.Count - 1];
            Systems[0].GetParent().RemoveChild(node);
            node.Free();
        }

        // grow
        while (Systems.Count < Game.Map.Data.Systems.Count)
        {
            UIGalaxySystem newSys = Systems[0].Duplicate(7) as UIGalaxySystem;
            Systems[0].GetParent().AddChild(newSys);
            Systems.Add(newSys);
        }

        // update
        for ( int idx = 0; idx < Systems.Count; idx++ ) 
        {
            if (idx < Game.Map.Data.Systems.Count)
            {
                Systems[idx].Refresh(Game.Map.Data.Systems[idx]._Node);
            }
        }
    }

    public void Refresh()
    {
        CurrentTurn.Text = "Current Turn: " + Game.Map.Data.Turn.ToString();
    }

    public void OnEndTurn()
    {
        Game.TurnLoop.CurrentPlayerData.TurnFinished = true;
        Game.TurnLoop.WaitingForHuman = false;
    }
}