using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

[Tool]
public partial class MapNode : Node
{
    [ExportCategory("Generated")]
    [Export]
    public DefLibrary DefLibrary;
    [Export]
    public MapGenerator MapGenerator;
    [Export]
    public MapData Data = null;
    [Export]
    public string StarGFXName = "";
    [Export]
    public string PathGFXName = "";
    [Export]
    public Node StarsNode = null;
    [Export]
    public Node3D StarsGFX = null;
    [Export]
    public Node PathsNode = null;
    [Export]
    public Node PlayersNode = null;
    [Export]
    public Node FleetsNode = null;


    [ExportCategory("Runtime")]
    [Export]
    public Game Game = null;
    [Export]
    public string SaveName = "Save";

    [Export]
    public bool LoadMapTrigger
    {
        get => false;
        set
        {
            if (value)
            {
                LoadMap();
            }
        }
    }
    
    [Export]
    public bool SaveMapTrigger
    {
        get => false;
        set
        {
            if (value)
            {
                SaveMap();
            }
        }
    }

    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            Game = GetNode<Game>("/root/Main/Game");
        }
    }

    public void LoadMap()
    {
        Data.LoadMap("Saves/" + SaveName + ".sav", DefLibrary);
    }
    
    public void SaveMap()
    {
        Data.SaveMap("Saves/" + SaveName + ".sav", DefLibrary);
    }

    public void SaveMap_Progressive()
    {
        Data.SaveMap_Progressive("Saves/" + SaveName + "/", DefLibrary);
    }

    public void ClearContainers()
    {
        while (StarsNode.GetChildCount(true) > 0)
        {
            Node child = StarsNode.GetChild(0, true);
            StarsNode.RemoveChild(child);
            child.Free();
        }
        while (StarsGFX.GetChildCount(true) > 0)
        {
            Node child = StarsGFX.GetChild(0, true);
            StarsGFX.RemoveChild(child);
            child.Free();
        }

        while (PathsNode.GetChildCount(true) > 0)
        {
            Node child = PathsNode.GetChild(0, true);
            PathsNode.RemoveChild(child);
            child.Free();
        }

        while (PlayersNode.GetChildCount(true) > 0)
        {
            Node child = PlayersNode.GetChild(0, true);
            PlayersNode.RemoveChild(child);
            child.Free();
        }

        while (FleetsNode.GetChildCount(true) > 0)
        {
            Node child = FleetsNode.GetChild(0, true);
            FleetsNode.RemoveChild(child);
            child.Free();
        }
    }

    public void OnBackgoundInputEvent(Node camera, InputEvent inputEvent, Vector3 position, Vector3 normal, int shapeIdx)
    {
        if (inputEvent is InputEventMouseButton mouseButtonEvent)
        {
            if (!mouseButtonEvent.IsPressed())
            {
                // on mouse button release
                if (mouseButtonEvent.ButtonIndex == MouseButton.Left)
                {
                    //GD.Print("You clicked on " + Label.Text);
                    //Game.Input.DeselectAll();
                    Game.Input.DeselectOneStep();
                }
                if (mouseButtonEvent.ButtonIndex == MouseButton.Right)
                {
                    //GD.Print("You clicked on " + Label.Text);
                    //DeselectAll();
                    //Game.Input.DeselectOneStep();
                    Game.Input.DeselectOneStep();
                }
            }
        }
    }
}
