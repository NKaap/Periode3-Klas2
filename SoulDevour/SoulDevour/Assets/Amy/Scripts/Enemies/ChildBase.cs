using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildBase : MonoBehaviour
{
    // Child Enemy Script

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
