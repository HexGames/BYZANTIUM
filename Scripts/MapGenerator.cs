using Godot;
using System;

// Editor
[Tool]
public partial class MapGenerator : Node
{
    [Export]
    public DefLibrary DefLibrary;
    //private Resource DataLibraryRes = null;
    //private DefLibrary DataLibrary
    //{
    //    get
    //    {
    //        return (DefLibrary)DataLibraryRes;
    //    }
    //}
    [Export]
    public Node CitiesNode = null;

    [Export]
    public bool GenerateMap
    {
        get => false;
        set
        {
            if (value)
            {
                GenerateMapFunc();
            }
        }
    }

    public void ClearMap()
    {
        while (CitiesNode.GetChildCount(true) > 0)
        {
            Node child = CitiesNode.GetChild(0, true);
            CitiesNode.RemoveChild(child);
            child.Free();
        }
    }

    public void GenerateMapFunc()
    {
        ClearMap();

        for ( int idx = 0; idx < DefLibrary.Cities.Count; idx++ )
        {
            CityNode node = new CityNode();
            node.Def = DefLibrary.Cities[idx];
            node.Name = node.Def.CityName;

            CitiesNode.AddChild(node, true, InternalMode.Back);
            node.Owner = GetTree().EditedSceneRoot;

            node.Data = new CityData();
            node.Data.Name = node.Name + "Data";
            node.Data.Population = node.Def.Population;
            node.Data.Prosperity = 15;
            node.AddChild(node.Data);
            node.Data.Owner = GetTree().EditedSceneRoot;

            PackedScene gfxScene = GD.Load<PackedScene>("res:///3DPrefabs/City.tscn");
            Node gfxNode = gfxScene.Instantiate();
            gfxNode.Name = node.Name + "GFX";
            node.AddChild(gfxNode);
            gfxNode.Owner = GetTree().EditedSceneRoot;
            gfxNode.GetParent().SetEditableInstance(gfxNode, true);

            node.GFX = gfxNode as CityGFX;
            node.GFX.Position = new Vector3( node.Def.Positon.X, 0.0f, node.Def.Positon.Y);
            node.GFX.SetCityName(node.Name);
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
