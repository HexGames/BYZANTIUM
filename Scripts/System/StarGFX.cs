using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

// Generated
[Tool]
public partial class StarGFX : Node3D
{
    [Export]
    private MeshInstance3D PlayerColor = null;
    [Export]
    private MeshInstance3D SystemSelected = null;
    [Export]
    private MeshInstance3D SectorSelected = null;
    [Export]
    private MeshInstance3D Hover = null;
    [Export]
    private MeshInstance3D HoverSector = null;
    [Export]
    private Node3D ShipFriendly = null;
    [Export]
    private MeshInstance3D ShipFriendly_Model = null;
    [Export]
    private MeshInstance3D ShipFriendly_SelectedPart = null;
    [Export]
    private MeshInstance3D ShipFriendly_SelectedTotal = null;
    [Export]
    private MeshInstance3D ShipFriendly_Hover = null;
    [Export]
    private Node3D ShipEnemy = null;
    [Export]
    private MeshInstance3D ShipEnemy_Model = null;
    [Export]
    private MeshInstance3D ShipEnemy_SelectedPart = null;
    [Export]
    private MeshInstance3D ShipEnemy_SelectedTotal = null;
    [Export]
    private MeshInstance3D ShipEnemy_Hover = null;
    [Export]
    public StarNode _Node = null;
    [Export]
    public UIGalaxySystem HUD = null;

    Game __Game;
    Game Game
    {
        get 
        {
            if (__Game != null)
            {
                return __Game;
            }
            else
            {
                __Game = GetNode<Game>("/root/Main/Game");
                return __Game;
            }
        }
    }

    public override void _Ready()
    {
        if (Engine.IsEditorHint()) return;

        // anti error hack
        CollisionObject3D collision_Sector = GetNode<CollisionObject3D>("Star/SectorArea3D");
        collision_Sector.InputEvent += SignalInputEventSector;
        collision_Sector.MouseEntered += OnHoverSector;
        collision_Sector.MouseExited += OnDehoverSector;
        CollisionObject3D collision = GetNode<CollisionObject3D>("Star/Area3D");
        collision.InputEvent += SignalInputEventStar;
        collision.MouseEntered += OnHoverStar;
        collision.MouseExited += OnDehoverStar;
        CollisionObject3D collision_ShipFriendly = GetNode<CollisionObject3D>("Ship_Friendly/Area3D");
        collision_ShipFriendly.InputEvent += SignalInputEventShipFriendly;
        collision_ShipFriendly.MouseEntered += OnHoverShipFriendly;
        collision_ShipFriendly.MouseExited += OnDehoverShipFriendly;
        CollisionObject3D collision_ShipEnemy = GetNode<CollisionObject3D>("Ship_Other/Area3D");
        collision_ShipEnemy.InputEvent += SignalInputEventShipEnemy;
        collision_ShipEnemy.MouseEntered += OnHoverShipEnemy;
        collision_ShipEnemy.MouseExited += OnDehoverShipEnemy;
    }

    public void SignalInputEventSector(Node camera, InputEvent inputEvent, Vector3 position, Vector3 normal, long shapeIdx)
    {
        return;
        if (_Node.Data.System?._Sector != null)
        {
            if (inputEvent is InputEventMouseButton mouseButtonEvent)
            {
                if (!mouseButtonEvent.IsPressed())
                {
                    // on mouse button release
                    if (mouseButtonEvent.ButtonIndex == MouseButton.Left)
                    {
                        //GD.Print("You clicked on " + Label.Text);
                        StarNode parentLocationNode = GetParent<StarNode>();
                        parentLocationNode.Select(true);
                    }
                }
            }
        }
    }

    public void OnHoverSector()
    {
        return;
        if (_Node.Data.System?._Sector != null)
        {
            HoverSector.Visible = true;
        }
    }

    public void OnDehoverSector()
    {
        return;
        HoverSector.Visible = false;
    }

    public void SignalInputEventStar(Node camera, InputEvent inputEvent, Vector3 position, Vector3 normal, long shapeIdx)
    {
        if (inputEvent is InputEventMouseButton mouseButtonEvent)
        {
            if (!mouseButtonEvent.IsPressed())
            {
                // on mouse button release
                if (mouseButtonEvent.ButtonIndex == MouseButton.Left)
                {
                    //GD.Print("You clicked on " + Label.Text);
                    StarNode parentLocationNode = GetParent<StarNode>();
                    parentLocationNode.Select();
                }
                // on mouse button release
                if (mouseButtonEvent.ButtonIndex == MouseButton.Right)
                {
                    //GD.Print("You clicked on " + Label.Text);
                    if (Game.Input.SelectedFleets.Count > 0 && Game.Input.SelectedFleets[0]._Player == Game.HumanPlayer)
                    {
                        Game.Input.MoveTo(_Node.Data);
                    }
                }
            }
        }
    }

    public void OnHoverStar()
    {
        Hover.Visible = true;
    }

    public void OnDehoverStar()
    {
        Hover.Visible = false;
    }

    public void SignalInputEventShipFriendly(Node camera, InputEvent inputEvent, Vector3 position, Vector3 normal, long shapeIdx)
    {
        if (inputEvent is InputEventMouseButton mouseButtonEvent)
        {
            if (!mouseButtonEvent.IsPressed())
            {
                // on mouse button release
                if (mouseButtonEvent.ButtonIndex == MouseButton.Left)
                {
                    //GD.Print("You clicked on " + Label.Text);
                    StarNode parentLocationNode = GetParent<StarNode>();
                    parentLocationNode.SelectFleet(true);
                }
                // on mouse button release
                if (mouseButtonEvent.ButtonIndex == MouseButton.Right)
                {
                    //GD.Print("You clicked on " + Label.Text);
                    if (Game.Input.SelectedFleets.Count > 0 && Game.Input.SelectedFleets[0]._Player == Game.HumanPlayer)
                    {
                        Game.Input.MoveTo(_Node.Data);
                    }
                }
            }
        }
    }

    public void OnHoverShipFriendly()
    {
        ShipFriendly_Hover.Visible = true;
    }

    public void OnDehoverShipFriendly()
    {
        ShipFriendly_Hover.Visible = false;
    }

    public void SignalInputEventShipEnemy(Node camera, InputEvent inputEvent, Vector3 position, Vector3 normal, long shapeIdx)
    {
        if (inputEvent is InputEventMouseButton mouseButtonEvent)
        {
            if (!mouseButtonEvent.IsPressed())
            {
                // on mouse button release
                if (mouseButtonEvent.ButtonIndex == MouseButton.Left)
                {
                    //GD.Print("You clicked on " + Label.Text);
                    StarNode parentLocationNode = GetParent<StarNode>();
                    parentLocationNode.SelectFleet(false);
                }
                // on mouse button release
                if (mouseButtonEvent.ButtonIndex == MouseButton.Right)
                {
                    //GD.Print("You clicked on " + Label.Text);
                    if (Game.Input.SelectedFleets.Count > 0 && Game.Input.SelectedFleets[0]._Player == Game.HumanPlayer)
                    {
                        Game.Input.MoveTo(_Node.Data);
                    }
                }
            }
        }
    }

    public void OnHoverShipEnemy()
    {
        ShipEnemy_Hover.Visible = true;
    }

    public void OnDehoverShipEnemy()
    {
        ShipEnemy_Hover.Visible = false;
    }

    public void RefreshPlayerColors()
    {
        PlayerData player = null;
        SystemData system = _Node.Data.System;
        if (system != null)
        {
            player = system._Sector._Player;
        }

        if (player != null)
        {
            Material material = PlayerColor.GetSurfaceOverrideMaterial(0).Duplicate(true) as Material;
            PlayerColor.SetSurfaceOverrideMaterial(0, material);
            if (material is StandardMaterial3D stdMaterial)
            {
                stdMaterial.AlbedoColor = player._Node.GFX;
            }
            PlayerColor.Visible = true;
        }
        else
        {
            PlayerColor.Visible = false;
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

    public void RefreshShips()
    {
        PlayerData player = null;
        SystemData system = _Node.Data.System;
        if (system != null)
        {
            player = system._Sector._Player;
        }

        Array<FleetData> fleets = Data.GetLinkFleetsData(_Node.Data._Data, Game.Map.Data);

        FleetData friendlyFleet = null;
        FleetData biggestNonFriendlyFleet = null; // first for the moment
        for (int idx = 0; idx < fleets.Count; idx++)
        {
            if (friendlyFleet == null && fleets[idx]._Player == Game.HumanPlayer)
            {
                friendlyFleet = fleets[idx];
            }

            if (biggestNonFriendlyFleet == null && fleets[idx]._Player != Game.HumanPlayer)
            {
                biggestNonFriendlyFleet = fleets[idx];
            }
        }

        if (player == null || player == Game.HumanPlayer)
        {
            if (friendlyFleet != null)
            {
                Material material = ShipFriendly_Model.GetSurfaceOverrideMaterial(0).Duplicate(true) as Material;
                ShipFriendly_Model.SetSurfaceOverrideMaterial(0, material);
                if (material is StandardMaterial3D stdMaterial)
                {
                    stdMaterial.AlbedoColor = friendlyFleet._Player._Node.GFX;
                    stdMaterial.Emission = friendlyFleet._Player._Node.GFX;
                }
                ShipFriendly_Model.Visible = true;
                ShipFriendly.Visible = true;
                ShipFriendly_Hover.Visible = false;
            }
            else
            {
                ShipFriendly.Visible = false;
            }

            if (biggestNonFriendlyFleet != null)
            {
                Material material = ShipEnemy_Model.GetSurfaceOverrideMaterial(0).Duplicate(true) as Material;
                ShipEnemy_Model.SetSurfaceOverrideMaterial(0, material);
                if (material is StandardMaterial3D stdMaterial)
                {
                    stdMaterial.AlbedoColor = biggestNonFriendlyFleet._Player._Node.GFX;
                    stdMaterial.Emission = biggestNonFriendlyFleet._Player._Node.GFX;
                }
                ShipEnemy_Model.Visible = true;
                ShipEnemy.Visible = true;
                ShipEnemy_Hover.Visible = false;
            }
            else
            {
                ShipEnemy.Visible = false;
            }
        }
        else
        {
            if (biggestNonFriendlyFleet != null)
            {
                Material material = ShipFriendly_Model.GetSurfaceOverrideMaterial(0).Duplicate(true) as Material;
                ShipFriendly_Model.SetSurfaceOverrideMaterial(0, material);
                if (material is StandardMaterial3D stdMaterial)
                {
                    stdMaterial.AlbedoColor = biggestNonFriendlyFleet._Player._Node.GFX;
                    stdMaterial.Emission = biggestNonFriendlyFleet._Player._Node.GFX;
                }
                ShipFriendly_Model.Visible = true;
                ShipFriendly.Visible = true;
                ShipFriendly_Hover.Visible = false;
            }
            else
            {
                ShipFriendly.Visible = false;
            }

            if (friendlyFleet != null)
            {
                Material material = ShipEnemy_Model.GetSurfaceOverrideMaterial(0).Duplicate(true) as Material;
                ShipEnemy_Model.SetSurfaceOverrideMaterial(0, material);
                if (material is StandardMaterial3D stdMaterial)
                {
                    stdMaterial.AlbedoColor = friendlyFleet._Player._Node.GFX;
                    stdMaterial.Emission = friendlyFleet._Player._Node.GFX;
                }
                ShipEnemy_Model.Visible = true;
                ShipEnemy.Visible = true;
                ShipEnemy_Hover.Visible = false;
            }
            else
            {
                ShipEnemy.Visible = false;
            }
        }
    }

    public void Select(bool system, bool sector, bool friendlyShips, bool enemyShips, bool partial = false)
    {
        SystemSelected.Visible = system;
        SectorSelected.Visible = sector;

        if (partial)
        {
            ShipFriendly_SelectedPart.Visible = friendlyShips;
            ShipFriendly_SelectedTotal.Visible = false;
            ShipEnemy_SelectedPart.Visible = enemyShips;
            ShipEnemy_SelectedTotal.Visible = false;
        }
        else
        {
            ShipFriendly_SelectedPart.Visible = friendlyShips;
            ShipFriendly_SelectedTotal.Visible = friendlyShips;
            ShipEnemy_SelectedPart.Visible = enemyShips;
            ShipEnemy_SelectedTotal.Visible = enemyShips;
        }
    }

    public void Deselect()
    {
        SystemSelected.Visible = false;
        SectorSelected.Visible = false;
        ShipFriendly_SelectedPart.Visible = false;
        ShipFriendly_SelectedTotal.Visible = false;
        ShipEnemy_SelectedPart.Visible = false;
        ShipEnemy_SelectedTotal.Visible = false;
    }
}
