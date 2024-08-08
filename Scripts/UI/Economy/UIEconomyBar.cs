using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIEconomyBar : Control
{
    [ExportCategory("Links")]
    [Export]
    public Control Empire_Container;
    [Export]
    public UIEconomyInfoItem Empire_BC;
    [Export]
    public UIEconomyInfoItem Empire_Research;
    [Export]
    public UIEconomyInfoItem Empire_Culture;
    [Export]
    public UIEconomyInfoItem Empire_Influence;
    [Export]
    public RichTextLabel Empire_Authority;
    private string Empire_Authority_Original = "";
    [Export]
    public Control SectorBg;
    [Export]
    public Control SectorToEmpire_1;
    [Export]
    public Control SectorToEmpire_2;
    [Export]
    public Control Sector_Container_1;
    [Export]
    public Control Sector_Container_2;
    [Export]
    public UIEconomyInfoItem Sector_Energy;
    [Export]
    public UIEconomyInfoItem Sector_Minerals;
    [Export]
    public UIEconomyInfoItem Sector_Production;
    [Export]
    public UIEconomyInfoItem Sector_Shipyard;
    [Export]
    public UIEconomyInfoItem Sector_BC;
    [Export]
    public UIEconomyInfoItem Sector_Research;
    [Export]
    public UIEconomyInfoItem Sector_Culture;
    [Export]
    public UIEconomyInfoItem Sector_Influence;
    [Export]
    public RichTextLabel Sector_Authority;
    private string Sector_Authority_Original = "";
    [Export]
    public Control SelectedToSector_1;
    [Export]
    public Control SelectedToSector_2;
    [Export]
    public Control SelectedSystem_1;
    [Export]
    public Control SelectedSystem_2;
    [Export]
    public Control SelectedPlanet_1;
    [Export]
    public Control SelectedPlanet_2;
    [Export]
    public UIEconomyInfoItem Selected_Energy;
    [Export]
    public UIEconomyInfoItem Selected_Minerals;
    [Export]
    public UIEconomyInfoItem Selected_Production;
    [Export]
    public UIEconomyInfoItem Selected_Shipyard;
    [Export]
    public UIEconomyInfoItem Selected_BC;
    [Export]
    public UIEconomyInfoItem Selected_Research;
    [Export]
    public UIEconomyInfoItem Selected_Culture;
    [Export]
    public UIEconomyInfoItem Selected_Influence;
    [Export]
    public RichTextLabel Selected_Authority;
    private string Selected_Authority_Original = "";
    [Export]
    public Control NoColony_1;
    [Export]
    public Control NoColony_2;

    [ExportCategory("Runtime")]
    [Export]
    public PlayerData _Player = null;
    [Export]
    public SectorData _Sector = null;
    [Export]
    public SystemData _System = null;
    [Export]
    public ColonyData _Colony = null;
    [Export]
    public PlanetData _Planet = null;


    public override void _Ready()
    {
        Empire_Authority_Original = Empire_Authority.Text;
        Sector_Authority_Original = Sector_Authority.Text;
        Selected_Authority_Original = Selected_Authority.Text;
    }

    public void Refresh(PlayerData player)
    {
        _Player = player;
        _Sector = null;
        _System = null;
        _Colony = null;
        _Planet = null;

        Refresh();
    }

    public void Refresh(SectorData sector)
    {
        _Player = sector._Player;
        _Sector = sector;
        _System = null;
        _Colony = null;
        _Planet = null;

        Refresh();
    }

    public void Refresh(PlayerData player, StarData star)
    {
        if (star.System != null)
        {
            _Player = star.System._Sector._Player;
            _Sector = star.System._Sector;
            _System = star.System;
            _Colony = null;
            _Planet = null;
        }
        else
        {
            _Player = player;
            _Sector = null;
            _System = null;
            _Colony = null;
            _Planet = null;
        }

        Refresh();
    }

    public void Refresh(PlayerData player, PlanetData planet)
    {
        if (planet.Colony != null)
        {
            _Player = planet.Colony._System._Sector._Player;
            _Sector = planet.Colony._System._Sector;
            _System = planet.Colony._System;
            _Colony = planet.Colony;
            _Planet = planet;
        }
        else if (planet._Star.System != null)
        {
            _Player = planet._Star.System._Sector._Player;
            _Sector = planet._Star.System._Sector;
            _System = planet._Star.System;
            _Colony = null;
            _Planet = planet;
        }
        else
        {
            _Player = player;
            _Sector = null;
            _System = null;
            _Colony = null;
            _Planet = null;
        }

        Refresh();
    }

    public void Refresh()
    {
        if (Game.self == null)
            return;

        if (_Player == null)
        {
            _Player = Game.self.HumanPlayer;
        }

        Empire_BC.Refresh(_Player.Resources_PerTurn.GetStockpileString("BC"), _Player.Resources_PerTurn.GetStockpileTooltip("BC"));
        Empire_BC.Visible = true;

        Empire_Research.Refresh(_Player.Resources_PerTurn.GetIncomeString("Research"));
        Empire_Research.Visible = true;

        Empire_Culture.Refresh(_Player.Resources_PerTurn.GetIncomeString("Culture"));
        Empire_Culture.Visible = true;

        Empire_Influence.Refresh(_Player.Resources_PerTurn.GetLimitString("Authority"));
        //Empire_Authority.Text = Empire_Authority_Original.Replace("$value", _Player.Resources_PerTurn.GetLimitString("Influence"));
        Empire_Influence.Visible = true;

        if (_Sector != null)
        {
            bool bad = _Sector.Resources_PerTurn.GetIncome("Energy").GetIncomeTotal() < _Sector.Resources_PerTurn.GetIncome("Production").GetIncomeTotal();
            bool waste = _Sector.Resources_PerTurn.GetIncome("Energy").GetIncomeTotal() > 2 * _Sector.Resources_PerTurn.GetIncome("Production").GetIncomeTotal();
            Sector_Energy.Refresh((bad ? Helper.GetColorPrefix_Bad() : (waste ? Helper.GetColorPrefix_Waste() : Helper.GetColorPrefix_Neutral())) + _Sector.Resources_PerTurn.GetIncomeString("Energy") + Helper.GetColorSufix());
            Sector_Energy.Visible = true;

            bad = _Sector.Resources_PerTurn.GetIncome("Minerals").GetIncomeTotal() < _Sector.Resources_PerTurn.GetIncome("Shipbuilding").GetIncomeTotal();
            waste = _Sector.Resources_PerTurn.GetIncome("Minerals").GetIncomeTotal() > 2 * _Sector.Resources_PerTurn.GetIncome("Shipbuilding").GetIncomeTotal();
            Sector_Minerals.Refresh((bad ? Helper.GetColorPrefix_Bad() : (waste ? Helper.GetColorPrefix_Waste() : Helper.GetColorPrefix_Neutral())) + _Sector.Resources_PerTurn.GetIncomeString("Minerals") + Helper.GetColorSufix());
            Sector_Minerals.Visible = true;

            Sector_Production.Refresh(_Sector.Resources_PerTurn.GetIncomeString("Production"));
            Sector_Production.Visible = true;

            Sector_Shipyard.Refresh(_Sector.Resources_PerTurn.GetIncomeString("Shipbuilding"));
            Sector_Shipyard.Visible = true;

            bool idle = _Sector.BuildQueue_PerTurn_ActionChange.Buildings.Count == 0;
            Sector_BC.Refresh(_Sector.Resources_PerTurn.GetIncomeString("BC", _Sector.Resources_PerTurn.GetIncome("BC").GetIncomeTotalSpecial(idle)));
            Sector_BC.Visible = true;

            Sector_Research.Refresh(_Sector.Resources_PerTurn.GetIncomeString("Research"));
            Sector_Research.Visible = true;

            Sector_Culture.Refresh(_Sector.Resources_PerTurn.GetIncomeString("Culture"));
            Sector_Culture.Visible = true;

            Sector_Influence.Refresh(_Sector.Resources_PerTurn.GetLimitString("Authority"));
            //Sector_Authority.Text = Sector_Authority_Original.Replace("$value", _Sector.Resources_PerTurn.GetLimitString("Influence"));
            Sector_Influence.Visible = true;

            SectorToEmpire_1.Visible = true;
            SectorToEmpire_2.Visible = true;

            Empire_Container.SelfModulate = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            SectorBg.Visible = true;
        }
        else
        {
            SectorToEmpire_1.Visible = false;
            SectorToEmpire_2.Visible = false;

            Empire_Container.SelfModulate = new Color(0.0f, 0.0f, 0.0f, 0.0f);

            SectorBg.Visible = false;
        }

        /*if (_System != null)
        {
            SelectedSystem_1.Visible = true;
            SelectedSystem_2.Visible = true;

            SelectedPlanet_1.Visible = false;
            SelectedPlanet_2.Visible = false;

            Selected_Energy.Refresh(_System.Resources_PerTurn.GetIncomeString("Energy"));
            Selected_Energy.Visible = true;

            Selected_Minerals.Refresh(_System.Resources_PerTurn.GetIncomeString("Minerals"));
            Selected_Minerals.Visible = true;

            Selected_Production.Refresh(_System.Resources_PerTurn.GetIncomeString("Production"));
            Selected_Production.Visible = true;

            Selected_Shipyard.Refresh(_System.Resources_PerTurn.GetIncomeString("Shipbuilding"));
            Selected_Shipyard.Visible = true;

            Selected_BC.Refresh(_System.Resources_PerTurn.GetIncomeString("BC"));
            Selected_BC.Visible = true;

            Selected_Research.Refresh(_System.Resources_PerTurn.GetIncomeString("Research"));
            Selected_Research.Visible = true;

            Selected_Culture.Refresh(_System.Resources_PerTurn.GetIncomeString("Culture"));
            Selected_Culture.Visible = true;

            Selected_Influence.Refresh(_System.Resources_PerTurn.GetIncomeString("Influence"));
            Selected_Influence.Visible = true;

            SelectedToSector_1.Visible = true;
            SelectedToSector_2.Visible = true;
        }
        else*/
        if (_Colony != null)
        {
            SelectedSystem_1.Visible = false;
            SelectedSystem_2.Visible = false;

            SelectedPlanet_1.Visible = true;
            SelectedPlanet_2.Visible = true;

            Selected_Energy.Refresh(_Colony.Resources_PerTurn.GetIncomeString("Energy"));
            Selected_Energy.Visible = true;

            Selected_Minerals.Refresh(_Colony.Resources_PerTurn.GetIncomeString("Minerals"));
            Selected_Minerals.Visible = true;

            Selected_Production.Refresh(_Colony.Resources_PerTurn.GetIncomeString("Production"));
            Selected_Production.Visible = true;

            Selected_Shipyard.Refresh(_Colony.Resources_PerTurn.GetIncomeString("Shipbuilding"));
            Selected_Shipyard.Visible = true;

            Selected_BC.Refresh(_Colony.Resources_PerTurn.GetIncomeString("BC"));
            Selected_BC.Visible = true;

            Selected_Research.Refresh(_Colony.Resources_PerTurn.GetIncomeString("Research"));
            Selected_Research.Visible = true;

            Selected_Culture.Refresh(_Colony.Resources_PerTurn.GetIncomeString("Culture"));
            Selected_Culture.Visible = true;

            Selected_Influence.Refresh(_Colony.Resources_PerTurn.GetLimitString("Authority"));
            //Selected_Authority.Text = Selected_Authority_Original.Replace("$value", _Colony.Resources_PerTurn.GetLimitString("Influence"));
            Selected_Influence.Visible = true;

            SelectedToSector_1.Visible = true;
            SelectedToSector_2.Visible = true;

            Sector_Container_1.SelfModulate = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            Sector_Container_2.SelfModulate = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            NoColony_1.Visible = false;
            NoColony_2.Visible = false;
        }
        else if (_Planet != null)
        {
            SelectedToSector_1.Visible = false;
            SelectedToSector_2.Visible = false;

            NoColony_1.Visible = true;
            NoColony_2.Visible = true;

            Sector_Container_1.SelfModulate = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            Sector_Container_2.SelfModulate = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }
        else
        {
            SelectedToSector_1.Visible = false;
            SelectedToSector_2.Visible = false;

            NoColony_1.Visible = false;
            NoColony_2.Visible = false;

            Sector_Container_1.SelfModulate = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            Sector_Container_2.SelfModulate = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }
    }
}