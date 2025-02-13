using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIActionsEconomy : Control
{
    [Export]
    private Control Buttons;
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
    private StarData _SelectedStar = null;
    private SystemData _SelectedSystem = null;
    private StarData _HoveredStar = null;
    private SystemData _HoveredSystem = null;


    public void Refresh(StarData selectedStar, StarData hoveredStar)
    {
        _HoveredStar = hoveredStar;
        _HoveredSystem = _HoveredStar?.System;

        _SelectedStar = selectedStar;
        _SelectedSystem = _SelectedStar?.System;

        SelectedName.SetTextWithReplace("$name", hoveredStar != null ? hoveredStar.StarName : selectedStar.StarName);

        if (_HoveredStar != null && _HoveredStar != _SelectedStar)
        {
            Buttons.Visible = false;
            SelectText.Visible = true;
        }
        else if(_SelectedSystem != null)
        {
            if (_SelectedSystem._Player == Game.self.HumanPlayer)
            {
                Buttons.Visible = true;
                SelectText.Visible = false;

                Colonize.Visible = true;
                Colonize.Disabled = _SelectedSystem.ActionEconomyColonize_PerTurn.Count == 0;
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

                ColonizeText.Visible = false;
            }
            else
            {
                Buttons.Visible = true;
                SelectText.Visible = false;

                Colonize.Visible = false;
                Build.Visible = false;
                Change.Visible = false;
                Nationalize.Visible = false;
                Privatize.Visible = false;
                ChangeBuget.Visible = false;
                ChangeTax.Visible = false;
                ChangeWelfare.Visible = false;
                ChangeControl.Visible = false;

                ColonizeText.Visible = false;
            }
        }
        else
        {
            Buttons.Visible = true;
            SelectText.Visible = false;

            Colonize.Visible = true;
            Colonize.Disabled = false;

            Build.Visible = false;
            Change.Visible = false;
            Nationalize.Visible = false;
            Privatize.Visible = false;
            ChangeBuget.Visible = false;
            ChangeTax.Visible = false;
            ChangeWelfare.Visible = false;
            ChangeControl.Visible = false;

            ColonizeText.Visible = false;
        }
    }

    public void OnColonization()
    {
        Game.self.Input.OnAction_Economy_Colonize();
    }

    public void RefreshSelectPlanet()
    {
        Buttons.Visible = true;
        SelectText.Visible = false;

        Colonize.Visible = false;
        Build.Visible = false;
        Change.Visible = false;
        Nationalize.Visible = false;
        Privatize.Visible = false;
        ChangeBuget.Visible = false;
        ChangeTax.Visible = false;
        ChangeWelfare.Visible = false;
        ChangeControl.Visible = false;

        ColonizeText.Visible = true;
    }

    public void OnCancel()
    {
        Game.self.Input.DeselectOneStep();
    }
}