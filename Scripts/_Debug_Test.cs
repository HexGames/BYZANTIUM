using Godot;

[Tool]
public partial class _Debug_Test : Node
{
    [Export]
    public bool Test
    {
        get => false;
        set
        {
            if (value)
            {
                TestFunc();
            }
        }
    }

    public void TestFunc()
    {
        //var dg = GetNode<DefGenerator>("DefGenerator");
        //GD.Print("Test DefGenerator." + dg.DefLibrary.); 
        //var LocationNode = GetNode<Node>("/root/Game");
        //var tree  = GetTree();
        //var Location = tree.EditedSceneRoot.GetNode<LocationNode>("Game/Locations/Avalon");
        //var Location = (LocationNode)LocationNode;
        //LocationNode Location = GetTree().Root.GetNode("Game").GetNode<LocationNode>("Game/Locations/Avalon");
        //GD.Print("Test Location Avalon.Def - " + Location.Def.FilePath);
        //GD.Print("Test Location Avalon.Data - " + Location.Data.Population.ToString());
    }
}
