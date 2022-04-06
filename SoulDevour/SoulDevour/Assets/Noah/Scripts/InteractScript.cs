using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        Debug.Log("entered Trigger");
        if(other.gameObject.tag == "Player")
        {
            toSeeCanvas.SetActive(true);
            playerInTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        playerInTrigger = false;
        toSeeCanvas.SetActive(false);
        viewCamera.SetActive(false);
    }
}
