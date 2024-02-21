using Godot;
using Godot.Collections;
using System.Collections.Generic;

//[Tool]
public partial class UIBudget : Control
{
    [ExportCategory("Links")]
    [Export]
    public Array<UIBudgetItemBuilding> Buildings = new Array<UIBudgetItemBuilding>(); // UNUSED !!!
    [Export]
    public Array<UIBudgetItemProject> ProjectsMinerals = new Array<UIBudgetItemProject>();
    [Export]
    public UIBudgetItemTreasury TreasuryMinerals = null;
    [Export]
    public Array<UIBudgetItemProject> ProjectsEnergy = new Array<UIBudgetItemProject>();
    [Export]
    public UIBudgetItemTreasury TreasuryEnergy = null;
    [Export]
    public UIBudgetItemUpkeep Upkeep = null;

    [Export]
    public Label Title = null;
    [Export]
    public RichTextLabel Description = null;
    [Export]
    public RichTextLabel CostLabel = null;
    [Export]
    public Button CancelBtn = null;

    [ExportCategory("Runtime")]
    public DataBlock _Data;

    Game Game;

    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            Game = GetNode<Game>("/root/Main/Game");
        }
    }

    public void RefreshBudget(DataBlock budgetData, bool edit, bool showCancel)
    {
        // clear buildings
        for (int idx = 0; idx < Buildings.Count; idx++)
        {
            Buildings[idx].Visible = false;
        }

        // budget
        _Data = budgetData;

        DataBlock minerals = _Data.GetSub("Minerals");
        if (minerals != null)
        {
            // projects
            Array<DataBlock> projects = minerals.GetSubs("Project");

            // grow
            while (ProjectsMinerals.Count < projects.Count)
            {
                UIBudgetItemProject newProject = ProjectsMinerals[0].Duplicate(7) as UIBudgetItemProject;
                ProjectsMinerals[0].GetParent().AddChild(newProject);
                ProjectsMinerals.Add(newProject);
            }

            for (int idx = 0; idx < ProjectsMinerals.Count; idx++)
            {
                if (idx < projects.Count)
                {
                    ProjectsMinerals[idx].Refresh(projects[idx], 0, 300, 2967);
                    ProjectsMinerals[idx].Visible = true;
                }
                else
                {
                    ProjectsMinerals[idx].Visible = false;
                }
            }

            // treasury
            TreasuryMinerals.Refresh(minerals.GetSub("Treasury"), 0, 432, 2967);
        }
        else
        {
            for (int idx = 0; idx < ProjectsMinerals.Count; idx++)
            {
                ProjectsMinerals[idx].Visible = false;
            }
            TreasuryMinerals.Visible = false;
        }

        DataBlock energy = _Data.GetSub("Energy");
        if (energy != null)
        {
            // projects
            Array<DataBlock> projects = energy.GetSubs("Project");

            // grow
            while (ProjectsEnergy.Count < projects.Count)
            {
                UIBudgetItemProject newProject = ProjectsEnergy[0].Duplicate(7) as UIBudgetItemProject;
                ProjectsEnergy[0].GetParent().AddChild(newProject);
                ProjectsEnergy.Add(newProject);
            }

            for (int idx = 0; idx < ProjectsEnergy.Count; idx++)
            {
                if (idx < projects.Count)
                {
                    ProjectsEnergy[idx].Refresh(projects[idx], 0, 300, 2967);
                    ProjectsEnergy[idx].Visible = true;
                }
                else
                {
                    ProjectsEnergy[idx].Visible = false;
                }
            }

            // upkeep
            Upkeep.Refresh(578, 2967);

            // treasury
            TreasuryEnergy.Refresh(energy.GetSub("Treasury"), 0, 432, 2967);
        }
        else
        {
            for (int idx = 0; idx < ProjectsEnergy.Count; idx++)
            {
                ProjectsEnergy[idx].Visible = false;
            }
            TreasuryEnergy.Visible = false;
        }

        CancelBtn.Visible = showCancel;
    }

    public void OnSet()
    {

    }

    public void OnCancel()
    {

    }
}