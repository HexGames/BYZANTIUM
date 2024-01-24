using Godot;
using Godot.Collections;

public partial class UISystemPlanet : Button
{
    [ExportCategory("Links")]
    [Export]
    public TextureRect PlanetImage = null;
    [Export]
    public TextureRect MoonOverlayImage = null;
    [Export]
    public Label PlanetName = null;
    [Export]
    public bool Selected = false;

    [ExportCategory("Runtime")]
    [Export]
    public DataBlock _Data = null;

    Game Game;
    UISystem Parent = null;
    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");
        Parent = GetNode<UISystem>("../../../");
    }

    public void Refresh( LocationData system, DataBlock planetSelected )
    {
        _Data = planetSelected;
        if (_Data == null) return;

        PlanetName.Text = _Data.ValueS;

        DataBlock typeData = _Data.GetSub("Type");
        float sizeIncremnt = 0.035f;
        if (typeData?.ValueS == "GasGiant") sizeIncremnt = 0.05f;


        if (_Data.ValueS == "Star" || _Data.ValueS == "Outer_System" || _Data.ValueS == "Asteroid_Belt")
        {
            PlanetImage.Texture = Game.Def.UIPlanets.GetPlanetTexture(_Data.ValueS);

            PlanetImage.AnchorLeft = 0.0f;
            PlanetImage.AnchorTop = 0.0f;
            PlanetImage.AnchorRight = 1.0f;
            PlanetImage.AnchorBottom = 1.0f;
        }
        else if (_Data.GetSub("Custom") != null)
        {
            PlanetImage.Texture = Game.Def.UIPlanets.GetPlanetTexture(_Data.ValueS);

            DataBlock sizeData = _Data.GetSub("Size");
            if (sizeData != null)
            {
                PlanetImage.AnchorLeft = 0.45f - sizeIncremnt * sizeData.ValueI;
                PlanetImage.AnchorTop = 0.45f - sizeIncremnt * sizeData.ValueI;
                PlanetImage.AnchorRight = 0.55f + sizeIncremnt * sizeData.ValueI;
                PlanetImage.AnchorBottom = 0.55f + sizeIncremnt * sizeData.ValueI;
            }
        }
        else
        { 
            PlanetImage.Texture = Game.Def.UIPlanets.GetPlanetTexture(typeData?.ValueS);

            DataBlock sizeData = _Data.GetSub("Size");
            if (sizeData != null)
            {
                PlanetImage.AnchorLeft = 0.45f - sizeIncremnt * sizeData.ValueI;
                PlanetImage.AnchorTop = 0.45f - sizeIncremnt * sizeData.ValueI;
                PlanetImage.AnchorRight = 0.55f + sizeIncremnt * sizeData.ValueI;
                PlanetImage.AnchorBottom = 0.55f + sizeIncremnt * sizeData.ValueI;
            }
        }

        if (_Data.GetSub("Moon") != null)
        {
            this.Icon = Game.Def.UIPlanets.BacgroundMoonTexture;
            MoonOverlayImage.Visible = true;
        }
        else
        {
            this.Icon = Game.Def.UIPlanets.BacgroundTexture;
            MoonOverlayImage.Visible = false;
        }
    }

    public void OnHoverEnter()
    {
        if (Parent.PlanetSelected != this) PlanetName.AnchorTop = 0.6f;

        //if (Parent.PlanetSelected != null && Parent.PlanetSelected != this)
        //    Parent.PlanetSelected.PlanetName.AnchorTop = 0.9f;
    }

    public void OnHoverExit()
    {
        PlanetName.AnchorTop = 0.8f;

        //if (Parent.PlanetSelected != null && Parent.PlanetSelected != this)
        //    Parent.PlanetSelected.PlanetName.AnchorTop = 0.8f;
    }

    public void OnSelect()
    {
        if (Selected) return;

        CustomMinimumSize = new Vector2(256, 256);
        PlanetName.AnchorTop = 0.8f;

        Selected = true;
        Parent.Select( this );
    }


    public void Deselect()
    {
        CustomMinimumSize = new Vector2(128, 128);

        Selected = false;
    }
}