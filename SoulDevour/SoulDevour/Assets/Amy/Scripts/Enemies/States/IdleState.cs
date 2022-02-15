using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public ChaseState chase;
    public bool seesPlayer;

    public override State RunCurrentState()
    {
        if (seesPlayer)
        {
            return chase;
        }
        else
        {
            return this;
        }
    }

  
}
