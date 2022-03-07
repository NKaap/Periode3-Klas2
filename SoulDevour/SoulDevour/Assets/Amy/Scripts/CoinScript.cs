using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinScript : MonoBehaviour
{
    // money manager 


    public Text coinText;
    public float currency;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            currency += 1;
            coinText.text = currency.ToString();
        }
    }
}
