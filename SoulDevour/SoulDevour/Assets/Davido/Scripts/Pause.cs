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
            
            if (GameIsPaused)
            {
                Resumeui();
                

            }
            else
            {

                Pauseui();
                
            }

        }
    }
    public void Resumeui()
    {
        
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
        
    }

    public void Pauseui()
    {

        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }



}
