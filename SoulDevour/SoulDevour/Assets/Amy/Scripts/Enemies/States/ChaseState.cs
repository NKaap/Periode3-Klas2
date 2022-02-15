using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public AttackState attack;
    public bool attackRange;


    public override State RunCurrentState()
    {
        if(attackRange)
        {
            return attack;
        }
        else
        {
            return this;
        }
    }

   
}
