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
        //var cityNode = GetNode<Node>("/root/Game");
        var tree  = GetTree();
        var city = tree.EditedSceneRoot.GetNode<CityNode>("Game/Cities/Avalon");
        //var city = (CityNode)cityNode;
        //CityNode city = GetTree().Root.GetNode("Game").GetNode<CityNode>("Game/Cities/Avalon");
        GD.Print("Test city Avalon.Def - " + city.Def.FilePath);
        GD.Print("Test city Avalon.Data - " + city.Data.Population.ToString());
    }
}
