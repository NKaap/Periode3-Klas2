using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	public Text timeShow;

    // werkende met UI 

    public float countdown = 0; // 8 minuten in seconde 480



    void Update()
	{
		// countdown ook nog op display on screen 
		countdown += Time.deltaTime;
		//Debug.Log(countdown);

		timeShow.text = countdown.ToString();

		if (countdown == 80)
		{
			countdown = 80;
			
			Debug.Log("Reached Zero");
			
			//if timer 0 do this
		}
	}

}
