using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UIStateMachine
{
    public ActionBase.ID ActionSelected = ActionBase.ID.DUMMY;

    public void Action(ActionBase.ID actionID)
    {
        ActionSelected = actionID;

        //StateID oldState = State; - not needed, as ExitState does not change State

        ExitState();

        if (StarSelected != null) PickPossibleActions(StarSelected);
        else if (PlanetSelected != null) PickPossibleActions(PlanetSelected._Star);

        if (ActionSelected == ActionBase.ID.ECONOMY_COLONIZE)
        {
            if (PossibleActions.Count > 0 && PlanetSelected != null)
            {
                PickPossibleActionsForPlanet(PlanetSelected);
                EnterState(StateID.ACTION_SELECT_CHOICE);
            }
            else if (PossibleActions.Count > 0 && StarSelected != null)
            {
                EnterState(StateID.ACTION_SELECT_PLANET);
            }
            else EnterState(State); // should never happen
        }
        else
        {
            if (StarSelected != null) EnterState(StateID.ACTION_SELECT_CHOICE);
            else EnterState(State); // should never happen)
        }
    }

    public void Action_SelectPlanet(PlanetData planet)
    {
        ExitState();

        PickPossibleActionsForPlanet(planet);

        if (PossibleActions_forPlanet.Count > 0) EnterState(StateID.ACTION_SELECT_CHOICE);
        else EnterState(StateID.ACTION_SELECT_PLANET); // should never happen
    }


    private void PickPossibleActions(StarData star)
    {
        PossibleActions.Clear();

        switch(ActionSelected)
        {
            case ActionBase.ID.ECONOMY_COLONIZE: if (star.System != null) PossibleActions.AddRange(star.System.ActionEconomyColonize_PerTurn); break;
            case ActionBase.ID.DUMMY: return;
        }
    }

    private void PickPossibleActionsForPlanet(PlanetData forPlanet)
    {
        PossibleActions_forPlanet.Clear();
        for (int idx = 0; idx < PossibleActions.Count; idx++)
        {
            if (PossibleActions[idx] is ActionPlanet)
            {
                ActionPlanet action = PossibleActions[idx] as ActionPlanet;
                if (action.Planet == forPlanet)
                {
                    PossibleActions_forPlanet.Add(action);
                }
            }
        }
    }
}

