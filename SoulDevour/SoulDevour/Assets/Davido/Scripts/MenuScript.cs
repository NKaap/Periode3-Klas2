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
    void Start()
    {
        dropdown.value = QualitySettings.GetQualityLevel();
    }

    //Scene's openen
    public void LoadLevel()
    {
        SceneManager.LoadScene("Amy's Lobby Test Scene");
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

    public void ChangeLevel(int value)
    {
        QualitySettings.SetQualityLevel(value);
        print("heehee");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main");
    }
    
    
    public void SetVolume(float volume)
    {
        print("eee");
        audioMixer.SetFloat("Volume", volume);
    }
    
    
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
