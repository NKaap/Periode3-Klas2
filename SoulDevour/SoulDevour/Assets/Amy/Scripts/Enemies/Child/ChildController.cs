using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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

   

    public Animator childAnimator;
    public bool isMoving;
    public Rigidbody rb;


    public GameObject childRagdoll;
    // throw pencil

    //[Header("Shooting Variables")]
    //[Space(8)]
    //public GameObject pencilForThrow;
    //public Transform shootPos;
    //public float pencilSpeed;
    //public float force;
    // public GameObject coinPrefab;


    public NavMeshAgent agent;
    public float detectionRange = 10f;

    public float dist;
    // timer


    public float radius = 10f;
    // public float timeLeft = 1.5f;

    private void Start()
    {

        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        //Collider[] colliders = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z), radius);
        //foreach (Collider collider in colliders)
        //{
        //    if (collider.transform.CompareTag("Player"))
        //    {
        //        ChildAnimations();

        //        speed = 4 * Time.deltaTime;
        //        childMove = Vector3.Distance(transform.position, playerTarget.transform.position) > 3;

        //        gameObject.transform.LookAt(playerTarget.transform);

        //        if (childMove)
        //        {
        //            gameObject.transform.position = Vector3.MoveTowards(transform.position, playerTarget.transform.position, speed);
        //        }
        //    }
        //}

        ChildAnimations();
        childHead.transform.LookAt(playerTarget.transform);

      //  childMove = Vector3.Distance(transform.position, playerTarget.transform.position) > 3;

        if (health <= 0)
        {
            //Instantiate(coinPrefab, transform.position, new Quaternion(0, 0, 0, 0));
            Destroy(gameObject);
            // set ragdoll active
        }

        if (playerTarget == null)
        {
            playerTarget = GameObject.FindGameObjectWithTag("Player");
        }
        float distance = Vector3.Distance(playerTarget.transform.position, transform.position);

        if (distance <= detectionRange)
        {
            agent.SetDestination(playerTarget.transform.position); // de enemy volgt de player

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();

            }
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


    public void FaceTarget()
    {
        Vector3 direction = (playerTarget.transform.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 100f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, detectionRange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            health -= 1;
        }


    }

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
