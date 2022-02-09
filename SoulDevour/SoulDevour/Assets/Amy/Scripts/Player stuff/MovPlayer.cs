using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovPlayer : MonoBehaviour
{

    public Rigidbody playerController;
    public Transform cam;

    public float turnSmoothTime = 0.1f;
    public Vector3 moveVector;
    float verticalVelosity;
    float targetAngle;

    bool done = true;
    bool changed = false;

    public List<ItemBase> items = new List<ItemBase>();

    // public float strength = 5; // player strength
    [SerializeField] private float speed = 10; // player speed
    [SerializeField] private float baseHealth = 4; // player health 
    [SerializeField] private float fruitHeal = 1;

    public float calculatedSpeed => GetSpeed();
    public float calculatedHealth => GetHealth();


    private void Update()
    {
        Move(calculatedSpeed);
        //GetStrength();
        
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


    public float GetHealth()
    {
        float output = baseHealth;

        foreach (ItemBase item in items)
        {
            switch (item.itemtype)
            {
               
                case ItemBase.ItemType.Fruit:
                    {
                        output += fruitHeal;
                        break;
                    }            
            }
        }
        
        return output;
    }


    public float GetSpeed()
    {
        float output = speed;
        foreach (ItemBase item in items)
        {
            switch (item.itemtype)
            {
                case ItemBase.ItemType.Feather:
                    {
                        output += 10f;
                        break;
                    }
            }
        }
        return output;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<ItemBase>(out ItemBase comp))
        {       
            items.Add(new ItemBase(comp));
            collision.gameObject.SetActive(false);
            Debug.Log(items[0].itemtype);
            
        }
    }
}


//public float GetStrength()
//{
//    float output = strength;
//    foreach (ItemBase item in items)
//    {
//        switch (item.itemtype)
//        {
//            case ItemBase.ItemType.BandAid:
//                {
//                    output += 1; // health
//                    // Playermov
//                    break;
//                }
//        }
//    }
//    return output;

//}
