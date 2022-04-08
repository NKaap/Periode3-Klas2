using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBossController : MonoBehaviour
{
    CharacterController _controller;
    Transform target;
    GameObject player;

    SkillPointManager skillManager;
    public GameObject skillObj;
    bool skillpointAdded = false;

    [SerializeField]
    private float _moveSpeed = 5.0f;

    public float health = 20;

    public Rigidbody ragdoll;
    public GameObject ragdollModel;
    public GameObject head;
    public Animator animations;
    public GameObject hand;

    public float timeLeft = 2;
    public bool alive = true;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        target = player.transform;

        _controller = GetComponent<CharacterController>();
        ragdoll = ragdoll.GetComponent<Rigidbody>();

        skillObj = GameObject.FindWithTag("SkillPointManager");
        skillManager = skillObj.GetComponent<SkillPointManager>();

   
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            TeacherMove();
        }

        TeacherDead();
      

        if (!alive)
        {
            if (!skillpointAdded)
            {
                skillManager.AddUnusedSkillPoint();
               
                skillpointAdded = true;
            }
         
        }
    }

    public void TeacherMove()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        transform.LookAt(player.transform);

        if (distance >= 7)
        {
            animations.SetBool("Walk", true);
            _moveSpeed = 3;
            Vector3 direction = target.position - transform.position;
            direction = direction.normalized;
            Vector3 velocity = direction * _moveSpeed;
            _controller.Move(velocity * Time.deltaTime);

            //  animations.SetBool("Walk", true);
        }

        if (distance <= 5 || distance >= 10)
        {
            //  animations.SetBool("Walk", false);
            animations.SetBool("Walk", true) ;
            _moveSpeed = 0;
            //Debug.Log("Pause.");
        }

        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            animations.SetTrigger("ThrowKid");

            timeLeft = 2;
        }

        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
    }

    public void TeacherDead()
    {
        if (health <= 0)
        {
         
            animations.SetBool("Walk", false);
            animations.SetBool("Dead", true);

            StartCoroutine(DeadYes());
            alive = false;
          
        }
    }

    public void ThrowKid()
    {
        Rigidbody instantiatedProjectile = Instantiate(ragdoll, hand.transform.position, hand.transform.rotation) as Rigidbody;
        //Debug.Log(instantiatedProjectile);
        instantiatedProjectile.GetComponentInChildren<Rigidbody>().AddForce(transform.up * 100);

    }

    IEnumerator DeadYes()
    {
        yield return new WaitForSeconds(2);
        animations.SetBool("DeadYes", true);
    }

    public IEnumerator PickupKid()
    {
        GameObject handObj =  Instantiate(ragdollModel, hand.transform.position, ragdollModel.transform.rotation, parent: hand.transform);
        handObj.transform.position = hand.transform.position;
        yield return new WaitForSeconds(1f);
        Destroy(handObj);
    }

}
