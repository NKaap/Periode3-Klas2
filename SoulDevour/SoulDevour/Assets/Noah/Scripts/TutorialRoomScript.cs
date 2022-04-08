using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialRoomScript : MonoBehaviour
{
    public GameObject[] popUps;
    public string[] popUpsString;
    public int popUpIndex;

    public GameObject skipButton;

    public GameObject textObject;

    public Text popUpText;

    public GameObject fightRoom;

    public GameObject bossRoom2;


    public bool isItemOpgapkt;
    public bool shopItemOpgepakt;

    [Header("Room Bool")]
    public bool firstClass;
    public bool fightClass;
    public bool itemRoom;
    public bool shopRoom;
    public bool secondClass;
    public bool bossRoom;

    [Header("Child In scene Check")]
    public int levendeKinder;
    public bool isTeacherDood;

    [Header("Item")]
    public bool itemOpgepakt;

    // Start is called before the first frame update

    private void Awake()
    {
        this.gameObject.GetComponent<TextEffect>().inputText = popUpsString[0];
    }
    void Start()
    {
        Debug.Log(GameObject.FindGameObjectsWithTag("Player"));
    }

    // Update is called once per frame
    void Update()
    {
        if (popUpText.text != popUpsString[popUpIndex])
        {
            this.gameObject.GetComponent<TextEffect>().inputText = popUpsString[popUpIndex];
        }

        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }
        if (popUpIndex == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 2)
        {
            if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 3)
        {
            if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 4)
        {
            if (firstClass == true)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 5)
        {
            if (popUpsString[6] == this.gameObject.GetComponent<TextEffect>().whereTextInputs.text || Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 6)
        {
            if (fightClass == true)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 7)
        {
            if (fightRoom.GetComponent<EnterRoom>().allEnemiesDead == true)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 8)
        {
            if (itemRoom == true)
            {
                popUpIndex++;
            }
        }
        else if(popUpIndex == 9)
        {
            if(popUpsString[10] == this.gameObject.GetComponent<TextEffect>().whereTextInputs.text || Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 10)
        {
            if (isItemOpgapkt == true)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 11)
        {
            if (shopRoom == true)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 12)
        {
            if (popUpsString[13] == this.gameObject.GetComponent<TextEffect>().whereTextInputs.text || Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 13)
        {
            if (popUpsString[14] == this.gameObject.GetComponent<TextEffect>().whereTextInputs.text || Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 14)
        {
            if (bossRoom == true)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 15)
        {
            if (popUpsString[15] == this.gameObject.GetComponent<TextEffect>().whereTextInputs.text || Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 16)
        {
            if (bossRoom2.GetComponent<BossRoom>().teacherDead == true)
            {
                SkipTutorial();
            }
        }
        else if (popUpIndex == popUps.Length)
        {
            SkipTutorial();
        }
    }

    public void SkipTutorial()
    {
        //popUpIndex = popUps.Length;
        //skipButton.SetActive(false);
        //textObject.SetActive(false);
        SceneManager.LoadScene("Lobby1");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == ("Item"))
        {
            isItemOpgapkt = true;
        }
        if(collision.gameObject.tag == ("ShopItem"))
        {
            shopItemOpgepakt = true;
        }
    }

}
