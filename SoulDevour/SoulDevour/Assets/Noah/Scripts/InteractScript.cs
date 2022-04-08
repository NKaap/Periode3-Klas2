using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InteractScript : MonoBehaviour
{
    public bool playerInTrigger;
    public Transform interactableView;

    [Header("Camera Settings")]
    public GameObject mainCamera;
    public GameObject viewCamera;

    [Header("Canvas Settings")]
    public GameObject toSeeCanvas;

    private void Awake()
    {
        toSeeCanvas.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInTrigger == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Pressed E");
                viewCamera.SetActive(true);
                toSeeCanvas.SetActive(false);
                viewCamera.transform.position = interactableView.transform.position;
                viewCamera.transform.rotation = interactableView.transform.rotation;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("entered Trigger");
        if(other.gameObject.tag == "Player")
        {
            toSeeCanvas.SetActive(true);
            playerInTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerInTrigger = false;
        toSeeCanvas.SetActive(false);
        viewCamera.SetActive(false);
    }
    public void Tutorial()
    {
        SceneManager.LoadScene(4);
    }
}
