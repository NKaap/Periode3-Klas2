using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
	[Header("Text Time Settings")]
	public float delay = 0.1f;
	public float textDelay;
	[Header("Input Settings")]
	public string inputText;
	public Text whereTextInputs;
	public string currentText = "";

	[Header("Check If Completed")]
	public bool isTextCompleted;

	// Use this for initialization
	void Start()
	{
		StartCoroutine(WaitForSecondsShowText());

	}

    private void FixedUpdate()
    {
        if(isTextCompleted == true)
        {
			if(whereTextInputs.text != inputText)
            {
				isTextCompleted = false;
				Debug.Log("Text Changed!");
				Start();
            }
            else
            {
				return;
            }
        }
    }

    public void CheckIfDone()
	{
		if (whereTextInputs.text == inputText && isTextCompleted == false)
		{
			isTextCompleted = true;
			Debug.Log("Text Completed");
		}
	}

	IEnumerator ShowText()
	{
		for (int i = 0; i < inputText.Length + 1; i++)
		{
			currentText = inputText.Substring(0, i);
			whereTextInputs.text = currentText;
			yield return new WaitForSeconds(delay);
			CheckIfDone();
		}
	}

	private IEnumerator WaitForSecondsShowText()
	{
		yield return new WaitForSeconds(textDelay);
		StartCoroutine(ShowText());

	}
}
