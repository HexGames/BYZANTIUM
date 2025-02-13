using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIActionsChoiceItem : Control
{
    public UIText Title;
    public TextureRect Icon;
    public UIText Description;
    public UIText Cost;
    public UIText Effect;

    // runtime
    ActionBase _Action;

    public override void _Ready()
    {
        Title = GetNode<UIText>("MarginContainer/VBoxContainer2/Middle/_Title");
        Icon = GetNode<TextureRect>("MarginContainer/VBoxContainer2/Middle/_Icon");
        Description = GetNode<UIText>("MarginContainer/VBoxContainer2/Middle/_Description");
        Cost = GetNode<UIText>("MarginContainer/VBoxContainer2/Middle/_Cost");
        Effect = GetNode<UIText>("MarginContainer/VBoxContainer2/Middle/_Effect");
    }

    public void Refresh(ActionBase actionBase)
    {
        _Action = actionBase;

        if (_Action is ActionEconomyColonize)
        {
            Refresh_Colonize(_Action as ActionEconomyColonize);
        }
    }

    public void Refresh_Colonize(ActionEconomyColonize action)
    {
        if (action.DistrictDef != null)
        {
            Title.SetTextWithReplace("$district", action.DistrictDef.Name, "$planet", action.Planet.PlanetName);
            Description.SetTextWithReplace("$district", action.DistrictDef.Name);
        }
        else
        {
            Title.SetTextWithReplace("$district", "New Colony");
            Description.SetTextWithReplace("$district", "new Colony");
        }

        Icon.Texture = Game.self.Def.AssetLib.GetTexture2D_Planet(action.Planet.Data.GetSubValueS("Type") + ".png");

        Cost.SetTextWithReplace("$val", Helper.ResValueToString(action.Cost_BC), "$t", action.Time.ToString());

        Effect.SetTextWithReplace("$effects", "---");
    }

    public void OnAction()
    {
        _Action.ExecuteOrder();

        Game.self.Input.OnExecuteAction(_Action);
    }
}