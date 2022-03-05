using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovPlayer : MonoBehaviour
{
    // ToDo ANIMATIONS ADD EN ROTATION FIX

    public enum PlayerTypes
    {
        TeddyBear, Panda, PinkBear, IceBear, RainbowBear, Shrek
    }

    public PlayerTypes playerTypes;
    public Animator playerAnimator;
    public bool playerisMoving;

    public bool walking;

    public Rigidbody playerController;
    public Transform cam;

    public float turnSmoothTime = 0.1f;
    public Vector3 moveVector;
    float verticalVelosity;
    float targetAngle;

    public List<ItemBase> items = new List<ItemBase>();

    // public float strength = 5; // player strength
    [SerializeField] private float speed = 10; // player speed
    [SerializeField] private float baseHealth = 4; // player health            HEALTH

    [SerializeField] public float calculatedSpeed => GetSpeed(); // gebruik deze om speed aan te roepen.
    [SerializeField] public float calculatedHealth => GetHealth(); // gebruik deze om health mee aan te roepen.
     
    public float kickForce;

    public float radius = 10; // radius voor de kinderen om te schoppen

    private void Update()
    {
        Move(calculatedSpeed);   
    
        KickChildren();
    }

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

    IEnumerator LerpRotation(Quaternion endRotation, float duration)
    {

        float time = 0;


        time += Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, endRotation, time * duration);
        yield return null;
        transform.rotation = endRotation;
        
    }
    #endregion

    #region SkillPoints Player

    public void SkillPoints() // voor skillpoints
    {
        switch (playerTypes)
        {
            case PlayerTypes.TeddyBear:
                {

                    return;
                }
        }
    }

    #endregion

    #region GetHealth and Speed

    public float GetHealth()
    {
        float output = baseHealth;

        foreach (ItemBase item in items)
        {
            switch (item.itemtype)
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
        Collider[] colliders = Physics.OverlapSphere(new Vector3 (transform.position.x, transform.position.y + 4, transform.position.z) , radius);
        foreach (Collider collider in colliders)
        {
            if (collider.transform.CompareTag("Child") && Input.GetButtonDown("Fire2"))
            {
                collider.GetComponent<Rigidbody>().AddExplosionForce(kickForce, transform.position, 10, 10, ForceMode.Impulse);
                Debug.Log("Yass");
            }
        }

    }

    #endregion

    #region Oncollision OnDawGizmos

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<ItemBase>(out ItemBase comp))
        {
            // copy item, do it in array, so it doesnt say "none"
            items.Add(Instantiate(comp));

            //items.Add(new ItemBase(comp));
            collision.gameObject.SetActive(false);
            Debug.Log(items[0].itemtype);
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y + 4, transform.position.z), radius);
    }
    #endregion
}
