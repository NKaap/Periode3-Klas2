using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public AttackState attack;
    public bool attackRange;
    public float radius = 4; // radius of the sphere

    public override State RunCurrentState()
    {
        //gameObject.transform.position = player.transform.position * Time.deltaTime;

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            if (collider.transform.CompareTag("Player"))
            {
                //float distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
                //if(distance <= 5)
                //{
                //    attackRange = true;
                //}
                //else
                //{
                //    attackRange = false;
                //}
                
            }
        }


        if (attackRange)
        {
            return attack;
        }
        else
        {
            // code voor chase
            return this;
        }
    }

   
}
