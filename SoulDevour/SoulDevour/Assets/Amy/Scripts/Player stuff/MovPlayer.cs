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

    [SerializeField] public float damage = 1;
    [SerializeField] public float speed = 5; // player speed
    [SerializeField] public float baseHealth = 4; // player health            HEALTH
    [SerializeField] public float baseJumpHeight = 2; // player jump
    public int maxJump = 2;
    public int timesJumped = 0;
    

    [SerializeField] public float calculatedSpeed => GetSpeed(); // gebruik deze om speed aan te roepen.
    [SerializeField] public float calculatedHealth => GetHealth(); // gebruik deze om health mee aan te roepen.
    [SerializeField] public float calculatedDamage => GetDamage();
    [SerializeField] public float calculatedJumpHeight => GetJumpHeight();

    [Header("Child Kick Variables")]
    [Space(8)]
    public float kickForce;
    public float radius = 10;

    [Header("Money UI")]
    [Space(8)]
    public Text moneyUI;
    public int money;
    public Text healthUI;

    public bool cantMove;

    

    public void PlayerModel()
    {
        switch (((int)playerTypes))
        {
            case 0:
                {
                    secondObj.GetComponent<SkinnedMeshRenderer>().material = playerMaterials[0];
                    break;
                }
            case 1:
                {
                    secondObj.GetComponent<SkinnedMeshRenderer>().material = playerMaterials[1];
                    break;
                }
            case 2:
                {
                    secondObj.GetComponent<SkinnedMeshRenderer>().material = playerMaterials[2];
                    break;
                }
            case 3:
                {
                    secondObj.GetComponent<SkinnedMeshRenderer>().material = playerMaterials[3];
                    break;
                }
            case 4:
                {

                    secondObj.GetComponent<SkinnedMeshRenderer>().material = playerMaterials[4];
                    break;
                }
            case 5:
                {
                    secondObj.GetComponent<SkinnedMeshRenderer>().material = playerMaterials[5];
                    break;
                }
            case 6:
                {
                    secondObj.GetComponent<SkinnedMeshRenderer>().material = playerMaterials[6];
                    break;
                }
        }
    }


    private void FixedUpdate()
    {
        //  even checken want je gebruikt de variable baseHealth;     
        if (!cantMove)
        {
            Move(calculatedSpeed);
        }
    }

    
    
    private void Update()
    {

        KickChildren();
        PlayerModel();
        Jump();

        moneyUI.text = (money + "Soul");
        if (money == 1)
        {
            moneyUI.text = (money + ("Soul"));
        }
        else
        {
            moneyUI.text = (money + ("Souls"));
        }

        healthUI.text = (calculatedHealth + "Life Energy");


        if (calculatedHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        //Debug.Log(skillManager.GetAllocatedPointsOf(0));
        //Debug.Log(skillManager.GetAllocatedPointsOf(1));
        //Debug.Log(skillManager.GetAllocatedPointsOf(2));
        //Debug.Log(skillManager.GetAllocatedPointsOf(3));

        Debug.Log(money + ".. Stonks");
      
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
            StartCoroutine(LerpRotation(Quaternion.Euler(1f, angle, 1f), 40));

            playerAnimator.SetBool("Idle", false);
            playerAnimator.SetBool("Walking", true);
        }
        if(direction.magnitude <= 0.1f)
        {
            playerAnimator.SetBool("Walking", false);
            playerAnimator.SetBool("Idle", true);
           
        }
        moveVector = new Vector3(direction.x, verticalVelosity, direction.z);
    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump") && timesJumped < maxJump)
        {

            timesJumped++;

            GetComponent<Rigidbody>().velocity = new Vector3 (0, calculatedJumpHeight,0);

        }
    }

    IEnumerator LerpRotation(Quaternion endRotation, float duration)
    {
        float time = 0;
        time += Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, endRotation, time * duration);
        yield return endRotation;
    }
    #endregion

    #region GetHealth GetSpeed GetDamage GetJumpHeight

    public float GetHealth() // hiervoor een functie zodat health ook echt aangepast word!
    {
        float output = baseHealth;

        output += skillManager.GetAllocatedPointsOf(2); // health


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

        output += skillManager.GetAllocatedPointsOf(0); // 0 == speed

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

    public float GetDamage()
    {
        float output = damage;

        output += skillManager.GetAllocatedPointsOf(3); // damage

        output += 1;

        return output;
    }

    public float GetJumpHeight()
    {
        float output = baseJumpHeight;

        output += skillManager.GetAllocatedPointsOf(1); // jump height

        return output;
    }

    #endregion

    #region Kick and Slap Child

    public void KickChildren()
    {

        if (Input.GetButtonDown("Fire2"))
        {
            playerAnimator.SetBool("Walking", false);
           
            playerAnimator.SetTrigger("Kick");
        }

        if (Input.GetButtonDown("SlapChild")) //
        {
            playerAnimator.SetBool("Walking", false);

            playerAnimator.SetTrigger("Slap");
        }
    }

    public void MidSlap()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            // playerAnimator.SetBool("UpperCut", true);
            cantMove = true;
            if (collider.transform.CompareTag("Child"))
            {
                collider.GetComponentInChildren<Rigidbody>().AddExplosionForce(kickForce, transform.position, 10, 10, ForceMode.Impulse);
                
            }
            if (collider.transform.CompareTag("LivingChild"))
            {
                collider.GetComponent<ChildTest>().health -= calculatedDamage;

                if (collider.GetComponent<ChildTest>().health <= 0)
                {
                    money += 1;
                }
            }
            // collider.GetComponent<ChildController>().health -= 10;
            Debug.Log("Yass");

        }
    }

    public void KickForceKids()
    {
        Debug.Log("Yass");
        cantMove = true;
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            if (collider.transform.CompareTag("Child"))
            {
                collider.GetComponentInChildren<Rigidbody>().AddExplosionForce(kickForce, transform.position, 10, 10, ForceMode.Impulse);
              
            }
            if (collider.transform.CompareTag("LivingChild"))
            {
                collider.GetComponent<ChildTest>().health -= calculatedDamage;

                if(collider.GetComponent<ChildTest>().health <= 0)
                {
                    money += 1;
                }
            }
        }
    }

    public void EndKick()
    {
        cantMove = false;
    }

    public void StopSlap()
    {
        cantMove = false;
    }

    public void Walk()
    {
        Debug.Log("pp");
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
            Debug.Log("Got hit by a child");
            baseHealth -= 1;
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
