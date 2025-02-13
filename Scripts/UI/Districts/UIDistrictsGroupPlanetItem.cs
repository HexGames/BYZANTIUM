using Godot;
using System.Transactions;

public partial class UIDistrictsGroupPlanetItem : Control
{
    // is beeing duplicated
    private UIText PopsValue;
    private Button Button;
    private Control ResIcon;
    private UITooltipTrigger ToolTip;
    private UIDistrictsGroupPlanetPops PopsList = null;

    // runtime
    public DistrictData _District = null;

    public override void _Ready()
    {
        PopsValue = GetNode<UIText>("Top/Top");
        Button = GetNode<Button>("Button");
        ToolTip = GetNode<UITooltipTrigger>("Button/ToolTip");
        ResIcon = GetNode<Control>("Bottom/Bottom");
        if (HasNode("Pops"))
        {
            PopsList = GetNode<UIDistrictsGroupPlanetPops>("Pops");
        }
    }

    public void Refresh(DistrictData district)
    {
        PopsValue.SetTextWithReplace("$", district.Pops.Count.ToString());
        Button.Disabled = true;

        //Icon.SelfModulate = new Color(1.0f, 1.0f, 1.0f, 0.25f + (district.Pops.Count != 0 ? 0.75f : 0.0f));
        ResIcon.SelfModulate = (district.Pops.Count != 0 ? new Color("ffffffff") : new Color("808080c0"));
        PopsValue.SelfModulate = (district.Pops.Count != 0 ? new Color("ffffffff") : new Color("808080c0"));

        if (PopsList != null)
        {
            PopsList.Refresh(district);
        }
    }

    //public void ShowButton()
    //{
    //    Button.Visible = true;
    //}
    //public void HideButton()
    //{
    //    Button.Visible = false;
    //}
}