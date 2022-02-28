using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingSystem : MonoBehaviour
{
   
    private void Start()
    {
      
    }

    private void Awake()
    {
        SaveFile saveObj = new SaveFile {

            levelSeed = 20,



        };
        string json = JsonUtility.ToJson(saveObj);
        Debug.Log(json);

        SaveFile loadedSaveObj =  JsonUtility.FromJson<SaveFile>(json);
        Debug.Log(loadedSaveObj.levelSeed);
    }

    public class SaveFile
    {
        
        public int levelSeed;
       // public string seedObj = levelSeedObj.GetComponent<FloorGenerator>().seed;
    }
}
