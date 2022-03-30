using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMov : MonoBehaviour
{
    public bool canjump = true;
    public float h;
    public float v;
    public Vector3 move;
    public float moveSpeed;
    public Vector3 jump;
    public Vector3 mouserotation;
    public int maxJump;
    public int timesJumped;

    void Update()
    {

        if (Input.GetButtonDown("Jump"))
        {
            if (timesJumped < maxJump)
            {
                timesJumped++;
                GetComponent<Rigidbody>().velocity = jump;
            }
            print("jumping");
        }
        h = Input.GetAxis("Horizontal");                                             // -1 0 1
        v = Input.GetAxis("Vertical");
        move.x = h;
        move.z = v;
        GetComponent<Transform>().Translate(move * Time.deltaTime * moveSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        timesJumped = 0;
    }
}
