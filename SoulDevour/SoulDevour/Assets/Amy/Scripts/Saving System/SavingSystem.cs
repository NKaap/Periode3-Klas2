using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Data
{
    // character select scene switch 
    public MovPlayer.PlayerTypes type;


    // active scene
    public string activeSceneName;

    // level seed
    public int levelSeed;

    // items equipped. WERKT NIET>
    public List<ItemBase.ItemType> itemsEquipped = new List<ItemBase.ItemType>();

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
        string json = File.ReadAllText(Application.dataPath + "/saveFile.json");
        Data loadedData = JsonUtility.FromJson<Data>(json);

        Debug.Log(loadedData.itemsEquipped.Count);

        Random.InitState(loadedData.levelSeed);
        player.GetComponent<MovPlayer>().items = loadedData.itemsEquipped;
    }

    public void LoadButton()
    {
        Load();
    }

    public void SaveButton()
    {
        Save();
    }

    public void ResetButton()
    {
        ResetSave();
    }

    public void Save()
    {
        Scene scene = SceneManager.GetActiveScene();

        gameSaving.activeSceneName = scene.name;

        gameSaving.levelSeed = Random.seed;

        gameSaving.itemsEquipped = player.GetComponent<MovPlayer>().items;
      
        string json = JsonUtility.ToJson(gameSaving);
        File.WriteAllText(Application.dataPath + "/saveFile.json", json);
        Debug.Log(gameSaving.itemsEquipped.Count);
    }

    public void Load()
    {
        string json = File.ReadAllText(Application.dataPath + "/saveFile.json");
        Data loadedData = JsonUtility.FromJson<Data>(json);
        SceneManager.LoadScene(loadedData.activeSceneName);

    }

    public void ResetSave()
    {
        string json = File.ReadAllText(Application.dataPath + "/saveFile.json");
        Data loadedData = JsonUtility.FromJson<Data>(json);
        File.WriteAllText(Application.dataPath + "/saveFile.json", "");
        SceneManager.LoadScene(loadedData.activeSceneName);
    }




}
