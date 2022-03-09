using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class MovPlayer : MonoBehaviour
{
    public enum PlayerTypes
    {
        TeddyBear, Panda, PinkBear, IceBear, RainbowBear, ClownBear, BlackBear,
    }

    public PlayerTypes playerTypes;
    public SkillPointManager skillManager;

    [Header("Animation Variables")]
    [Space(8)]
    public Animator playerAnimator;
    public bool playerisMoving;
    public bool walking;

    [Header("Player Variables")]
    [Space(8)]
    public Rigidbody playerController;
    public Transform cam;

    [Header("Moving and Rotation")]
    [Space(8)]
    public float turnSmoothTime = 0.1f;
    public Vector3 moveVector;
    float verticalVelosity;
    float targetAngle;

    [Header("Items Equipped")]
    [Space(8)]
    public List<ItemBase.ItemType> items = new List<ItemBase.ItemType>();

    [Header("Base Speed, Jump and Health")]
    [Space(8)]

    [SerializeField] private float speed = 10; // player speed

    [SerializeField] private float baseHealth; // player health            HEALTH
    [SerializeField] private Vector3 baseJumpHeight; // player jump
    public int maxJump = 2;
    public int timesJumped = 0;

    [SerializeField] public float calculatedSpeed => GetSpeed(); // gebruik deze om speed aan te roepen.
    [SerializeField] public float calculatedHealth => GetHealth(); // gebruik deze om health mee aan te roepen.

    [Header("Child Kick Variables")]
    [Space(8)]
    public float kickForce;
    public float radius = 10;


   
    private void FixedUpdate()
    {
         //  even checken want je gebruikt de variable baseHealth;
        Move(calculatedSpeed);
        TakeDamage(calculatedHealth);
        KickChildren();
    }
    private void Update()
    {
        Jump();
    }

    #region Basic Player Functionality

    #region Move % Rotation

    public void Animations()
    {
        if (playerisMoving)
        {
            walking = true;
            playerAnimator.SetBool("Walking", walking);
        }
        else
        {
            walking = false;
            playerAnimator.SetBool("Walking", walking);
        }
    }
    
    private void Move(float speed)
    {

        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(hor, 0, ver).normalized;


     

        if (direction.magnitude >= 0.1f)
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref verticalVelosity, turnSmoothTime);
            Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            transform.position += moveDir.normalized * speed * Time.deltaTime;

            StartCoroutine(LerpRotation(Quaternion.Euler(1f, angle, 1f), 5));
        }   
        moveVector = new Vector3(direction.x, verticalVelosity, direction.z);

       
    }
    public void Jump()
    {
        if (Input.GetButtonDown("Jump") && timesJumped < maxJump)
        {

            timesJumped++;

            GetComponent<Rigidbody>().velocity = baseJumpHeight;

        }
    }
    IEnumerator LerpRotation(Quaternion endRotation, float duration)
    {
        float time = 0;
        time += Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, endRotation, time * duration);
        yield return null;
        transform.rotation = endRotation;
        
    }
    #endregion


    public void TakeDamage(float health)
    {
        health -= 10;
        //Debug.Log(health);
    }

    #region GetHealth and Speed

    public float GetHealth() // hiervoor een functie zodat health ook echt aangepast word!
    {
        float output = baseHealth;

        foreach (ItemBase.ItemType item in items)
        {
            switch (item)
            {
               
                case ItemBase.ItemType.Fruit:
                    {
                        output += 1;
                        break;
                    }
                case ItemBase.ItemType.TextBook:
                    {
                        output += 1;
                        break;
                    }
                case ItemBase.ItemType.Soup:
                    {
                        output += 1;
                        break;
                    }
                case ItemBase.ItemType.BandAid:
                    {
                        output += 2;
                        break;
                    }
                case ItemBase.ItemType.Ressurect: // werkt dit?
                    {
                        if(output == 0)
                        {
                            Instantiate(gameObject, transform.position, transform.rotation);
                            output = 1;
                        }
                        break;
                    }
            }
        }
        
        return output;
    }


    public float GetSpeed() // deze word aangepast in de update met calculated speed!
    {
        float output = speed;
        foreach (ItemBase.ItemType item in items)
        {
            switch (item)
            {
                case ItemBase.ItemType.Feather:
                    {
                        output += 10f;
                        break;
                    }
                case ItemBase.ItemType.Socks:
                    {
                        output += 5f;
                        break;
                    }
            }
        }
        return output;

    }

    #endregion

    #region KickChild

    public void KickChildren()
    {
        Collider[] colliders = Physics.OverlapSphere(new Vector3 (transform.position.x, transform.position.y, transform.position.z) , radius);
        foreach (Collider collider in colliders)
        {
            if (collider.transform.CompareTag("Child") && Input.GetButtonDown("Fire2"))
            {
                //playerAnimator.SetBool(Kick, true);
                collider.GetComponentInChildren<Rigidbody>().AddExplosionForce(kickForce, transform.position, 10, 10, ForceMode.Impulse);
                Debug.Log("Yass");
            }
            else
            {
                //playerAnimator.SetBool(Kick, false);
            }
        }

    }

    #endregion

    #region Oncollision OnDawGizmos

    private void OnCollisionEnter(Collision collision)
    {
        timesJumped = 0;

        if (collision.gameObject.TryGetComponent<ItemBase>(out ItemBase comp))
        {
            // copy item, do it in array, so it doesnt say "none"
            if (!items.Contains(comp.itemtype))
                items.Add(comp.itemtype);

            //items.Add(new ItemBase(comp));
            collision.gameObject.SetActive(false);
            Debug.Log(items[0]);
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y + 4, transform.position.z), radius);
    }
    #endregion

    #endregion
}
