using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIPlanetInfoPlanet : Control
{
    // is beeing duplicated
    private Control BGIdle;
    private Control BGSelected;

    private Button IconBtn;
    private Control IconSelected;
    private TextureRect IconTexture;
    private UITooltipTrigger IconTooltip;

    private UIText NameText;
    private UIText DescriptionText;

    private Control ActionsBg;
    private UIButton Terraform;

    // Runtime
    public PlanetData _Planet = null;

    public override void _Ready()
    {
        BGIdle = GetNode<Control>("PanelBg");
        BGSelected = GetNode<Control>("SelectedBG");

        IconBtn = GetNode<Button>("MarginContainer/VBoxContainer/HBoxContainer/Icon/Button");
        IconSelected = GetNode<Control>("MarginContainer/VBoxContainer/HBoxContainer/Icon/Selected");
        IconTexture = GetNode<TextureRect>("MarginContainer/VBoxContainer/HBoxContainer/Icon/Icon");
        IconTooltip = GetNode<UITooltipTrigger>("MarginContainer/VBoxContainer/HBoxContainer/Icon/Button/ToolTip");

        NameText = GetNode<UIText>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Name");
        DescriptionText = GetNode<UIText>("MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/Description");

        ActionsBg = GetNode<Control>("MarginContainer/VBoxContainer/Actions");
        Terraform = GetNode<UIButton>("MarginContainer/VBoxContainer/Actions/Terraform");
    }


    public void Refresh(PlanetData planet)
    {
        _Planet = planet;

        IconTexture.Texture = Game.self.Def.AssetLib.GetTexture2D_Planet(_Planet.Data.GetSubValueS("Type") + ".png");

        NameText.SetTextWithReplace("$name", _Planet.PlanetName);

        int maxPops = _Planet.Data.GetSubValueI("PopsMax");
        if (_Planet.Colony != null)
        {
            int currentPops = 0;
            for (int idx = 0; idx < _Planet.Colony.Districts.Count; idx++)
            {
                if (_Planet.Colony.Districts[idx].HasFullPop())
                {
                    currentPops++;
                }
            }
            DescriptionText.SetTextWithReplace("$description", currentPops.ToString() + Helper.GetIcon("Pops") + "/" + maxPops.ToString() + Helper.GetIcon("Pops"));
        }
        else if (maxPops > 0)
        {
            DescriptionText.SetTextWithReplace("$description", maxPops.ToString() + Helper.GetIcon("Pops") + "Max");
        }
        else
        {
            DescriptionText.SetTextWithReplace("$description", "Uninhabitble");
        }

        CloseActions();
    }

    public void OpenActions()
    {
        BGIdle.Visible = false;
        BGSelected.Visible = true;

        IconBtn.Visible = false;
        IconSelected.Visible = true;

        ActionsBg.Visible = true;

        Terraform.Visible = true;
    }

    public void CloseActions()
    {
        BGIdle.Visible = true;
        BGSelected.Visible = false;

        IconBtn.Visible = true;
        IconSelected.Visible = false;

        ActionsBg.Visible = false;
    }

    public void OnSelect()
    {
        Game.self.GalaxyUI.PlanetInfo.OnSelectPlanet();
    }
}