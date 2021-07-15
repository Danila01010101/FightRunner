using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public int _coinAmount { set; get; }

    private string _savedCoinAmount = "Coins";
    private string _savedCoinMultiplier = "CoinMultiplier";

    private TextMeshProUGUI _coinText;

    private int _coinMultiplier = 1;

    void Start()
    {
        _coinText = gameObject.GetComponent<TextMeshProUGUI>();
        _coinAmount = PlayerPrefs.GetInt(_savedCoinAmount);
        _coinMultiplier = PlayerPrefs.GetInt(_savedCoinMultiplier);
        if (_coinMultiplier == 0)
        {
            IncreaseCoinMultiplier();
        }
        UpdateCoins();
    }

    public void IncreaseCoins()
    {
        _coinAmount += 1 * _coinMultiplier;
        UpdateCoins();
    }

    public void GetRevard(int revard)
    {
        _coinAmount += revard * _coinMultiplier;
        UpdateCoins();
    }

    private void UpdateCoins()
    {
        _coinText.text = _coinAmount.ToString();
        PlayerPrefs.SetInt(_savedCoinAmount, _coinAmount);
    }

    public void IncreaseCoinMultiplier()
    {
        PlayerPrefs.SetInt(_savedCoinMultiplier, PlayerPrefs.GetInt(_savedCoinMultiplier) + 1);
        _coinMultiplier += 1;
        UpdateCoins();
    }
}
