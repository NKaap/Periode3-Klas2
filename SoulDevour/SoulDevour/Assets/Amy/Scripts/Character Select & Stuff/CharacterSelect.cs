using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterSelect : MonoBehaviour
{
    public GameObject[] characterModels;
    public Transform[] uiCharacterInfo;


    public GameObject activeCharacter;
    public Transform activeInfo;

    public int index;
    public GameObject playerObj;
    public Transform playerPos;

    // Start is called before the first frame update
    void Start()
    {
       
        foreach(GameObject obj in characterModels)
        {
            obj.SetActive(false);
        }

        foreach(Transform text in uiCharacterInfo)
        {
            text.gameObject.SetActive(false);
        }

       
        activeInfo.gameObject.SetActive(false);
        activeInfo = uiCharacterInfo[0];
        activeInfo.gameObject.SetActive(true);

        activeCharacter.SetActive(false);
        activeCharacter = characterModels[0];    
        activeCharacter.SetActive(true);
    }

    public void LeftButton()
    {
        index++;
        Debug.Log(index);
        if (index <= characterModels.Length -1 && index <= uiCharacterInfo.Length -1)
        {
            activeCharacter.SetActive(false);
            activeCharacter = characterModels[index];
            activeCharacter.SetActive(true);

            activeInfo.gameObject.SetActive(false);
            activeInfo = uiCharacterInfo[index];
            activeInfo.gameObject.SetActive(true);
        }
        else
        {
            index = characterModels.Length -1;
        }
       
    }

    public void RightButton()
    {
        index--;
        Debug.Log(index);
        if (index >= 0)
        {
            activeCharacter.SetActive(false);
            activeCharacter = characterModels[index];
            activeCharacter.SetActive(true);


            activeInfo.gameObject.SetActive(false);
            activeInfo = uiCharacterInfo[index];
            activeInfo.gameObject.SetActive(true);
        }
        else
        {
            index = 0;
        }
       
    }

    public void SelectButton()
    {
        // playerObj word de player met scripts etc.

        playerObj = activeCharacter;
        SceneManager.LoadScene(1); // 1 is de game denk ik ?
    }

    public void ExitSelectMode()
    {
        
        
        // cancel character select mode
    }
}
