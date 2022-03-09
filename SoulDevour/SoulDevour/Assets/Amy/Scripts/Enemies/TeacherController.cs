using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherController : MonoBehaviour
{

    //public enum TeacherTypes
    //{
    //    Frans, Mike, Tim, Rhonja, Normal      voor als ze allemaal verschillende functionality hebben, of animations switch case
    //}

    //public TeacherTypes teacherTyp;
    public float range;
    public GameObject playerTarget;
    public bool teacherMove = true;
    public GameObject teacherHead;
    public float health = 20;
    public float speed;
    public float timeLeft = 5; // time left for throwing kid
    public Transform hand;
    public float throwChildSpeed;
    public Animator teacherAnimator;

    public Rigidbody[] kidVariantsForThrow;
    // Update is called once per frame

    void Update()
    {

        TeacherAnimations();
        ChildDead();

        speed = 4 * Time.deltaTime;
        teacherMove = Vector3.Distance(transform.position, playerTarget.transform.position) > 6;
        teacherHead.transform.LookAt(playerTarget.transform);
        gameObject.transform.LookAt(playerTarget.transform);



        if (teacherMove)
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, playerTarget.transform.position, speed);
        }

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            timeLeft = 1.5f;
            // GameObject pencil = Instantiate(pencilForThrow, shootPos.position, Quaternion.identity);
            //pencil.GetComponent<Rigidbody>().AddForce(transform.forward * force);

            Rigidbody kid = Instantiate(kidVariantsForThrow[Random.Range(0,kidVariantsForThrow.Length)], hand.transform.position, hand.transform.rotation) as Rigidbody;
            kid.velocity = hand.transform.TransformDirection(new Vector3(0, 0, throwChildSpeed));
        }
    }

    public void TeacherAnimations()
    {
        // swing in scene als player in de kamer komt
     // if moving, walk
     // if throwing, throw
     // if dead, dead
     // als radio played, pain
     // if player is dead, twerk
    }

    public void ChildDead()
    {
        if (health <= 0)
        {
           // Instantiate(item, transform.position, new Quaternion(0, 0, 0, 0));  instantiate een item!
            Destroy(gameObject);
            //teacherAnimator.SetBool(Dead, true);


        }
    }
}
