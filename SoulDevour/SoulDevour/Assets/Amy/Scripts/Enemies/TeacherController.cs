using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TeacherController : MonoBehaviour
{
  
    public float range;
    [SerializeField] private GameObject playerTarget;
    public bool teacherMove = true;
   
    public float health = 10;
    public float speed;

    public float timeLeft = 5; // time left for throwing kid

    public Transform hand;
    public float throwChildSpeed = 10;
   // public Animator teacherAnimator;

    public GameObject kidVariantsForThrow;


    void Update()
    {

        TeacherDead();
        speed = 4 * Time.deltaTime;
        teacherMove = Vector3.Distance(transform.position, playerTarget.transform.position) > 6;    
        gameObject.transform.LookAt(playerTarget.transform);

        playerTarget = GameObject.FindGameObjectWithTag("Player");


        if (teacherMove)
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, playerTarget.transform.position, speed);
        }

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            GameObject kid = Instantiate(kidVariantsForThrow, hand.transform.position, hand.transform.rotation);
            kid.GetComponent<Rigidbody>().velocity = hand.transform.TransformDirection(new Vector3(0, 0, throwChildSpeed));

            timeLeft = 1.5f;
        }
    }

    public void TeacherDead()
    {
        if (health <= 0)
        {
           // Instantiate(item, transform.position, new Quaternion(0, 0, 0, 0));  instantiate een item!
            Destroy(gameObject);
            SceneManager.LoadScene(4);
            //teacherAnimator.SetBool(Dead, true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            health -= 0.5f;
        }
       
    }
}
