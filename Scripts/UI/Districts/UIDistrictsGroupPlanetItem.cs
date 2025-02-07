using Godot;
using System.Transactions;

public partial class UIDistrictsGroupPlanetItem : Control
{
    // is beeing duplicated
    private UIText PopsValue;
    private Button Button;
    private UITooltipTrigger ToolTip;
    private UIDistrictsGroupPlanetPops PopsList = null;

    // runtime
    public DistrictData _District = null;

    public override void _Ready()
    {
        PopsValue = GetNode<UIText>("Top/Top");
        Button = GetNode<Button>("Panel/Button");
        ToolTip = GetNode<UITooltipTrigger>("ToolTip");
        if (HasNode("Pops"))
        {
            PopsList = GetNode<UIDistrictsGroupPlanetPops>("Pops");
        }
    }

    public void Refresh(DistrictData district)
    {
        
    }
}