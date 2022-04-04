using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject settings;
     
    void Start()
    {
       pauseMenu.SetActive(false);
       Time.timeScale = 1;
       GameIsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            if (GameIsPaused)
            {
                Resumeui();
                

            }
            else
            {

                Pauseui();
                settings.SetActive(false);
            }

        }
    }
    public void Resumeui()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
        settings.SetActive(false);
    }

    public void Pauseui()
    {

        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;

    }



}
