using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChildStateMachine : MonoBehaviour
{
    protected ChildState currentState;

    private void Update()
    {
        
    }

    private void RunStateMachine()
    {
        ChildState nextState = currentState;
    }
    
}
