using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildController : MonoBehaviour
{
    public bool playerInRoom;
    public GameObject playerTarget;
    public bool childMove = true;
    public float radius;
    public GameObject childHead;
   
    void Update()
    {
        float speed = 4f * Time.deltaTime;
        childMove = Vector3.Distance(transform.position, playerTarget.transform.position) > 10;
        childHead.transform.LookAt(playerTarget.transform);
        gameObject.transform.LookAt(playerTarget.transform);

        if (childMove)
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, playerTarget.transform.position, speed);
        }

    }
}
