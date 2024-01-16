using Godot;
using Godot.Collections;

public partial class UISystemPlanet : Button
{
    [Export]
    public TextureRect PlanetImage = null;
    [Export]
    public Label PlanetName = null;
    [Export]
    public bool Selected = false;

    [Export]
    public DataBlock _Data = null;

    UISystem Parent = null;
    public override void _Ready()
    {
        Parent = GetNode<UISystem>("../../../");
    }

    public void Refresh( LocationData system, DataBlock planetSelected )
    {
        _Data = planetSelected;
        if (_Data == null) return;

        PlanetName.Text = _Data.ValueS;

        DataBlock sizeData = _Data.GetSub("PlanetSize");
        if (sizeData != null)
        {
            PlanetImage.AnchorLeft = 0.45f - 0.05f * sizeData.ValueI;
            PlanetImage.AnchorTop = 0.45f - 0.05f * sizeData.ValueI;
            PlanetImage.AnchorRight = 0.55f + 0.05f * sizeData.ValueI;
            PlanetImage.AnchorBottom = 0.55f + 0.05f * sizeData.ValueI;
        }
    }

    public void OnHoverEnter()
    {
        PlanetName.AnchorTop = 0.6f;

        if (Parent.PlanetSelected != null && Parent.PlanetSelected != this)
            Parent.PlanetSelected.PlanetName.AnchorTop = 0.9f;
    }

    public void OnHoverExit()
    {
        PlanetName.AnchorTop = 0.8f;

        if (Parent.PlanetSelected != null && Parent.PlanetSelected != this)
            Parent.PlanetSelected.PlanetName.AnchorTop = 0.8f;
    }

    public void OnSelect()
    {
        if (Selected) return;

        CustomMinimumSize = new Vector2(256, 256);

        Selected = true;
        Parent.Select( this );
    }


    public void Deselect()
    {
        CustomMinimumSize = new Vector2(128, 128);

        Selected = false;
    }
}