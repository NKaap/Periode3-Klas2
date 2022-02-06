using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour 
{
   

    // BASE BULLET == PROPJE PAPIER - met een curve 
    public Transform bullet;
    public float baseDamage;
   
    public MovPlayer source;


    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.TryGetComponent<ChildBase>(out ChildBase huts))
        //{
        //    huts.SubtractHealth(CalculateDamage());
        //    DoEffect();
        //    Destroy(gameObject);
        //}
        //if (collision.gameObject)
        //{
        //    DoEffect();
        //    Destroy(gameObject);
        //}
    }


    public float CalculateDamage()
    {
        float output = baseDamage;
        // edit output
        return output;
    }

    public void DoEffect()
    {
        foreach (ItemBase item in source.items)
        {
            switch (item.itemtype)
            {
               
            }
        }
    }
}
