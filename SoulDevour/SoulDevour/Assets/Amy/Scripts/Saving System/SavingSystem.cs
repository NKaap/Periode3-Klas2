using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Data
{
  
    // active scene
    public string activeSceneName;

    

    // level seed
    public string levelSeed;

    // items equipped. WERKT NIET>
    public List<ItemBase> itemsEquipped = new List<ItemBase>();

    // shop room 
    public GameObject shopItemOne;
    public GameObject shopItemTwo;
    public GameObject shopItemThree;

    // item room
    public GameObject itemRoomItem;

    // boss room 
    public GameObject teacherObj;

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

        gameSaving.activeSceneName = scene.name;

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
        
        Debug.Log(loadedData.levelSeed);
        Debug.Log(loadedData.itemsEquipped);

       
        player.GetComponent<MovPlayer>().items = loadedData.itemsEquipped;
        generator.GetComponent<FloorGenerator>().seed = loadedData.levelSeed;
        SceneManager.LoadScene(loadedData.activeSceneName);
        
    }

   

    

   
}
