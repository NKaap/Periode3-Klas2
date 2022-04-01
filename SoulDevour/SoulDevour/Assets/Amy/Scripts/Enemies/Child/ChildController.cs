using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChildController : MonoBehaviour
{
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

    public NavMeshAgent agent;
    public float detectionRange = 10f;

    public float dist;
    public bool following = false;

    public float timeLeft = 2;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTarget = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        float distance = Vector3.Distance(playerTarget.transform.position, transform.position);
        childHead.transform.LookAt(playerTarget.transform);
      
        if (health <= 0)
        {
            Instantiate(childRagdoll, transform.position, new Quaternion(0, 0, 0, 0));
            Destroy(gameObject);
        }

        if (distance <= detectionRange)
        {
            agent.SetDestination(playerTarget.transform.position); // de enemy volgt de player

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }




       // transform.LookAt(new Vector3(playerTarget.transform.position.x, transform.position.y, playerTarget.transform.position.z));
        if (distance <= 2f)
        {
            //  following = false;
            childAnimator.SetBool("Walk", false);
            childAnimator.SetBool("Rest", true);
            agent.enabled = false;

            // slap
            // childAnimator.SetBool("Slap", true);
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
               // playerTarget.GetComponent<MovPlayer>().baseHealth -= 1f;
            }
           
        }
        else
        {
            //  following = true;
            childAnimator.SetBool("Rest", false);
            childAnimator.SetBool("Walk", true);
           // agent.SetDestination(playerTarget.transform.position);
            agent.enabled = true;
        }
    }

    public void FaceTarget()
    {
        Vector3 direction = (playerTarget.transform.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime );
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            health -= 1;
        }
    }

}

