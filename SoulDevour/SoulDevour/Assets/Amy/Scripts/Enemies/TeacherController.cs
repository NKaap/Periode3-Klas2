using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;


public class TeacherController : MonoBehaviour
{
    public bool playerInRoom;
    public GameObject playerTarget;
    

    public float health = 2;
    public GameObject teacherHead;

    public Animator teacherAnim;
    public bool isMoving;
    public Rigidbody rb;

    public GameObject throwKids;
    public Transform rightHand;

    public NavMeshAgent agent;
    public float detectionRange = 10f;

    public float dist;
    public bool following = false;

    public float timeLeft = 30;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTarget = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        float distance = Vector3.Distance(playerTarget.transform.position, transform.position);
        teacherHead.transform.LookAt(playerTarget.transform);

        if(playerTarget == null)
        {
            playerTarget = GameObject.FindGameObjectWithTag("Player");
        }


        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            GameObject kids = Instantiate(throwKids, rightHand.position, new Quaternion(0, 0, 0, 0));
            timeLeft = 30;
            kids.GetComponent<Rigidbody>().AddForce(Vector3.forward * 5, ForceMode.Force);
            timeLeft = 30;
        }
      

        if (health <= 0)
        {
           
            Destroy(gameObject);
            // death animation
            // portal to next scene show.
           // SceneManager.LoadScene("LevelTwo"); // for each level a load scene, or different script. 
        }

        if (distance <= detectionRange)
        {
            agent.SetDestination(playerTarget.transform.position); // de enemy volgt de player

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }

        if (distance <= 3f)
        {
            agent.enabled = false;
            // throw kids each 2 sec. w animation, under hand
        }
        else
        {
            agent.SetDestination(playerTarget.transform.position);
            agent.enabled = true;
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
