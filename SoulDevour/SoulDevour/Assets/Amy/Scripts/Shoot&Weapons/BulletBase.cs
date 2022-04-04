using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour 
{
    
    // BASE BULLET == PROPJE PAPIER - met een curve 
    public Transform bullet;

    public float baseDamage = 1;
    public float calculatedDamage => CalculateDamage();

    public MovPlayer source;
    public Transform fireParticle;


    private void OnCollisionEnter(Collision collision)
    {
      
        if (collision.gameObject.transform.CompareTag("EndBoss"))
        {
            GameObject coll = collision.gameObject.GetComponent<EndBossController>().gameObject;
            coll.GetComponent<EndBossController>().health -= calculatedDamage;
           
        }
        if (collision.gameObject.transform.CompareTag("Teacher"))
        {
            GameObject coll = collision.gameObject.GetComponent<TeacherController>().gameObject;
            coll.GetComponent<TeacherController>().health -= calculatedDamage;

        }
        if (collision.gameObject.transform.CompareTag("LivingChild"))
        {
            GameObject collChild = collision.gameObject.GetComponent <ChildTest>().gameObject;
            collChild.GetComponent<ChildTest>().health -= calculatedDamage;
           
        }
        if (collision.gameObject)
        {
            Destroy(gameObject);
        }
    }


    public float CalculateDamage() // doesnt work.
    {
        float output = baseDamage;

        foreach (ItemBase.ItemType item in source.items)
        {
            switch (item)
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
        foreach (ItemBase.ItemType item in source.items)
        {
            switch (item)
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
