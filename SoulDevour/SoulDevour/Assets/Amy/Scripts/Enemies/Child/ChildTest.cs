using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildTest : MonoBehaviour
{
    CharacterController _controller;
    Transform target;
    GameObject player;

    [SerializeField]
    private float _moveSpeed = 5.0f;

    public float health = 4;

    public GameObject ragdoll;
    public GameObject head;
    public Animator animations;

    public float timeLeft = 2;
 

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        target = player.transform;
        _controller = GetComponent<CharacterController>();
      
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        head.transform.LookAt(player.transform);
        transform.LookAt(player.transform);

        if (distance >= 3)
        {
            _moveSpeed = 5;
            Vector3 direction = target.position - transform.position;
            direction = direction.normalized;
            Vector3 velocity = direction * _moveSpeed;
            _controller.Move(velocity * Time.deltaTime);
           
            animations.SetBool("Walk", true);
        }
     
        if(distance <= 3 || distance >= 10)
        {
            animations.SetBool("Walk", false);
           
            _moveSpeed = 0;
            Debug.Log("Pause.");

         

        }

     

        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            animations.SetTrigger("Slap");
            timeLeft = 2;
        }


        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        ChildDead();
    }



    public void ChildDead()
    {
        if (health <= 0)
        {
           Instantiate(ragdoll, transform.position, new Quaternion(0, 0, 0, 0));
           Destroy(gameObject);
           
        }
    }

    public void ChildSlapEvent()
    {
        // eigenlijk iets in Gethealth aanpassen.

        player.GetComponent<MovPlayer>().baseHealth -= 1;
    }


}
