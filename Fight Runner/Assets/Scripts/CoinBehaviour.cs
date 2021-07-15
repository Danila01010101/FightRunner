using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinBehaviour : MonoBehaviour
{
    private string _playerTag = "Player";

    private TextMeshProUGUI _coinText;
    private CoinCounter _coinCounter;


    private void Start()
    {
        _coinText = GameObject.Find("Interface/Coins").GetComponent<TextMeshProUGUI>();
        _coinCounter = _coinText.GetComponent<CoinCounter>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == _playerTag)
        {
            Destroy(gameObject);
            _coinCounter.IncreaseCoins();
        }
    }
}
