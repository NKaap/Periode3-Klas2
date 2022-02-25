using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildController : MonoBehaviour
{

    public bool playerInRoom;
    public GameObject playerTarget;
    public bool childMove = true;

    public float radius;
    // Start is called before the first frame update
    void Start()
    {
        //playerTarget = GameObject.FindGameObjectWithTag("TargetPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 4f * Time.deltaTime;

        if (Vector3.Distance(transform.position, playerTarget.transform.position) > 10)
        {
            childMove = true;
        }
        else if (Vector3.Distance(transform.position, playerTarget.transform.position) < 10)
        {
            childMove = false;
        }

        if (childMove)
        {
            
            gameObject.transform.position = Vector3.MoveTowards(transform.position, playerTarget.transform.position, speed);
        }



        //Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        //foreach (Collider collider in colliders)
        //{
        //    if (collider.transform.CompareTag("Player"))
        //    {
        //        childMove = false;
        //        Debug.Log("Yass");
        //    }
        //    else
        //    {

        //        if (Vector3.Distance(transform.position, playerTarget.transform.position) > 3)
        //        {
        //            childMove = true;
        //        }
        //        else if (Vector3.Distance(transform.position, playerTarget.transform.position) < 3)
        //        {
        //            childMove = false;
        //        }

        //    }

    }

    //if (Vector3.Distance(transform.position, playerTarget.transform.position) < 3)
    //{
    //    speed = 0;
    //    transform.position += Vector3.zero;
    //    playerTarget.transform.position *= -1;
    //}
    //else
    //{
    //    gameObject.transform.position = Vector3.MoveTowards(transform.position, playerTarget.transform.position, speed);
    //}


    //private void OnDrawGizmos()
    //{
    //    Color color = Color.magenta;
    //    Gizmos.DrawWireSphere(gameObject.transform.position, radius);
    //}
}
