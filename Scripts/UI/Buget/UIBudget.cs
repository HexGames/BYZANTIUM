using Godot;
using Godot.Collections;
using System.Collections.Generic;

//[Tool]
public partial class UIBudget : Control
{
    [ExportCategory("Links")]
    [Export]
    public Array<UIBudgetItemProject> ProjectsProduction = new Array<UIBudgetItemProject>();
    [Export]
    public UIBudgetItemTreasury TreasuryProduction = null;
    //[Export]
    //public Array<UIBudgetItemProject> ProjectsEnergy = new Array<UIBudgetItemProject>();
    //[Export]
    //public UIBudgetItemTreasury TreasuryEnergy = null;
    //[Export]
    //public UIBudgetItemUpkeep Upkeep = null;

    [Export]
    public Label Title = null;
    [Export]
    public RichTextLabel Description = null;
    [Export]
    public RichTextLabel CostLabel = null;
    [Export]
    public Button CancelBtn = null;

    [ExportCategory("Runtime")]
    public SectorData _Data;

    Game Game;

    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            Game = GetNode<Game>("/root/Main/Game");
        }
    }

    public void RefreshBudget(SectorData sector, int production, bool edit, bool showCancel)
    {
        // budget
        _Data = sector;

        //DataBlock production = _Data.GetSub("Production");
        //if (production != null)
        {
            // projects
            //Array<DataBlock> projects = production.GetSubs("Project");

            List<BudgetWrapper.Info> projects = new List<BudgetWrapper.Info>();
            BudgetWrapper.Info treasury = null;
            //projects.AddRange(sector.BudgetPerTurn.Items);
            //for (int idx = 0; idx < projects.Count; idx++)
            //{
            //    if (projects[idx].IsTreasury)
            //    {
            //        treasury = projects[idx];
            //        projects.RemoveAt(idx);
            //        idx--;
            //        break;
            //    }
            //}

            //// grow
            //while (ProjectsProduction.Count < projects.Count - 1)
            //{
            //    UIBudgetItemProject newProject = ProjectsProduction[0].Duplicate(7) as UIBudgetItemProject;
            //    ProjectsProduction[0].GetParent().AddChild(newProject);
            //    ProjectsProduction.Add(newProject);
            //}
            //
            //for (int idx = 0; idx < ProjectsProduction.Count; idx++)
            //{
            //    if (idx < projects.Count)
            //    {
            //        int locked = projects[idx].Locked;
            //        int unlocked = projects[idx].Value - locked;
            //        ProjectsProduction[idx].Refresh(projects[idx].Name, locked, unlocked, 0, sector.BudgetPerTurn.GetProduction(sector.BudgetPerTurn.Items[idx].Name, production));
            //        ProjectsProduction[idx].Visible = true;
            //    }
            //    else
            //    {
            //        ProjectsProduction[idx].Visible = false;
            //    }
            //}

            //// treasury
            //{
            //    int locked = treasury.Locked;
            //    int unlocked = treasury.Value - locked;
            //    TreasuryProduction.Refresh(locked, unlocked, 0, production);
            //}
        }
        //else
        //{
        //    for (int idx = 0; idx < ProjectsProduction.Count; idx++)
        //    {
        //        ProjectsProduction[idx].Visible = false;
        //    }
        //    TreasuryProduction.Visible = false;
        //}

        CancelBtn.Visible = showCancel;
    }

    public void OnSet()
    {

    }

    public void OnCancel()
    {

    }
}