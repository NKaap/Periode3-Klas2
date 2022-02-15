using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
	public GameObject gameOverScreen;

    // werkende met UI 

    public float countdown = 5; // 8 minuten in seconde 480

    private void Start()
    {
		gameOverScreen.SetActive(false);
    }

    void Update()
	{
		// countdown ook nog op display on screen 
		countdown -= Time.deltaTime;
		//Debug.Log(countdown);

		if (countdown <= 0)
		{
			countdown = 0;
			Debug.Log("Game over, didnt make it in time");
			gameOverScreen.SetActive(true);
			//if timer 0 do this
		}
	}

}
