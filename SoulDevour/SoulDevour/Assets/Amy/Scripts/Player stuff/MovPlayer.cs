using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MovPlayer : MonoBehaviour
{
    public enum PlayerTypes
    {
        TeddyBear = 0, Panda = 1, PinkBear = 2, IceBear = 3, RainbowBear = 4, ClownBear = 5, BlackBear = 6,
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

    public GameObject playerObject;
    public SkinnedMeshRenderer gameObjectMesh;
    public GameObject secondObj;
    public Material gameObjectMaterial;
    public GameObject[] playerModel;
    public Material[] playerMaterials;
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

    public float baseHealth = 4; // player health            HEALTH
    [SerializeField] private Vector3 baseJumpHeight; // player jump
    public int maxJump = 2;
    public int timesJumped = 0;

    [SerializeField] public float calculatedSpeed => GetSpeed(); // gebruik deze om speed aan te roepen.
    [SerializeField] public float calculatedHealth => GetHealth(); // gebruik deze om health mee aan te roepen.

    [Header("Child Kick Variables")]
    [Space(8)]
    public float kickForce;
    public float radius = 10;

    

    //public void PlayerModel()
    //{
    //    switch (((int)playerTypes))
    //    {
    //        case 0:
    //            {
    //                secondObj.GetComponent<SkinnedMeshRenderer>().material = playerMaterials[0];
    //                break;
    //            }
    //        case 1:
    //            {
    //                secondObj.GetComponent<SkinnedMeshRenderer>().material = playerMaterials[1];
    //                break;
    //            }
    //        case 2:
    //            {
    //                secondObj.GetComponent<SkinnedMeshRenderer>().material = playerMaterials[2];
    //                break;
    //            }
    //        case 3:
    //            {
    //                secondObj.GetComponent<SkinnedMeshRenderer>().material = playerMaterials[3];                 
    //                break;
    //            }
    //        case 4:
    //            {

    //                secondObj.GetComponent<SkinnedMeshRenderer>().material = playerMaterials[4];
    //                break;
    //            }
    //        case 5:
    //            {
    //                secondObj.GetComponent<SkinnedMeshRenderer>().material = playerMaterials[5];
    //                break;
    //            }
    //        case 6:
    //            {
    //                secondObj.GetComponent<SkinnedMeshRenderer>().material = playerMaterials[6];
    //                break;
    //            }
    //    }
    //}

    
    private void FixedUpdate()
    {
         //  even checken want je gebruikt de variable baseHealth;
        
    }
    private void Update()
    {
        Move(calculatedSpeed);

        KickChildren();

        // PlayerModel();
        Jump();
        //playerAnimator.SetBool("Walking", true);

        if(baseHealth <= 0)
        {
            
            SceneManager.LoadScene("GameOver");
        }
      
    }

    #region Basic Player Functionality

    #region Move % Rotation

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
            playerisMoving = true;
            StartCoroutine(LerpRotation(Quaternion.Euler(1f, angle, 1f), 5));

            playerAnimator.SetBool("Rest", false);
            playerAnimator.SetBool("Walking", true);
        }
        else
        {
            playerisMoving = false;
        
            playerAnimator.SetBool("Walking", false);
            playerAnimator.SetBool("Rest", true);
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
                        output += 5f;
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
                playerAnimator.SetBool("Kick", true);
                collider.GetComponentInChildren<Rigidbody>().AddExplosionForce(kickForce, transform.position, 10, 10, ForceMode.Impulse);
                Debug.Log("Yass");
            }
            else
            {
                playerAnimator.SetBool("Kick", false);
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

        if (collision.transform.CompareTag("BulletChild"))
        {
            baseHealth -= 2;
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
