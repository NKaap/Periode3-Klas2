using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildController : MonoBehaviour
{
    // ENEMIES IN DE ROOM PREFAB

    [Header("Child Target Variables")]
    public bool playerInRoom;
    public GameObject playerTarget;
    public bool childMove = true;

    
    [Header("Child Variables")]
    [Space(8)]
    public GameObject childHead;
    public float speed;

    // throw pencil
    
    [Header("Shooting Variables")]
    [Space(8)]
    public GameObject pencilForThrow;
    public Transform shootPos;
    public float pencilSpeed;

    // timer
    
    public float timeLeft = 3f;

    void Update()
    {
        speed = 4 * Time.deltaTime;
        childMove = Vector3.Distance(transform.position, playerTarget.transform.position) > 6;
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
