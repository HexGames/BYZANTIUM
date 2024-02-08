using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIGalaxyResources : Control
{
    [ExportCategory("Links")]
    [Export]
    public Array<Label> Resources = new Array<Label>();

    [ExportCategory("Runtime")]
    [Export]
    public DataBlock _Data = null;

    Game Game;

    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");
    }

    public void Refresh(DataBlock playerResources)
    {
        ResourcesWrapper resInfo = new ResourcesWrapper(playerResources);

        // grow
        while (Resources.Count < resInfo.Resources.Count)
        {
            Label newResLabel = Resources[0].Duplicate(7) as Label;
            Resources[0].GetParent().AddChild(newResLabel);
            Resources.Add(newResLabel);
        }

        // update
        for ( int idx = 0; idx < Resources.Count; idx++ ) 
        {
            if (idx < resInfo.Resources.Count)
            {
                switch(resInfo.Resources[idx].ResType)
                {
                    case ResourcesWrapper.Info.Type.VALUE: Resources[idx].Text = resInfo.Resources[idx].Value_1.ToString() + " " + DataBlock.ResToUIString(resInfo.Resources[idx].Name); break;
                    case ResourcesWrapper.Info.Type.VALUE_INCOME: Resources[idx].Text = resInfo.Resources[idx].Value_1.ToString() + " (" + (resInfo.Resources[idx].Value_2 > 0 ? "+" : "") + resInfo.Resources[idx].Value_2.ToString() + ") " + DataBlock.ResToUIString(resInfo.Resources[idx].Name); break;
                    case ResourcesWrapper.Info.Type.INCOME: Resources[idx].Text = (resInfo.Resources[idx].Value_2 > 0 ? "+" : "") + resInfo.Resources[idx].Value_2.ToString() + " " + DataBlock.ResToUIString(resInfo.Resources[idx].Name); break;
                    case ResourcesWrapper.Info.Type.TOTAL_USED: Resources[idx].Text = resInfo.Resources[idx].Value_2.ToString() + "/" + resInfo.Resources[idx].Value_1.ToString() + " " + DataBlock.ResToUIString(resInfo.Resources[idx].Name); break;
                }
                Resources[idx].Visible = true;
            }
            else
            {
                Resources[idx].Visible = false;
            }
        }
    }
}