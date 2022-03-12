using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    void Start()
    {
       
    }

    //Scene's openen
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void FullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        Debug.Log("eee");
    }


    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        print(qualityIndex);
    }



}
