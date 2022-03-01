using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildController : MonoBehaviour
{
    // ENEMIES IN DE ROOM PREFAB


    public bool playerInRoom;
    public GameObject playerTarget;
    public bool childMove = true;
    public float radius;
    public GameObject childHead;

    // throw pencil
    public GameObject pencilForThrow;
    public Transform shootPos;
    public float pencilSpeed;
    public float speed ;
    // timer
    public float timeLeft = 3f;

    void Update()
    {
        speed = 4 * Time.deltaTime;
        childMove = Vector3.Distance(transform.position, playerTarget.transform.position) > 10;
        childHead.transform.LookAt(playerTarget.transform);
        gameObject.transform.LookAt(playerTarget.transform);

        if (childMove)
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, playerTarget.transform.position, speed);
        }

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            timeLeft = 3;
           // GameObject pencil = Instantiate(pencilForThrow, shootPos.position, Quaternion.identity);
            //pencil.GetComponent<Rigidbody>().AddForce(transform.forward * force);

            Rigidbody pencilRB = Instantiate(pencilForThrow.GetComponent<Rigidbody>(), shootPos.transform.position, shootPos.transform.rotation) as Rigidbody;
            pencilRB.velocity = shootPos.transform.TransformDirection(new Vector3(0, 0, pencilSpeed));
        }
    }
}
