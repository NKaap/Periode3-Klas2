using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherController : MonoBehaviour
{
    public float range;

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            if (collider.transform.CompareTag("Child"))
            {
                // doe kind onder de hand van de teacher, gooi kind, als de hand "leeg" is, spawn nieuw kind, doe alles opnieuw.
                // teacher loopt ook een beetje rond, en achter je aan.


                Debug.Log("Yass");
            }
        }
    }
}
