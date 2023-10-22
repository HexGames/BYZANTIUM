using Godot;

[Tool]
public partial class _Autorun : Node
{
    //public override void _Ready()
    //{
    //    // Initialization of the plugin goes here.
    //    OnEditorStart();
    //}
    //
    //public override void _ExitTree()
    //{
    //    // Clean-up of the plugin goes here.
    //    OnEditorClose();
    //}


    /*[Export]
    public bool GenerateAll
    {
        get => false;
        set
        {
            if (value)
            {
                OnEditorStart();
            }
        }
    }

    [Export]
    public bool ClearAll
    {
        get => false;
        set
        {
            if (value)
            {
                OnEditorClose();
            }
        }
    }

    public void OnEditorStart()
    {
        SceneTree tree  = GetTree();

        if (tree == null || tree.EditedSceneRoot == null) return;

        DefGenerator defGen = tree.EditedSceneRoot.GetNode<DefGenerator>("Generators/DefGenerator");
        defGen.GenerateCityDefFunc();

        MapGenerator mapGen = tree.EditedSceneRoot.GetNode<MapGenerator>("Generators/MapGenerator");
        mapGen.GenerateMapFunc();
    }

    public void OnEditorClose()
    {
        SceneTree tree  = GetTree();

        if (tree == null || tree.EditedSceneRoot == null) return;

        MapGenerator mapGen = tree.EditedSceneRoot.GetNode<MapGenerator>("Generators/MapGenerator");
        mapGen.ClearMap();

        DefGenerator defGen = tree.EditedSceneRoot.GetNode<DefGenerator>("Generators/DefGenerator");
        defGen.ClearCityDef();
    }*/
}
