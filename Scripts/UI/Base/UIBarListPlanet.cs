using Godot;
using Godot.Collections;
using System.Collections.Generic;

//[Tool]
public partial class UIBarListPlanet : Control
{
    private const int SIZE_MIN = 32;
    private const int SIZE = 32;

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
    private Control BuildAvalilable = null;
    private Control BuildBtn = null;

    [ExportCategory("Runtime")]
    [Export]
    public bool IsSelected = false;

    [Export]
    public PlanetData _PlanetData = null;
    [Export]
    public PlanetData _ParentPlanetData = null;

    Game Game;

    private bool LockBuildButton = false;

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

            if (HasNode("AvailableBuild"))
            {
                BuildAvalilable = GetNode<Control>("AvailableBuild");
                BuildBtn = GetNode<Control>("ButtonBuild");
            }

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

        if (_PlanetData.Data.ValueS == "Star" || _PlanetData.Data.ValueS == "Outer_System" || _PlanetData.Data.ValueS == "Asteroid_Field")
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
                        Planet.CustomMinimumSize = new Vector2(SIZE_MIN + SIZE * sizeData.ValueI, SIZE_MIN + SIZE * sizeData.ValueI);
                        PlanetRings.CustomMinimumSize = new Vector2(1.5f * SIZE_MIN + 1.5f * SIZE * sizeData.ValueI, 0.5f * SIZE_MIN + 0.5f * SIZE * sizeData.ValueI);
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
                        Planet.CustomMinimumSize = new Vector2(SIZE_MIN + SIZE * sizeData.ValueI, SIZE_MIN + SIZE * sizeData.ValueI);
                        PlanetRings.CustomMinimumSize = new Vector2(1.5f * SIZE_MIN + 1.5f * SIZE * sizeData.ValueI, 0.5f * SIZE_MIN + 0.5f * SIZE * sizeData.ValueI);
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
                    ParentPlanet.CustomMinimumSize = new Vector2(SIZE_MIN + SIZE * sizeData.ValueI, SIZE_MIN + SIZE * sizeData.ValueI);
                    ParentPlanetRings.CustomMinimumSize = new Vector2(1.5f * SIZE_MIN + 1.5f * SIZE * sizeData.ValueI, 0.5f * SIZE_MIN + 0.5f * SIZE * sizeData.ValueI);
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

        bool hasPossibleBuildings = false;
        for (int idx = 0; idx < Game.TurnLoop.CurrentHumanPlayerData.Sectors.Count; idx++)
        {
            SectorData sector = Game.TurnLoop.CurrentHumanPlayerData.Sectors[idx];
            for (int buildIdx = 0; buildIdx < sector.AvailableBuildings_PerTurn.Count; buildIdx++)
            {
                ActionTargetInfo info = sector.AvailableBuildings_PerTurn[buildIdx];
                if (info._Planet == planetData)
                {
                    LockBuildButton = sector.ActionBuildQueue.GetSubs("Building").Count == 0;
                    hasPossibleBuildings = true;
                    break;
                }
            }
            if (hasPossibleBuildings) break;
        }

        if (BuildAvalilable != null)
        {
            if (hasPossibleBuildings)
            {
                BuildBtn.Visible = LockBuildButton || IsSelected;
                BuildAvalilable.Visible = true;
            }
            else
            {
                BuildAvalilable.Visible = false;
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
        BuildBtn.Visible = true;

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
        BuildBtn.Visible = LockBuildButton;

        IsSelected = false;
    }

    public void OnBuild()
    {
        Game.WindowsUI.Build(_PlanetData);
    }
}