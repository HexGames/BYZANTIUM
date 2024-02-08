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
    public Node SystemsNode = null;
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
        Data.LoadMap(SaveName, DefLibrary);
    }
    
    public void SaveMap()
    {
        Data.SaveMap(SaveName, DefLibrary);
    }

    public void ClearContainers()
    {
        while (SystemsNode.GetChildCount(true) > 0)
        {
            Node child = SystemsNode.GetChild(0, true);
            SystemsNode.RemoveChild(child);
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
                    Game.Input.DeselectLocation();
                }
            }
        }
    }
}
