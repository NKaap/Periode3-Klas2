using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class BossLevelFinished : MonoBehaviour
{

    int index;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            // save alle info over de player. - skillpoints 
            SaveToNextScene();
        }
    }


    public void SaveToNextScene()
    {
        Data gameSaveEnd = new Data();
        gameSaveEnd.type = (MovPlayer.PlayerTypes)index;

        string jsonChar = JsonUtility.ToJson(gameSaveEnd);
        File.WriteAllText(Application.dataPath + "/endSaveFile.json", jsonChar);

        Data gameSaving = new Data();
        string json = JsonUtility.ToJson(gameSaving);
        File.WriteAllText(Application.dataPath + "/saveFile.json", json);

        //   string json = File.ReadAllText(Application.dataPath + "/saveFile.json");
        ////   Data loadedData = JsonUtility.FromJson<Data>(json);
        //   json = JsonUtility.ToJson(new Data());
        //   File.WriteAllText(Application.dataPath + "/saveFile.json", json);

        SceneManager.LoadScene(1);
         

    }
}
