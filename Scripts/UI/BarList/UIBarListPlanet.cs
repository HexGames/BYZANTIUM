using Godot;
using Godot.Collections;
using System.Collections.Generic;

//[Tool]
public partial class UIBarListPlanet : Control
{
    // is beeing duplicated
    private UIGalaxyBarList GalaxyList = null;
    private UISystemBarList SystemList = null;
    private Label PlanetName = null;
    private TextureRect Simple = null;
    private TextureRect Planet = null;
    private TextureRect PlanetRings = null;
    private TextureRect ParentPlanet = null;
    private TextureRect ParentPlanetRings = null;
    private TextureRect Station = null;
    private Panel Selected = null;

    [ExportCategory("Runtime")]
    [Export]
    public bool IsSelected = false;

    [Export]
    public PlanetData _PlanetData = null;
    [Export]
    public PlanetData _ParentPlanetData = null;

    Game Game;

    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            Game = GetNode<Game>("/root/Main/Game");
            GalaxyList = GetNode<Control>("../../") as UIGalaxyBarList;
            SystemList = GetNode<Control>("../../../") as UISystemBarList;
            PlanetName = GetNode<Label>("Mask/NameBackground/Name");
            Simple = GetNode<TextureRect>("Mask/Simple");
            Planet = GetNode<TextureRect>("Mask/Planet");
            PlanetRings = GetNode<TextureRect>("Mask/Planet/Rings");
            ParentPlanet = GetNode<TextureRect>("Mask/Parent");
            ParentPlanetRings = GetNode<TextureRect>("Mask/Parent/Rings");
            Station = GetNode<TextureRect>("Mask/Station");
            Selected = GetNode<Panel>("Selected");

            Visible = false;
        }
    }

    public void Refresh(PlanetData planetData, PlanetData parentPlanetData)
    {
        _PlanetData = planetData;
        _ParentPlanetData = parentPlanetData;
        Name = _PlanetData.PlanetName + "_UI";

        //Planet.Texture = 

        DataBlock typeData = _PlanetData.Data.GetSub("Type");
        //float sizeIncremnt = 0.035f;
        //if (typeData?.ValueS == "GasGiant") sizeIncremnt = 0.05f;

        if (_PlanetData.Data.ValueS == "Star" || _PlanetData.Data.ValueS == "Outer_System" || _PlanetData.Data.ValueS == "Asteroid_Belt")
        {
            Simple.Texture = Game.Def.UIPlanets.GetPlanetTexture(_PlanetData.Data.ValueS);
            Simple.Visible = true;

            if (_PlanetData.Data.GetSub("Link:Player:Sector:System:Colony") != null)
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
                if (_PlanetData.Data.GetSub("Custom") != null)
                {
                    Planet.Texture = Game.Def.UIPlanets.GetPlanetTexture(_PlanetData.Data.ValueS);

                    DataBlock sizeData = _PlanetData.Data.GetSub("Size");
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

                    DataBlock sizeData = _PlanetData.Data.GetSub("Size");
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

                if (_PlanetData.Data.GetSub("Rings") != null)
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
                DataBlock parentTypeData = _ParentPlanetData.Data.GetSub("Type");
                if (_ParentPlanetData.Data.GetSub("Custom") != null)
                {
                    ParentPlanet.Texture = Game.Def.UIPlanets.GetPlanetTexture(_ParentPlanetData.Data.ValueS);
                }
                else
                {
                    ParentPlanet.Texture = Game.Def.UIPlanets.GetPlanetTexture(parentTypeData?.ValueS);
                }

                DataBlock sizeData = _ParentPlanetData.Data.GetSub("Size");
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

                if (_ParentPlanetData.Data.GetSub("Rings") != null)
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

        PlanetName.Text = _PlanetData.PlanetName;

        Visible = true;
    }

    public void OnHover()
    {
        if (SystemList != null)
        {
            SystemList.Hover(this);
        }
    }

    public void OnDehover()
    {
        if (SystemList != null)
        {
            SystemList.Unhover();
        }
    }

    public void OnSelect()
    {
        if (IsSelected) return;
        IsSelected = true;

        Selected.Visible = true;

        if (GalaxyList != null)
        {
            GalaxyList.Select(this);
        }
        else if (SystemList != null)
        {
            SystemList.Select(this);
        }
    }

    public void Deselect()
    {
        Selected.Visible = false;

        IsSelected = false;
    }
}