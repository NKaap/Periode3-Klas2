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
    public float health = 2;
    public GameObject childHead;
    public float speed;
    public GameObject coinPrefab;

    public Animator childAnimator;
    public bool isMoving;
    public Rigidbody rb;
    // throw pencil
    
    [Header("Shooting Variables")]
    [Space(8)]
    public GameObject pencilForThrow;
    public Transform shootPos;
    public float pencilSpeed;

    // timer
    
    public float timeLeft = 1.5f;

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
            timeLeft = 1.5f;
           // GameObject pencil = Instantiate(pencilForThrow, shootPos.position, Quaternion.identity);
            //pencil.GetComponent<Rigidbody>().AddForce(transform.forward * force);

            Rigidbody pencilRB = Instantiate(pencilForThrow.GetComponent<Rigidbody>(), shootPos.transform.position, shootPos.transform.rotation) as Rigidbody;
            pencilRB.velocity = shootPos.transform.TransformDirection(new Vector3(0, 0, pencilSpeed));
        }
    }

    public void ChildAnimations()
    {
        if (rb.velocity.magnitude > 0.01)
        {
            isMoving = true;
            childAnimator.SetBool("Walk", isMoving);
        }
        if (rb.velocity.magnitude <= 0.01)
        {
            isMoving = false;
            childAnimator.SetBool("Walk", isMoving);
        }
    }

    public void ChildDead()
    {
        if(health <= 0)
        {
            Instantiate(coinPrefab, transform.position, new Quaternion(0, 0, 0, 0));
            Destroy(gameObject);
            
        }
    }
}
