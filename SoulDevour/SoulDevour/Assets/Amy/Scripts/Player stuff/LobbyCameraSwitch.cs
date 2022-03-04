using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCameraSwitch : MonoBehaviour
{

    public Camera playerCam;
    public Camera characterSelectCam;

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
            canvas.enabled = true;
            Debug.Log("YAssssssssss");
            playerCam.enabled = false;
            characterSelectCam.enabled = true;
        }
        
    }

    public void CharacterExit()
    {
        canvas.enabled = false;
        playerCam.enabled = true;
        characterSelectCam.enabled = false;
    }

}
