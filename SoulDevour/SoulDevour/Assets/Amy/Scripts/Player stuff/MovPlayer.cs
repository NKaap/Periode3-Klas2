using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovPlayer : MonoBehaviour
{

    public Rigidbody playerController;
    public Transform cam;
    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;
    bool isGrounded;
    Vector3 moveVector;
    float verticalVelosity;
    float targetAngle;

    bool done = true;
    bool changed = false;

    public float speedForMove = 5;


    private void Update()
    {
        Move(speedForMove);
    }



    private void SetRotation()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(hor, 0, ver).normalized;


        if (direction.magnitude >= 0.1f)
        {
            if (targetAngle != (Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y))
            {
                changed = true;
                StopAllCoroutines();
            }
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            if (done || changed)
            {
                changed = false;
                done = false;
                StartCoroutine(LerpRotation(Quaternion.Euler(0f, targetAngle, 0f), 0.25f));
            }

        }
        moveVector = new Vector3(direction.x, verticalVelosity, direction.z);
    }

    private void Move(float speed)
    {

        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(hor, 0, ver).normalized;

        if (direction.magnitude >= 0.1f)
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.position += moveDir.normalized * speed * Time.deltaTime;
        }
        moveVector = new Vector3(direction.x, verticalVelosity, direction.z); 
        StartCoroutine(LerpRotation(Quaternion.Euler(0f, targetAngle, 0f), 0.25f));
        SetRotation();

    }

    private void Gravity()
    {
        transform.position += new Vector3(0, -1f * Time.deltaTime, 0);
    }

    IEnumerator LerpRotation(Quaternion endRotation, float duration)
    {

        float time = 0;
        Quaternion startRotation = transform.rotation;
        while (time < duration)
        {

            transform.rotation = Quaternion.Lerp(startRotation, endRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.rotation = endRotation;
        done = true;
    }
}


