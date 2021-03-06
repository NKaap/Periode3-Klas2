using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TeacherController : MonoBehaviour
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
        //head.transform.LookAt(player.transform);
        //transform.LookAt(player.transform);

        if (distance >= 3)
        {
            _moveSpeed = 5;
            Vector3 direction = target.position - transform.position;
            direction = direction.normalized;
            Vector3 velocity = direction * _moveSpeed;
            _controller.Move(velocity * Time.deltaTime);
            animations.SetBool("Walk", true);
        }

        if (distance <= 3 || distance >= 10)
        {
            animations.SetBool("Walk", false);
            _moveSpeed = 0;
            Debug.Log("Pause.");
        }

        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            Debug.Log("gooi met kind");
            timeLeft = 2;
        }

        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        TeacherDead();
    }

    public void TeacherDead()
    {
        if (health <= 0)
        {
            animations.SetBool("Dead", true);
        }
    }


}
