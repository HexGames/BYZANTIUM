using Godot;
using Godot.Collections;
using System.Collections.Generic;

//[Tool]
public partial class UIPawnListPlanet : Control
{
    // is beeing duplicated
    private UIPawnList Parent = null;
    private Label PlanetName = null;
    private TextureRect Simple = null;
    private TextureRect Planet = null;
    private TextureRect PlanetRings = null;
    private TextureRect ParentPlanet = null;
    private TextureRect ParentPlanetRings = null;
    private TextureRect Station = null;

    [ExportCategory("Runtime")]
    [Export]
    public bool Selected = false;

    [Export]
    public DataBlock _PlanetData = null;
    [Export]
    public DataBlock _ParentPlanetData = null;

    //[Export]
    //public bool AutoLink
    //{
    //    get => false;
    //    set
    //    {
    //        if (value)
    //        {
    //            AutoLinkFunc();
    //        }
    //    }
    //}

    Game Game;

    /*public void AutoLinkFunc()
    {
    }*/

    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            Game = GetNode<Game>("/root/Main/Game");
            Parent = GetNode<UIPawnList>("../../");
            PlanetName = GetNode<Label>("Mask/NameBackground/Name");
            Simple = GetNode<TextureRect>("Mask/Simple");
            Planet = GetNode<TextureRect>("Mask/Planet");
            PlanetRings = GetNode<TextureRect>("Mask/Planet/Rings");
            ParentPlanet = GetNode<TextureRect>("Mask/Parent");
            ParentPlanetRings = GetNode<TextureRect>("Mask/Parent/Rings");
            Station = GetNode<TextureRect>("Mask/Station");
            
            Visible = false;
        }
    }

    public void Refresh(DataBlock planetData, DataBlock parentPlanetData)
    {
        _PlanetData = planetData;
        _ParentPlanetData = parentPlanetData;
        Name = _PlanetData.ValueS + "_UI";

        //Planet.Texture = 

        DataBlock typeData = _PlanetData.GetSub("Type");
        //float sizeIncremnt = 0.035f;
        //if (typeData?.ValueS == "GasGiant") sizeIncremnt = 0.05f;

        if (_PlanetData.ValueS == "Star" || _PlanetData.ValueS == "Outer_System" || _PlanetData.ValueS == "Asteroid_Belt")
        {
            Simple.Texture = Game.Def.UIPlanets.GetPlanetTexture(_PlanetData.ValueS);
            Simple.Visible = true;

            if (_PlanetData.GetSub("Link:Player:Colony") != null)
            {
                Station.Visible = true;
            }
            else
            {
                Station.Visible = false;
            }

            Planet.Visible = false;
            ParentPlanet.Visible = false;
        }
        else
        {
            Simple.Visible = false;
            Station.Visible = false;

            // refresh planet
            {
                if (_PlanetData.GetSub("Custom") != null)
                {
                    Planet.Texture = Game.Def.UIPlanets.GetPlanetTexture(_PlanetData.ValueS);

                    DataBlock sizeData = _PlanetData.GetSub("Size");
                    if (sizeData != null)
                    {
                        Planet.CustomMinimumSize = new Vector2(32 * sizeData.ValueI, 32 * sizeData.ValueI);
                        PlanetRings.CustomMinimumSize = new Vector2(48 * sizeData.ValueI, 16 * sizeData.ValueI);
                    }
                    else
                    {
                        Planet.CustomMinimumSize = new Vector2(128, 128);
                        PlanetRings.CustomMinimumSize = new Vector2(192, 64);
                    }
                }
                else
                {
                    Planet.Texture = Game.Def.UIPlanets.GetPlanetTexture(typeData?.ValueS);

                    DataBlock sizeData = _PlanetData.GetSub("Size");
                    if (sizeData != null)
                    {
                        Planet.CustomMinimumSize = new Vector2(32 * sizeData.ValueI, 32 * sizeData.ValueI);
                        PlanetRings.CustomMinimumSize = new Vector2(48 * sizeData.ValueI, 16 * sizeData.ValueI);
                    }
                    else
                    {
                        Planet.CustomMinimumSize = new Vector2(128, 128);
                        PlanetRings.CustomMinimumSize = new Vector2(192, 64);
                    }
                }

                if (_PlanetData.GetSub("Rings") != null)
                {
                    PlanetRings.Visible = true;
                }
                else
                {
                    PlanetRings.Visible = false;
                }
            }
            Planet.Visible = true;

            // refresh parent planet
            if (_ParentPlanetData != null)
            {
                DataBlock parentTypeData = _ParentPlanetData.GetSub("Type");
                if (_ParentPlanetData.GetSub("Custom") != null)
                {
                    ParentPlanet.Texture = Game.Def.UIPlanets.GetPlanetTexture(_ParentPlanetData.ValueS);
                }
                else
                {
                    ParentPlanet.Texture = Game.Def.UIPlanets.GetPlanetTexture(parentTypeData?.ValueS);
                }

                DataBlock sizeData = _ParentPlanetData.GetSub("Size");
                if (sizeData != null)
                {
                    ParentPlanet.CustomMinimumSize = new Vector2(32 * sizeData.ValueI, 32 * sizeData.ValueI);
                    ParentPlanetRings.CustomMinimumSize = new Vector2(48 * sizeData.ValueI, 16 * sizeData.ValueI);
                }
                else
                {
                    ParentPlanet.CustomMinimumSize = new Vector2(128, 128);
                    ParentPlanetRings.CustomMinimumSize = new Vector2(192, 64);
                }

                if (_ParentPlanetData.GetSub("Rings") != null)
                {
                    ParentPlanetRings.Visible = true;
                }
                else
                {
                    ParentPlanetRings.Visible = false;
                }

                ParentPlanet.Visible = true;
            }
            else
            {
                ParentPlanet.Visible = false;
            }
        }

        PlanetName.Text = _PlanetData.ValueS;

        Visible = true;
    }

    public void OnSelect()
    {
        if (Selected) return;
        Selected = true;

        Parent.Select(this);
    }

    public void Deselect()
    {
        Selected = false;
    }

    public override void _Process(double delta)
    { 
        if (Selected)
        {
            if (CustomMinimumSize.X < 192)
            {
                CustomMinimumSize = new Vector2(Mathf.Min(CustomMinimumSize.X + (float)delta * 500, 192), CustomMinimumSize.Y);
                Size = CustomMinimumSize;
            }
        }
        else
        {
            if (CustomMinimumSize.X > 96)
            {
                CustomMinimumSize = new Vector2(Mathf.Max(CustomMinimumSize.X - (float)delta * 500, 96), CustomMinimumSize.Y);
                Size = CustomMinimumSize;
            }
        }
    }
}