using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIActionsEconomy : Control
{
    [Export]
    private Control Buttons;
    [Export]
    private Control PlanetButtons;
    [Export]
    private TextureButton Colonize;
    [Export]
    private UITooltipTrigger ColonizeToolTip;
    [Export]
    private TextureButton Build;
    [Export]
    private UITooltipTrigger BuildToolTip;
    [Export]
    private TextureButton Change;
    [Export]
    private UITooltipTrigger ChangeToolTip;
    [Export]
    private TextureButton Nationalize;
    [Export]
    private UITooltipTrigger NationalizeToolTip;
    [Export]
    private TextureButton Privatize;
    [Export]
    private UITooltipTrigger PrivatizeToolTip;
    [Export]
    private Control SystemButtons;
    [Export]
    private TextureButton ChangeBuget;
    [Export]
    private UITooltipTrigger ChangeBugetToolTip;
    [Export]
    private TextureButton ChangeTax;
    [Export]
    private UITooltipTrigger ChangeTaxToolTip;
    [Export]
    private TextureButton ChangeWelfare;
    [Export]
    private UITooltipTrigger ChangeWelfareToolTip;
    [Export]
    private TextureButton ChangeControl;
    [Export]
    private UITooltipTrigger ChangeControlToolTip;

    [Export]
    private Control ColonizeText;

    [Export]
    private TextureButton Deselect;

    [Export]
    private Control SelectText;

    [Export]
    private UIText SelectedName;

    //
    public SystemData _System = null;
    public PlanetData _Planet = null;
    public StarData _TextStar = null;

    private bool Refreshed = false;
    public void NeedsRefresh()
    {
        Refreshed = false;
        if (Visible && SystemButtons.Visible) RefreshSystem(_System);
        else if (Visible && PlanetButtons.Visible) RefreshPlanet(_Planet);
        else if (Visible && SelectText.Visible) RefreshText(_TextStar);
    }

    public void RefreshText(StarData star)
    {
        if (star == null) return;
        Visible = true;
        Buttons.Visible = false;
        SelectText.Visible = true;
        if (_TextStar == star && Refreshed) return;

        _System = null;
        _Planet = null;
        _TextStar = star;
        Refreshed = true;

        SelectedName.SetTextWithReplace("$name", _TextStar.StarName);
    }

    public void RefreshSystem(SystemData system)
    {
        if (system == null) return;
        Visible = true;
        Buttons.Visible = true;
        SystemButtons.Visible = true;
        PlanetButtons.Visible = true;
        ColonizeText.Visible = false;
        SelectText.Visible = false;
        if (_System == system && Refreshed) return;

        _System = system;
        _Planet = null;
        _TextStar = null;
        Refreshed = true;

        SelectedName.SetTextWithReplace("$name", _System.Star.StarName);

        if (_System._Player == Game.self.HumanPlayer)
        {
            Colonize.Visible = true;
            Colonize.Disabled = _System.ActionEconomyColonize_PerTurn.Count == 0;
            Build.Visible = true;
            Build.Disabled = false;
            Change.Visible = true;
            Change.Disabled = false;
            Nationalize.Visible = true;
            Nationalize.Disabled = false;
            Privatize.Visible = true;
            Privatize.Disabled = false;
            ChangeBuget.Visible = true;
            ChangeBuget.Disabled = false;
            ChangeTax.Visible = true;
            ChangeTax.Disabled = false;
            ChangeWelfare.Visible = true;
            ChangeWelfare.Disabled = false;
            ChangeControl.Visible = true;
            ChangeControl.Disabled = false;
        }
        else
        {
            Colonize.Visible = false;
            Build.Visible = false;
            Change.Visible = false;
            Nationalize.Visible = false;
            Privatize.Visible = false;
            ChangeBuget.Visible = false;
            ChangeTax.Visible = false;
            ChangeWelfare.Visible = false;
            ChangeControl.Visible = false;
        }
    }

    public void RefreshPlanet(PlanetData planet)
    {
        if (planet == null) return;
        if (planet._Star.System == null) return;
        Visible = true;
        Buttons.Visible = true;
        SystemButtons.Visible = false;
        PlanetButtons.Visible = true;
        ColonizeText.Visible = false;
        SelectText.Visible = false;
        if (_Planet == planet && Refreshed) return;

        _System = null;
        _Planet = planet;
        _TextStar = null;
        Refreshed = true; 

        SelectedName.SetTextWithReplace("$name", _Planet.PlanetName);

        if (_Planet._Star.System._Player == Game.self.HumanPlayer)
        {
            Colonize.Visible = true;
            Colonize.Disabled = ActionPlanet.HasActionForPlanet(_Planet._Star.System.ActionEconomyColonize_PerTurn, _Planet);
            Build.Visible = true;
            Build.Disabled = false;
            Change.Visible = true;
            Change.Disabled = false;
            Nationalize.Visible = true;
            Nationalize.Disabled = false;
            Privatize.Visible = true;
            Privatize.Disabled = false;
        }
        else
        {
            Colonize.Visible = false;
            Build.Visible = false;
            Change.Visible = false;
            Nationalize.Visible = false;
            Privatize.Visible = false;
        }
    }

    public void OnColonization()
    {
        Game.self.UI.Action(ActionBase.ID.ECONOMY_COLONIZE);
    }

    public void Refresh_Colonize()
    {
        Visible = true;
        Buttons.Visible = true;
        SystemButtons.Visible = false;
        PlanetButtons.Visible = false;
        SelectText.Visible = false;

        ColonizeText.Visible = true;
    }

    public void OnCancel()
    {
        Game.self.UI.Deselect();
    }
}