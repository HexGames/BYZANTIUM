using Godot;
using Godot.Collections;

public partial class UIPlanetInfo : Control
{
    [ExportCategory("Links")]
    [Export]
    private RichTextLabel TitleLabel = null;
    private string TitleLabel_Original = null;
    [Export]
    public Array<UIPlanetInfoItem> Properties = new Array<UIPlanetInfoItem>();

    [ExportCategory("Runtime")]
    [Export]
    public PlanetData _Planet = null;
    //[Export]
    //public DataBlock _Layout = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");

        TitleLabel_Original = TitleLabel.Text;
    }

    public void Refresh(PlanetData planet, string forceTitle = "")
    {
        _Planet = planet;

        string name = forceTitle;
        if (name == "") _Planet.Data.ValueToString();
        if (name == "") name = _Planet.Data.ValueS;

        TitleLabel.Text = TitleLabel_Original.Replace("$name", name);

        Array<DataBlock> subs = _Planet.Data.GetSubs();

        // grow
        while (Properties.Count < subs.Count + 2)
        {
            UIPlanetInfoItem newItem = Properties[0].Duplicate(7) as UIPlanetInfoItem;
            Properties[0].GetParent().AddChild(newItem);
            Properties.Add(newItem);
        }

        for (int idx = 0; idx < Properties.Count; idx++)
        {
            if (idx < subs.Count && subs[idx].ToUIShow())
            {
                Properties[idx].Refresh(subs[idx]);
                Properties[idx].Visible = true;
                if (subs[idx].Name == "Building")
                {
                    Properties[idx].Visible = false;
                }
            }
            /*else if (idx == subs.Count)
            {
                Properties[idx].Refresh("Total yields:", "Total possible yields", false);
                Properties[idx].Visible = true;
            }
            else if (idx == subs.Count + 1)
            {
                Properties[idx].Refresh(_Planet.BaseResources_PerTurn.GetTotalIncomeCondensed(), "Total possible yields", true);
                Properties[idx].Visible = true;
            }*/
            else
            {
                Properties[idx].Visible = false;
            }
        }
    }
}