using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingSystem : MonoBehaviour
{


    private void Awake()
    {
        SaveFile saveObj = new SaveFile {

            levelSeed = 20,

        };
        string json = JsonUtility.ToJson(saveObj);
        
    
    }

    public class SaveFile
    {
        public int levelSeed;

    }
}
