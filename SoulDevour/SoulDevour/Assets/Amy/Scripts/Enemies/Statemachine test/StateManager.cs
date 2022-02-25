using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
   [SerializeField] protected State currentState;

    private void Update()
    {

    }

    private void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState();

        if (nextState == null)
        {
            // switch to next state 
            SwitchNextState(nextState);
        }
    }

    void SwitchNextState(State NextState)
    {
        currentState = NextState;
    }
}
