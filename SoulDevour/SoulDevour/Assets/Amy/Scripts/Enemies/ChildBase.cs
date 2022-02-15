using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildBase : MonoBehaviour
{

    // Statemachines 

    // Child Enemy Script
    public GameObject target; // player // is altijd hetzelfde
    public bool ragDoll = false;

    
    private float health;

    public void SubtractHealth(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            ragDoll = true;
            // set ragdoll = true;
        }
    }
}
