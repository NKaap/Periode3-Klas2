using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [Header("Text Settings")]
    public string[] popUps;
    public int popUpIndex;

    [Header("UI settings")]
    public GameObject skipButton;

    public GameObject tutorialCamera;

    public GameObject tekstShit;

    [Header("Camera Settings")]
    public int cameraSpeedInRoom;

    [Header("Door Settings")]
    public GameObject doorObject;
    public bool doorIsOpen;
    public Animator doorAnim;

    [Header("Door Sounds")]
    public AudioSource doorOpenSound;
    public AudioSource doorCloseSound;

    private void Awake()
    {
        this.gameObject.GetComponent<TextEffect>().inputText = popUps[0];
    }

    // OnEnable is Called when object is Enabled
    public void OnEnable()
    {
        this.gameObject.GetComponent<TextEffect>().inputText = popUps[0];
        tutorialCamera.SetActive(true);
        tutorialCamera.GetComponent<CameraController>().currentView = tutorialCamera.GetComponent<CameraController>().startingView;
        tutorialCamera.GetComponent<CameraController>().transitionSpeed = 0.5f;
        skipButton.SetActive(true);
        tekstShit.SetActive(true);
        popUpIndex = 0;
    }

    public void Start()
    {
        tutorialCamera.SetActive(true);
        popUpIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (popUpIndex >= popUps.Length)
        {
            Debug.Log("Out of Array");
            return;
        }


        if (Input.GetMouseButtonDown(0))
        {

            this.gameObject.GetComponent<TextEffect>().inputText = popUps[popUpIndex + 1];

        }
        /*for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }*/
        if (popUpIndex == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ToggleDoor();
                popUpIndex++;
                tutorialCamera.GetComponent<CameraController>().currentView = tutorialCamera.GetComponent<CameraController>().roomView;
            }
        }
        else if (popUpIndex == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ToggleDoor();
                popUpIndex++;
                tutorialCamera.GetComponent<CameraController>().transitionSpeed = cameraSpeedInRoom;
                tutorialCamera.GetComponent<CameraController>().currentView = tutorialCamera.GetComponent<CameraController>().achievementView;
            }
        }
        else if (popUpIndex == 2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
                tutorialCamera.GetComponent<CameraController>().currentView = tutorialCamera.GetComponent<CameraController>().whiteboardView;
            }
        }
        else if (popUpIndex == 3)
        {
            if (Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
                tutorialCamera.GetComponent<CameraController>().currentView = tutorialCamera.GetComponent<CameraController>().bearView;
            }
        }
        else if (popUpIndex == 4)
        {
            if (Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
                tutorialCamera.GetComponent<CameraController>().currentView = tutorialCamera.GetComponent<CameraController>().roomView;

            }
        }

        else if (popUpIndex + 1 == popUps.Length)
        {
            SkipTutorial();
        }
    }
    public void popUpPlusPlus()
    {
        popUpIndex++;
    }

    public void SkipTutorial()
    {
        popUpIndex = popUps.Length;
        tutorialCamera.GetComponent<CameraController>().currentView = tutorialCamera.GetComponent<CameraController>().roomView;
        tutorialCamera.SetActive(false);
        skipButton.SetActive(false);
        tekstShit.SetActive(false);
        if (doorIsOpen == true)
        {
            doorAnim.Play("CloseDoor");
        }
        this.gameObject.SetActive(false);
    }
    public void ToggleDoor()
    {
        doorIsOpen = !doorIsOpen;
        if (doorIsOpen == false)
        {
            doorAnim.Play("CloseDoor");
            doorCloseSound.Play();
        }
        if (doorIsOpen == true)
        {
            doorAnim.Play("OpenDoor");
            doorOpenSound.Play();
        }
    }

}
