using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Data
{
   

    public Scene activeScene;
    public float health;
    public string levelSeed;
    public List<ItemBase> itemsEquipped = new List<ItemBase>();

    
}

public class SavingSystem : MonoBehaviour
{
    public GameObject player;
    public GameObject generator;

    public Data gameSaving = new Data();

    private void Start()
    {
        
    }

    public void LoadButton()
    {
        Load();
    }

    public void SaveButton()
    {
        Save();
    }

    public void Save()
    {
        Scene scene = SceneManager.GetActiveScene();

      

        gameSaving.activeScene = scene;
        gameSaving.health = player.GetComponent<MovPlayer>().calculatedHealth;
        gameSaving.levelSeed = generator.GetComponent<FloorGenerator>().seed;
        gameSaving.itemsEquipped = player.GetComponent<MovPlayer>().items;
      
        string json = JsonUtility.ToJson(gameSaving);
        File.WriteAllText(Application.dataPath + "/saveFile.json", json);

    }

    public void Load()
    {


        string json = File.ReadAllText(Application.dataPath + "/saveFile.json");
        Data loadedData = JsonUtility.FromJson<Data>(json);

        
        // alleen dit nog laten werken ingame ! :) 
        Debug.Log(loadedData.health);
        Debug.Log(loadedData.levelSeed);
        Debug.Log(loadedData.itemsEquipped);
        Debug.Log(loadedData.activeScene);
    }

   

    

   
}
