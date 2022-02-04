using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour 
{
   

    // BASE BULLET == PROPJE PAPIER - met een curve 
    public Transform bullet;
    public float baseDamage;
   
    public MovPlayer source;

    public GameObject peanutEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<ChildBase>(out ChildBase huts))
        {
            huts.SubtractHealth(CalculateDamage());
            DoEffect();
            Destroy(gameObject);
        }
        if (collision.gameObject)
        {
            DoEffect();
            Destroy(gameObject);
        }
    }


    public float CalculateDamage()
    {
        float output = baseDamage;
        
        return output;
    }

    public void DoEffect()
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
