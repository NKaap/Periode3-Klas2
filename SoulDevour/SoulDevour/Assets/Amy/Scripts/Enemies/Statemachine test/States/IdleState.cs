using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public ChaseState chase;
    public bool seesPlayer;
    public float idleRadius = 10f;
    public GameObject player;

    public override State RunCurrentState()
    {
        
       

        Collider[] colliders = Physics.OverlapSphere(transform.position, idleRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.transform.CompareTag("Player"))
            {

                seesPlayer = true;
            }
            else
            {
                seesPlayer = false;
            }
        }


        if (seesPlayer)
        {
            return chase;
        }
        else
        {
            // code voor idle
            return this;
        }
    }

  
}
