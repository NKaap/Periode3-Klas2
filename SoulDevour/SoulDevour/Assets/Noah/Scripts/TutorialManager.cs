using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public string[] popUps;
    public int popUpIndex;

    public GameObject skipButton;

    public GameObject tutorialCamera;

    public GameObject tekstShit;

    [Header("Door Settings")]
    public GameObject doorObject;
    public float openSpeed;
    public bool doorIsOpen;
    private float angle;
    public Vector3 direction;
    public Animator doorAnim;

    private void Awake()
    {
        this.gameObject.GetComponent<TextEffect>().inputText = popUps[0];
    }

    // Update is called once per frame
    void Update()
    {
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
                //tutorialCamera.GetComponent<CameraController>().transitionSpeed = 5;
            }
        }
        else if (popUpIndex == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ToggleDoor();
                popUpIndex++;
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
        this.gameObject.SetActive(false);
    }

    public void ToggleDoor()
    {
        doorIsOpen = !doorIsOpen;
        if (doorIsOpen == false) // Move rotation to closed
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,-100,0) , openSpeed);
            doorAnim.Play("CloseDoor");
        if (doorIsOpen == true) // Move rotation to open
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,0,0), openSpeed);
            doorAnim.Play("OpenDoor");
    }

}
