using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour 
{
    
    // BASE BULLET == PROPJE PAPIER - met een curve 
    public Transform bullet;
    public float baseDamage => CalculateDamage();
   
    public MovPlayer source;
    public Transform fireParticle;

   
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

        foreach (ItemBase item in source.items)
        {
            switch (item.itemtype)
            {
                case ItemBase.ItemType.PropjePapier: // base bullet
                    {
                        output += 1;
                        break;
                    }
                case ItemBase.ItemType.Fireballs:
                    {
                        output += 2;
                        break;
                    }
                case ItemBase.ItemType.Crown:
                    {
                        output += 2;
                        break;
                    }
                case ItemBase.ItemType.Scissor:
                    {
                        output += 4;
                        break;
                    }
                case ItemBase.ItemType.Tooth:
                    {
                        output += 3;
                        break;
                    }

            }
        }

        // edit output
        return output;
    }

    public void DoEffect()
    {
        foreach (ItemBase item in source.items)
        {
            switch (item.itemtype)
            {
                case ItemBase.ItemType.Fireballs:
                    {
                       // fireParticle;
                        break;
                    }
            }
        }
    }
}
