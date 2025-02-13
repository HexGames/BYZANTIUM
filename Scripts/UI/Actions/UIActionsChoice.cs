using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class UIActionsChoice : Control
{
    [Export]
    public Array<UIActionsChoiceItem> Choices = new Array<UIActionsChoiceItem>();
    //[Export]
    //public TextureButton ButtonOk;
    [Export]
    public TextureButton ButtonCancel;

    public void Refresh<T>(List<T> possibleActions) where T : ActionBase
    {
        while (Choices.Count < possibleActions.Count)
        {
            UIActionsChoiceItem newItem = Choices[0].Duplicate(7) as UIActionsChoiceItem;
            Choices[0].GetParent().AddChild(newItem);
            Choices.Add(newItem);
        }

        for (int idx = 0; idx < Choices.Count; idx++)
        {
            if (idx < possibleActions.Count)
            {
                Choices[idx].Visible = true;
                Choices[idx].Refresh(possibleActions[idx]);
            }
            else
            {
                Choices[idx].Visible = false;
            }
        }

        //ButtonOk
    }

    public void OnCancel()
    {
        Game.self.Input.OnCancelAction();
    }
}