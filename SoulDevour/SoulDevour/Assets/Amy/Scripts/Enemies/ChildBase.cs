using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildBase : MonoBehaviour
{

    private float health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SubtractHealth(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
