using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildBase : MonoBehaviour
{

    // Statemachines 

    // Child Enemy Script
    public GameObject target; // player // is altijd hetzelfde



    private float health;

    public void SubtractHealth(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
