using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class CharacterSelect : MonoBehaviour
{
    [Header("Character Information")]
    [Space(8)]
    public GameObject[] characterModels;
    public Transform[] uiCharacterInfo;


    [Header("Active Information")]
    [Space(8)]
    public GameObject activeCharacter;
    public Transform activeInfo;

    [Header("Other")]
    [Space(8)]
    public int index;
    public GameObject playerObj;
    public Transform playerPos;
    public SkillPointManager skillPointManager;

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

    #region Buttons

    public void LeftButton()
    {
        index++;
        Debug.Log(index);
        if (index <= characterModels.Length -1 && index <= uiCharacterInfo.Length )
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
            index = characterModels.Length ;
        }
        //Debug.Log((MovPlayer.PlayerTypes)index);
        skillPointManager.SetSkillData((MovPlayer.PlayerTypes)index);
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
        // Debug.Log((MovPlayer.PlayerTypes)index);
        skillPointManager.SetSkillData((MovPlayer.PlayerTypes)index);
    }

    public void SelectButton()
    {
       
        Data gameSavingChar = new Data();
        gameSavingChar.type = (MovPlayer.PlayerTypes)index;
        string jsonChar = JsonUtility.ToJson(gameSavingChar);
        File.WriteAllText(Application.dataPath + "/characterSaveFile.json", jsonChar);

     //   string json = File.ReadAllText(Application.dataPath + "/saveFile.json");
     ////   Data loadedData = JsonUtility.FromJson<Data>(json);
     //   json = JsonUtility.ToJson(new Data());
     //   File.WriteAllText(Application.dataPath + "/saveFile.json", json);
      

        SceneManager.LoadScene(1); // 1 is de game denk ik ?

    }

    #endregion
}
