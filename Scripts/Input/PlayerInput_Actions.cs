using Godot;
using Godot.Collections;
using System.Collections.Generic;

// Generated
public partial class PlayerInput : Node
{
    //private InputState StartState = InputState.FAR_GALAXY;


    // --------------------------------------------------------------------------------- ACTIONS ECONOMY
    public void OnAction_Economy_Colonize()
    {
        //if (SelectedStar.System == null || SelectedStar.System._Player != Game.self.HumanPlayer) return;
        //
        //switch (State)
        //{
        //    case InputState.FAR_STAR:
        //    case InputState.CLOSE_STAR:
        //        CS_Action_from_Star_to_Select_Planet(SelectedStar.System.ActionEconomyColonize_PerTurn);
        //        break;
        //    case InputState.CLOSE_PLANET:
        //        break;
        //}
    }
    
    // --------------------------------------------------------------------------------- ACTIONS ECONOMY
    public void OnAction_Economy_SelectPlanet<T>(List<T> possibleActions) where T : ActionBase
    {
        //switch (State)
        //{
        //    case InputState.AE_SELECT_PLANET:
        //        CS_Action_from_Select_Planet_to_Select_Choice(possibleActions);
        //        break;
        //}
    }

    public void OnExecuteAction(ActionBase action)
    {
        //switch (State)
        //{
        //    case InputState.AE_SELECT_CHOICE:
        //        CS_Action_from_Select_Choice_to_Star();
        //        break;
        //}
    }

    // ---------------------------------------------------------------------------------
    public void OnCancelAction()
    {
        //switch (State)
        //{
        //    case InputState.AE_SELECT_PLANET:
        //        CS_Action_from_Select_Planet_to_Star();
        //        break;
        //    case InputState.AE_SELECT_CHOICE:
        //        CS_Action_from_Select_Choice_to_Select_Planet();
        //        break;
        //}
    }

    // ---------------------------------------------------------------------------------

    public void CS_Action_from_Star_to_Select_Planet<T>(List<T> possibleActions) where T : ActionBase
    {
        //StartState = State;
        //
        //Game.self.UIGalaxy.ShowStarInfo_ActionSelectPlanet(SelectedStar, possibleActions);
        //
        //State = InputState.AE_SELECT_PLANET;
        //LockedInput = true;
    }

    // ---------------------------------------------------------------------------------
    public void CS_Action_from_Select_Planet_to_Select_Choice<T>(List<T> possibleActions) where T : ActionBase
    {
        //Game.self.UIGalaxy.ShowStarInfo_ShowMakeChoice(SelectedStar, possibleActions);
        //
        //State = InputState.AE_SELECT_CHOICE;
        //LockedInput = true;
    }

    // ---------------------------------------------------------------------------------
    public void CS_Action_from_Select_Choice_to_Select_Planet()
    {
        //Game.self.UIGalaxy.ShowStarInfo_CancelMakeChoice();
        //
        //State = InputState.AE_SELECT_PLANET;
    }

    // ---------------------------------------------------------------------------------
    public void CS_Action_from_Select_Planet_to_Star()
    {
        //Game.self.UIGalaxy.ShowStarInfo_CancelAction(SelectedStar);
        //
        //State = StartState;
        //LockedInput = false;
    }

    // ---------------------------------------------------------------------------------
    public void CS_Action_from_Select_Choice_to_Star()
    {
        //Game.self.UIGalaxy.ShowStarInfo(SelectedStar, null);
        //Game.self.UIGalaxy.ShowStarInfo_CancelAction(SelectedStar);
        //
        //State = StartState;
        //LockedInput = false;
    }
}
