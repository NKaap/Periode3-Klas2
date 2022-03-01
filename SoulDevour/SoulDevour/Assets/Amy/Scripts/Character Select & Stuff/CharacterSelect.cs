using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] characterModels;
    public GameObject activeCharacter;
    public int index;
    public GameObject playerObj;

    // Start is called before the first frame update
    void Start()
    {
       
        foreach(GameObject obj in characterModels)
        {
            obj.SetActive(false);
        }

        activeCharacter.SetActive(false);
        activeCharacter = characterModels[0];
        activeCharacter.SetActive(true);
    }

    public void LeftButton()
    {
        index++;
        Debug.Log(index);
        if (index <= characterModels.Length -1)
        {
            activeCharacter.SetActive(false);
            activeCharacter = characterModels[index];
            activeCharacter.SetActive(true);
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
        }
        else
        {
            index = 0;
        }
       
    }

    public void SelectButton()
    {
        playerObj = activeCharacter;
    }

    public void ExitSelectMode()
    {

    }
}
