using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UISystemPanel : Panel
{
    [ExportCategory("Links")]
    [Export]
    public Control ContentParent = null;
    [Export]
    public Array<Label> Properties = new Array<Label>();
    [Export]
    public Array<Panel> Lines = new Array<Panel>();

    [ExportCategory("Runtime")]
    [Export]
    public DataBlock _Data = null;

    Game Game;
    public override void _Ready()
    {
        Game = GetNode<Game>("/root/Main/Game");
    }

    public void Clear()
    {
        for (int idx = 0; idx < Properties.Count; idx++)
        {
            Properties[idx].Visible = false;
        }
        for (int idx = 0; idx < Lines.Count; idx++)
        {
            Lines[idx].Visible = false;
        }
    }

    public void Refresh(List<UISystem.PropertyInfo> infos)
    {
        Clear();

        int maxRow = 0;
        for (int idx = 0; idx < infos.Count; idx++)
        {
            maxRow = Mathf.Max(infos[idx].Row, maxRow);
        }

        //unparent
        for (int idx = 0; idx < Properties.Count; idx++)
        {
            ContentParent.RemoveChild(Properties[idx]);
        }
        for (int idx = 0; idx < Lines.Count; idx++)
        {
            ContentParent.RemoveChild(Lines[idx]);
        }

        // grow
        while (Properties.Count < infos.Count)
        {
            Label newProp = Properties[0].Duplicate(7) as Label;
            Properties.Add(newProp);
        }

        while (Lines.Count < maxRow)
        {
            Panel newLine = Lines[0].Duplicate(7) as Panel;
            Lines.Add(newLine);
        }

        int lineIdx = 0;
        for (int idx = 0; idx < infos.Count; idx++)
        {
            if (infos[idx].Row > lineIdx)
            {
                ContentParent.AddChild(Lines[lineIdx]);
                Lines[lineIdx].Visible = true;
                lineIdx++;
            }

            ContentParent.AddChild(Properties[idx]); 
            StyleBoxFlat styleBox = new StyleBoxFlat();
            styleBox.BgColor = infos[idx].BGColor;
            Properties[idx].Text = " " + infos[idx].Text + " ";
            Properties[idx].AddThemeStyleboxOverride("normal", styleBox);
            if (infos[idx].Tooltip != "") Properties[idx].TooltipText = infos[idx].Tooltip;
            Properties[idx].Visible = true;
        }

        for (int idx = infos.Count; idx < Properties.Count; idx++)
        {
            ContentParent.AddChild(Properties[idx]);
            Properties[idx].Visible = false;
        }

        for (int idx = lineIdx; idx < Lines.Count; idx++)
        {
            ContentParent.AddChild(Lines[idx]);
            Lines[idx].Visible = false;
        }
    }
}