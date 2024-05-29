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
    private RichTextLabel PlanetName = null;
    private static string PlanetName_Original = "";
    private TextureRect Simple = null;
    private TextureRect Planet = null;
    private TextureRect PlanetRings = null;
    private TextureRect ParentPlanet = null;
    private TextureRect ParentPlanetRings = null;
    private TextureRect Station = null;
    private TextureRect Flag = null;
    private Control Build = null;
    private Control Pops = null;
    private Control BuildUnavailable = null;
    private Control PopsUnavailable = null;
    private Panel Selected = null;
    private Control Colony = null;
    private Control ColonyOwnerRow = null;
    private RichTextLabel ColonyOwnerText = null;
    private static string ColonyOwnerText_Original = "";
    private Control ColonyAuthorityRow = null;
    private RichTextLabel ColonyAuthorityText = null;
    private static string ColonyAuthorityText_Original = "";
    private Control ColonyPopsRow = null;
    private Control ColonyPopsBg = null;
    private RichTextLabel ColonyPopsText = null;
    private static string ColonyPopsText_Original = "";
    private Control ColonyPopsControlledBg = null;
    private RichTextLabel ColonyPopsControlledText = null;
    private static string ColonyPopsControlledText_Original = "";
    private Control ColonyResRow = null;
    private Control ColonyRes_1_Bg = null;
    private RichTextLabel ColonyRes_1_Text = null;
    private static string ColonyRes_1_Text_Original = "";
    private Control ColonyRes_2_Bg = null;
    private RichTextLabel ColonyRes_2_Text = null;
    private static string ColonyRes_2_Text_Original = "";


    [ExportCategory("Runtime")]
    [Export]
    public bool IsSelected = false;
    [Export]
    public bool IsSelected_Buildings = false;
    [Export]
    public bool IsSelected_Population = false;

    [Export]
    public PlanetData _PlanetData = null;
    [Export]
    public PlanetData _ParentPlanetData = null;

    Game Game;

    private bool LockBuildButton = false;
    private bool HasPossibleBuildings = false;

    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            Game = GetNode<Game>("/root/Main/Game");
            GalaxyList = GetNode<Control>("../../") as UIGalaxyBarList;
            SystemList = GetNode<Control>("../../../") as UISystemBarList;
            PlanetName = GetNode<RichTextLabel>("Mask/NameBackground/Name");
            if (PlanetName_Original.Length == 0) PlanetName_Original = PlanetName.Text;
            Simple = GetNode<TextureRect>("Mask/Simple");
            Planet = GetNode<TextureRect>("Mask/Planet");
            PlanetRings = GetNode<TextureRect>("Mask/Planet/Rings");
            ParentPlanet = GetNode<TextureRect>("Mask/Parent");
            ParentPlanetRings = GetNode<TextureRect>("Mask/Parent/Rings");
            Station = GetNode<TextureRect>("Mask/Station");
            Flag = GetNode<TextureRect>("Mask/Flag");
            Selected = GetNode<Panel>("Selected");

            if (HasNode("Build"))
            {
                Build = GetNode<Control>("Build");
                BuildUnavailable = GetNode<Control>("Mask/UnavailableBuild");
            }
            if (HasNode("Pops"))
            {
                Pops = GetNode<Control>("Pops");
                PopsUnavailable = GetNode<Control>("Mask/UnavailablePops");
            }

            if (HasNode("Colony"))
            {
                Colony = GetNode<Control>("Colony");

                ColonyOwnerRow = GetNode<Control>("Colony/VBoxContainer/OwnerRow");
                ColonyOwnerText = GetNode<RichTextLabel>("Colony/VBoxContainer/OwnerRow/Owner/MarginContainer/RichTextLabel");
                if (ColonyOwnerText_Original.Length == 0) ColonyOwnerText_Original = ColonyOwnerText.Text;

                ColonyAuthorityRow = GetNode<Control>("Colony/VBoxContainer/Authority");
                ColonyAuthorityText = GetNode<RichTextLabel>("Colony/VBoxContainer/Authority/Authority/MarginContainer/RichTextLabel");
                if (ColonyAuthorityText_Original.Length == 0) ColonyAuthorityText_Original = ColonyAuthorityText.Text;

                ColonyPopsRow = GetNode<Control>("Colony/VBoxContainer/PopsRow");
                ColonyPopsBg = GetNode<Control>("Colony/VBoxContainer/PopsRow/Pops");
                ColonyPopsText = GetNode<RichTextLabel>("Colony/VBoxContainer/PopsRow/Pops/MarginContainer/RichTextLabel");
                if (ColonyPopsText_Original.Length == 0) ColonyPopsText_Original = ColonyPopsText.Text;
                ColonyPopsControlledBg = GetNode<Control>("Colony/VBoxContainer/PopsRow/PopsControled");
                ColonyPopsControlledText = GetNode<RichTextLabel>("Colony/VBoxContainer/PopsRow/PopsControled/MarginContainer/RichTextLabel");
                if (ColonyPopsControlledText_Original.Length == 0) ColonyPopsControlledText_Original = ColonyPopsControlledText.Text;

                ColonyResRow = GetNode<Control>("Colony/VBoxContainer/IncomeRow");
                ColonyRes_1_Bg = GetNode<Control>("Colony/VBoxContainer/IncomeRow/Res_1");
                ColonyRes_1_Text = GetNode<RichTextLabel>("Colony/VBoxContainer/IncomeRow/Res_1/MarginContainer/RichTextLabel");
                if (ColonyRes_1_Text_Original.Length == 0) ColonyRes_1_Text_Original = ColonyRes_1_Text.Text;
                ColonyRes_2_Bg = GetNode<Control>("Colony/VBoxContainer/IncomeRow/Res_2");
                ColonyRes_2_Text = GetNode<RichTextLabel>("Colony/VBoxContainer/IncomeRow/Res_2/MarginContainer/RichTextLabel");
                if (ColonyRes_2_Text_Original.Length == 0) ColonyRes_2_Text_Original = ColonyRes_2_Text.Text;
            }

            Visible = false;
        }
    }

    public void Refresh(PlanetData planetData, PlanetData parentPlanetData)
    {
        _PlanetData = planetData;
        _ParentPlanetData = parentPlanetData;
        IsSelected = false;
        IsSelected_Buildings = false;
        IsSelected_Population = false;
        Name = _PlanetData.PlanetName + "_UI";

        //Planet.Texture = 

        DataBlock typeData = _PlanetData.Data.GetSub("Type", false);
        //float sizeIncremnt = 0.035f;
        //if (typeData?.ValueS == "GasGiant") sizeIncremnt = 0.05f;

        //if (_PlanetData.Colony != null)
        //{
        //    Flag.Texture = Game.Assets.GetTexture2D(_PlanetData.Colony._System._Sector._Player.Empire.GetSub("Flag").ValueS);
        //    Flag.Visible = true;
        //}
        //else
        {
            Flag.Visible = false;
        }

        if (_PlanetData.Data.ValueS == "Star" || _PlanetData.Data.ValueS == "Outer_System" || _PlanetData.Data.ValueS == "Asteroids")
        {
            Simple.Texture = Game.Def.UIPlanets.GetPlanetTexture(_PlanetData.Data.ValueS);
            Simple.Visible = true;

            if (_PlanetData._Star.System != null)
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
                if (_PlanetData.Data.GetSub("Custom", false) != null)
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

                if (_PlanetData.Data.GetSub("Rings", false) != null)
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
                if (_ParentPlanetData.Data.GetSub("Custom", false) != null)
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

                if (_ParentPlanetData.Data.GetSub("Rings", false) != null)
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

        LockBuildButton = false;
        HasPossibleBuildings = false;
        for (int idx = 0; idx < Game.TurnLoop.CurrentHumanPlayerData.Sectors.Count; idx++)
        {
            SectorData sector = Game.TurnLoop.CurrentHumanPlayerData.Sectors[idx];
            for (int buildIdx = 0; buildIdx < sector.AvailableBuildings_PerTurn.Count; buildIdx++)
            {
                DefBuildingWrapper info = sector.AvailableBuildings_PerTurn[buildIdx];
                if (info._Planet == planetData)
                {
                    LockBuildButton = sector.ActionBuildQueue.GetSubs("Building").Count == 0;
                    HasPossibleBuildings = true;
                    break;
                }
            }
            if (HasPossibleBuildings) break;
        }

        if (Build != null)
        {
            Build.Visible = LockBuildButton && HasPossibleBuildings == true; 
            BuildUnavailable.Visible = LockBuildButton == false && HasPossibleBuildings == true;

            //if (HasPossibleBuildings)
            //{
            //    Build.Visible = true;
            //    BuildUnavailable.Visible = false;
            //}
            //else
            //{
            //    Build.Visible = false;
            //    BuildUnavailable.Visible = true;
            //}
        }

        if (Pops != null)
        {
            //if (_PlanetData.Colony != null && _PlanetData.Colony.IsWorld())
            //{
            //    Pops.Visible = true;
            //    PopsUnavailable.Visible = false;
            //}
            //else
            //{
            //    Pops.Visible = false;
            //    PopsUnavailable.Visible = true;
            //}
            Pops.Visible = false;
            PopsUnavailable.Visible = false;
        }

        PlanetName.Text = PlanetName_Original.Replace("$name", _PlanetData.PlanetName);

        // --- Colony 
        if (Colony != null)
        {
            if(_PlanetData.Colony != null)
            {
                if (_PlanetData.Colony.Type.ValueS == "Star")
                {
                    ColonyOwnerText.Text = ColonyOwnerText_Original.Replace("$player", _PlanetData.Colony._System._Sector._Player.PlayerName);
                    ColonyOwnerRow.Visible = true;
                }
                else
                {
                    ColonyOwnerRow.Visible = false;
                }

                int authority = _PlanetData.Colony.Resources_PerTurn.GetLimit("Authority").GetUsedTotal();
                if (authority > 0)
                {
                    ColonyAuthorityText.Text = ColonyAuthorityText_Original.Replace("$val", _PlanetData.Colony.Resources_PerTurn.GetLimit("Authority").ToString_Used(true));
                    ColonyAuthorityRow.Visible = true;
                }
                else
                {
                    ColonyAuthorityRow.Visible = false;
                }

                if (_PlanetData.Colony.Type.ValueS == "World")
                {
                    ColonyPopsText.Text = ColonyPopsText_Original.Replace("$val", _PlanetData.Colony.Resources_PerTurn.GetPops().ToString_Pops());
                    ColonyPopsBg.Visible = true;

                    int controlledPops = _PlanetData.Colony.Resources_PerTurn.GetPops().GetCPops();
                    if (controlledPops > 0)
                    {
                        ColonyPopsControlledText.Text = ColonyPopsControlledText_Original.Replace("$val", _PlanetData.Colony.Resources_PerTurn.GetPops().ToString_CPops());
                        ColonyPopsControlledBg.Visible = true;
                    }
                    else
                    {
                        ColonyPopsControlledBg.Visible = false;
                    }

                    ColonyPopsRow.Visible = true;
                }
                else
                {
                    ColonyPopsRow.Visible = false;
                }

                //private Control ColonyResRow = null;
                //private Control ColonyRes_1_Bg = null;
                //private RichTextLabel ColonyRes_1_Text = null;
                //private static string ColonyRes_1_Text_Original = "";
                //private Control ColonyRes_2_Bg = null;
                //private RichTextLabel ColonyRes_2_Text = null;
                //private static string ColonyRes_2_Text_Original = "";

                if (_PlanetData.Colony.Type.ValueS == "Outpost" || _PlanetData.Colony.Type.ValueS == "Colony")
                {
                    ColonyRes_1_Bg.Visible = false;
                    ColonyRes_2_Bg.Visible = false;
                    for (int idx = 0; idx < _PlanetData.Colony.Resources_PerTurn.Incomes.Count; idx++)
                    {
                        if (_PlanetData.Colony.Resources_PerTurn.Incomes[idx].GetIncomeTotal() > 0)
                        {
                            if (ColonyRes_1_Bg.Visible == false)
                            {
                                ColonyRes_1_Text.Text = ColonyRes_1_Text_Original.Replace("$val", _PlanetData.Colony.Resources_PerTurn.Incomes[idx].ToString_Income(true)).Replace("Turn", _PlanetData.Colony.Resources_PerTurn.Incomes[idx].Name);
                                ColonyRes_1_Bg.Visible = true;
                            }
                            else //if (ColonyRes_2_Bg.Visible == false)
                            {
                                ColonyRes_2_Text.Text = ColonyRes_2_Text_Original.Replace("$val", _PlanetData.Colony.Resources_PerTurn.Incomes[idx].ToString_Income(true)).Replace("Turn", _PlanetData.Colony.Resources_PerTurn.Incomes[idx].Name);
                                ColonyRes_2_Bg.Visible = true;
                                break;
                            }
                        }
                    }
                    ColonyResRow.Visible = ColonyRes_1_Bg.Visible;
                }
                else
                {
                    ColonyResRow.Visible = false;
                }

                Colony.Visible = true;
            }
            else
            {
                Colony.Visible = false;
            }
        }
        else
        {
            Colony.Visible = false;
        }

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

        Game.Input.SelectPlanet(_PlanetData, false, false);

        //if (GalaxyList != null)
        //{
        //    //GalaxyList.Select(this);
        //}
        //else if (SystemList != null)
        //{
        //    SystemList.Select(this);
        //}
    }

    public void OnSelectBuildings()
    {
        if (IsSelected_Buildings) return;
        IsSelected_Buildings = true;
        IsSelected_Population = false;

        Selected.Visible = true;

        Game.Input.SelectPlanet(_PlanetData, true, false);

        //if (GalaxyList != null)
        //{
        //    //GalaxyList.Select(this);
        //}
        //else if (SystemList != null)
        //{
        //    SystemList.Select(this);
        //}
    }

    public void OnSelectPopulation()
    {
        if (IsSelected_Population) return;
        IsSelected_Buildings = false;
        IsSelected_Population = true;

        Selected.Visible = true;

        Game.Input.SelectPlanet(_PlanetData, false, true);

        //if (GalaxyList != null)
        //{
        //    //GalaxyList.Select(this);
        //}
        //else if (SystemList != null)
        //{
        //    SystemList.Select(this);
        //}
    }

    public void Deselect()
    {
        Selected.Visible = false;

        IsSelected = false;
        IsSelected_Buildings = false;
        IsSelected_Population = false;
    }
}