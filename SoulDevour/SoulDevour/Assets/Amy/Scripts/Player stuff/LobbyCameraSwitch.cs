using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCameraSwitch : MonoBehaviour
{

    public Camera playerCam;
    public Camera characterSelectCam;

    public GameObject player;
    public Transform playerPos;
   
    public Canvas canvas;

   
    
    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
        playerCam.enabled = true;
        characterSelectCam.enabled = false;
    }

   
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.transform.CompareTag("Player"))
        {
           
            player.SetActive(false); 
           // player.GetComponent<MovPlayer>().enabled = false;
            canvas.enabled = true;
            Debug.Log("YAssssssssss");
            playerCam.enabled = false;
            characterSelectCam.enabled = true;
           
            Cursor.lockState = CursorLockMode.None;
           
        }
        
    }

    public void CharacterExit()
    {
        player.transform.position = playerPos.position;

        player.SetActive(true); // instantiate chosen player. and destroy old one.


       // player.GetComponent<MovPlayer>().enabled = true;
        canvas.enabled = false;
        playerCam.enabled = true;
        characterSelectCam.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

}
