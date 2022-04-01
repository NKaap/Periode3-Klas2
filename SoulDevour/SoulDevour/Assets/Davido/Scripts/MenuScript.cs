using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class MenuScript : MonoBehaviour
{
    public Dropdown dropdown;
    public SceneRenderPipeline[] qualityLevels;
    public AudioMixer audioMixer;


    //change quality
    void Start()
    {
        dropdown.value = QualitySettings.GetQualityLevel();
    }

    

    //load MainMenu
    public void MainMenu()
    {
        SceneManager.LoadScene("Main");
    }


    //quit
    public void QuitGame()
    {
        Application.Quit();
    }


    public void AmyTest()
    {
        SceneManager.LoadScene("Amy's Lobby Test Scene");
    }
  

    //fullscreen
    public void FullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        Debug.Log("eee");
    }

    //Quality changes

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        print(qualityIndex);
    }

    public void ChangeLevel(int value)
    {
        QualitySettings.SetQualityLevel(value);
        print("heehee");
    }

    
    
    
    //volume

    public void SetVolume(float volume)
    {
        print("eee");
        audioMixer.SetFloat("Volume", volume);
    }
    
  

   

  // Game Over Contineu

    public void Con()
    {
        SceneManager.LoadScene("Amy's Lobby Test Scene");
    }

    //Transition


    public Animator transition;

    public float transitionTime = 1f;

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }


    public void ClickButoon()
    {

        LoadNextLevel();
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }



}
