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
    public float force;


    public float dist;
    // timer

    public float timeLeft = 1.5f;

    void Update()
    {
        ChildAnimations();
       
        speed = 4 * Time.deltaTime;
        childMove = Vector3.Distance(transform.position, playerTarget.transform.position) > 3;
    
        gameObject.transform.LookAt(playerTarget.transform);

        if (childMove)
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, playerTarget.transform.position, speed);
        }

        //timeLeft -= Time.deltaTime;
        //if (timeLeft <= 0)
        //{
        //    timeLeft = 1.5f;
        //    GameObject pencil = Instantiate(pencilForThrow, shootPos.position, Quaternion.identity);
        //    pencil.GetComponent<Rigidbody>().AddForce(transform.forward * force);

        //    Rigidbody pencilRB = Instantiate(pencilForThrow.GetComponent<Rigidbody>(), shootPos.transform.position, shootPos.transform.rotation) as Rigidbody;
        //    pencilRB.velocity = shootPos.transform.TransformDirection(new Vector3(0, 0, pencilSpeed));
        //}


        if (health <= 0)
        {
            //Instantiate(coinPrefab, transform.position, new Quaternion(0, 0, 0, 0));
            Destroy(gameObject);
            // set ragdoll active
        }
    }

    public void ChildAnimations()
    {
        dist = Vector3.Distance(playerTarget.transform.position, gameObject.transform.position);
        if (dist < 3f)
        {
            childAnimator.SetBool("Walk", false);
            childAnimator.SetBool("Rest", true);
        }
        else
        {
            childAnimator.SetBool("Rest", false);
            childAnimator.SetBool("Walk", true);
            // timer so they slap u once every 2 seconds
        }
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bullet") )
        {
            health -= 1;
        }

        
    }
}
