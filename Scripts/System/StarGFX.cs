using Godot;
using Godot.Collections;
using System.Collections.Generic;

// Generated
[Tool]
public partial class StarGFX : Node3D
{
    [Export]
    private Array<MeshInstance3D> PlayerColorBands = new Array<MeshInstance3D>();
    [Export]
    public StarNode _Node = null;
    [Export]
    public UIGalaxySystem HUD = null;

    public override void _Ready()
    {
        if (Engine.IsEditorHint()) return;

        // anti error hack
        CollisionObject3D collision = GetNode<CollisionObject3D>("Star/Area3D");
        collision.InputEvent += SignalInputEvent;
    }

    public void SignalInputEvent(Node camera, InputEvent inputEvent, Vector3 position, Vector3 normal, long shapeIdx)
    {
        if (inputEvent is InputEventMouseButton mouseButtonEvent)
        {
            if ( !mouseButtonEvent.IsPressed() )
            {
                // on mouse button release
                if (mouseButtonEvent.ButtonIndex == MouseButton.Left)
                {
                    //GD.Print("You clicked on " + Label.Text);
                    StarNode parentLocationNode = GetParent<StarNode>();
                    parentLocationNode.Select();
                }
            }
        }
    }

    public void RefreshPlayerColors()
    {
        SystemData system = _Node.Data.System;
        PlayerData player = null;
        if (system != null)
        {
            player = system._Sector._Player;
        }

        if (player != null)
        {
            for (int idx = 0; idx < PlayerColorBands.Count; idx++)
            {
                Material material = PlayerColorBands[idx].GetSurfaceOverrideMaterial(0).Duplicate(true) as Material;
                PlayerColorBands[idx].SetSurfaceOverrideMaterial(0, material);
                if (material is StandardMaterial3D stdMaterial)
                {
                    stdMaterial.AlbedoColor = player._Node.GFX;
                }
                PlayerColorBands[idx].Visible = true;
            }
        }
        else
        {
            for (int idx = 0; idx < PlayerColorBands.Count; idx++)
            {
                PlayerColorBands[idx].Visible = false;
            }
        }

        /*List<PlayerData> players = _Node.Data.GetPlayersPresentinSystem();

        switch (players.Count)
        {
            case 0:
                {
                    for (int idx = 0; idx < PlayerColorBands.Count; idx++)
                    {
                        PlayerColorBands[idx].Visible = false;
                    }
                    break;
                }
            case 1:
            case 2:
            case 3:
                {

                    for (int idx = 0; idx < PlayerColorBands.Count; idx++)
                    {
                        Material material = PlayerColorBands[idx].GetSurfaceOverrideMaterial(0).Duplicate(true) as Material;
                        PlayerColorBands[idx].SetSurfaceOverrideMaterial(0, material);
                        if (material is StandardMaterial3D stdMaterial)
                        {
                            stdMaterial.AlbedoColor = players[idx % players.Count]._Node.GFX;
                        }
                        PlayerColorBands[idx].Visible = true;
                    }
                    break;
                }
            case 4:
            case 5:
                {
                    for (int idx = 0; idx < PlayerColorBands.Count; idx++)
                    {
                        if (idx < players.Count)
                        {
                            Material material = PlayerColorBands[idx].GetSurfaceOverrideMaterial(0).Duplicate(true) as Material;
                            PlayerColorBands[idx].SetSurfaceOverrideMaterial(0, material);
                            if (material is StandardMaterial3D stdMaterial)
                            {
                                stdMaterial.AlbedoColor = players[idx]._Node.GFX;
                            }
                            PlayerColorBands[idx].Visible = true;
                        }
                        else
                        {
                            PlayerColorBands[idx].Visible = false;
                        }
                    }
                    break;
                }
        }*/
    }
}
