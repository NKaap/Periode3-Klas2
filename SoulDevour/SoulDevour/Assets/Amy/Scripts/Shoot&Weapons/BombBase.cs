using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBase : MonoBehaviour
{
    // BASE BOMB == NIKS - met een curve 
    public Transform bomb;
    public float baseDamage;

    public MovPlayer source;

    public GameObject peanutEffect;
 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<ChildBase>(out ChildBase huts))
        {
            huts.SubtractHealth(CalculateBombDamage());
            DoBombEffect();
            Destroy(gameObject);
        }
        if (collision.gameObject)
        {
            DoBombEffect();
            Destroy(gameObject);
        }
    }


    public float CalculateBombDamage()
    {
        float output = baseDamage;
        // edit output
        return output;
    }

    public void DoBombEffect()
    {
        foreach (ItemBase item in source.items)
        {
            switch (item.itemtype)
            {
                case ItemBase.ItemType.PeanutButter:
                    {
                        Instantiate(peanutEffect, transform.position, new Quaternion(0, 0, 0, 0));
                        break;
                    }
            }
        }
    }
}
