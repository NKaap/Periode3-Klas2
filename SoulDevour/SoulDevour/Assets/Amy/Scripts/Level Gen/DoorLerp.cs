using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorLerp : MonoBehaviour
{

    public float radius = 2;

    public Transform doorpos1, doorpos2;

    public bool side1, side2;

    
    // Update is called once per frame
    void Update()
    {
        Collider[] collidersOne = Physics.OverlapSphere(doorpos1.position, radius);
        bool outOfRange = false;
        foreach (Collider collider1 in collidersOne)
        {    
            if (collider1.transform.CompareTag("Player"))
            {          
                // voor nu 0,0,0, ff testen als muren er goed in zitten 
                outOfRange = true;
            }
        }

        if (outOfRange && side1)
        {   
            gameObject.transform.eulerAngles = new Vector3(
                gameObject.transform.eulerAngles.x,
                gameObject.transform.eulerAngles.y ,
                gameObject.transform.eulerAngles.z + 80 
                 );
           
            side1 = false;
        }
        else if (!outOfRange && !side1)
        {
            StartCoroutine(WaitTime1());
            side1 = true;
        }


        Collider[] collidersTwo = Physics.OverlapSphere(doorpos2.position, radius);
        bool outOfRange2 = false;
        foreach (Collider collider in collidersTwo)
        {
            if (collider.transform.CompareTag("Player"))
            {
            // voor nu 0,0,0, ff testen als muren er goed in zitten 
                Debug.Log("Yass");
                outOfRange2 = true;
            } 

        }
        if (outOfRange2 && side2)
        {
            gameObject.transform.eulerAngles = new Vector3(
                gameObject.transform.eulerAngles.x,
                gameObject.transform.eulerAngles.y ,
                gameObject.transform.eulerAngles.z - 80
                 );
           
            side2 = false;
        }
        else if (!outOfRange2 && !side2)
        {
            StartCoroutine(WaitTime2());
            side2 = true;
        }

    }
    
    IEnumerator WaitTime1()
    {
        yield return new WaitForSeconds(2f);
        gameObject.transform.eulerAngles = new Vector3(
            gameObject.transform.eulerAngles.x,
            gameObject.transform.eulerAngles.y,
            gameObject.transform.eulerAngles.z - 80
            );
      
    }

    IEnumerator WaitTime2()
    {
        yield return new WaitForSeconds(2f);
        gameObject.transform.eulerAngles = new Vector3(
          gameObject.transform.eulerAngles.x,
          gameObject.transform.eulerAngles.y,
          gameObject.transform.eulerAngles.z + 80
          );
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(doorpos1.position, radius);
        Gizmos.DrawWireSphere(doorpos2.position, radius);
    }

    
}
