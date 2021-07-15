using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItems : MonoBehaviour
{
    private int _damageIcreaserCost = 200;
    private int _coinIcreaserCost = 200;

    private string _savedDamageIcreaserCost = "DamageIcreaserCost";
    private string _savedCoinIcreaserCost = "CoinIcreaserCost";

    private CoinCounter _coinCounter;
    private MainCharacterController _playerController;
    private void Awake()
    {
        _coinCounter = GameObject.Find("Interface/Coins").GetComponent<TextMeshProUGUI>().GetComponent<CoinCounter>();
        _playerController = GameObject.Find("Character").GetComponent<MainCharacterController>();
        if (PlayerPrefs.GetInt(_savedDamageIcreaserCost) == 0)
            PlayerPrefs.SetInt(_savedDamageIcreaserCost, _damageIcreaserCost);
        else
            _damageIcreaserCost = PlayerPrefs.GetInt(_savedDamageIcreaserCost);
        if (PlayerPrefs.GetInt(_savedCoinIcreaserCost) == 0)
            PlayerPrefs.SetInt(_savedCoinIcreaserCost, _coinIcreaserCost);
        else
            _coinIcreaserCost = PlayerPrefs.GetInt(_savedCoinIcreaserCost);
        if (gameObject.CompareTag("CoinIncreaser"))
            gameObject.transform.GetChild(0).GetComponentInChildren<Text>().text = _coinIcreaserCost.ToString();

        if (gameObject.CompareTag("DamageIncreaser"))
            gameObject.transform.GetChild(0).GetComponentInChildren<Text>().text = _damageIcreaserCost.ToString();

    }

    public void AddDamage()
    {
        if (_coinCounter._coinAmount >= _damageIcreaserCost)
        {
            PlayerPrefs.SetInt("Damage", PlayerPrefs.GetInt("Damage")+1);
            _damageIcreaserCost *= 2;
            PlayerPrefs.SetInt(_savedDamageIcreaserCost, _damageIcreaserCost);
            gameObject.transform.GetChild(0).GetComponentInChildren<Text>().text = _damageIcreaserCost.ToString();

            _coinCounter._coinAmount -= _damageIcreaserCost;
        }
    }

    public void IncreaseCoinMultiplier()
    {
        if (_coinCounter._coinAmount >= _coinIcreaserCost)
        {
            _coinCounter.IncreaseCoinMultiplier();
            _coinCounter._coinAmount -= _coinIcreaserCost;
            _coinIcreaserCost *= 2;
            PlayerPrefs.SetInt(_savedCoinIcreaserCost, _coinIcreaserCost);
            gameObject.transform.GetChild(0).GetComponentInChildren<Text>().text = _coinIcreaserCost.ToString();
        }
    }


    public void DeleteSaves()
    {
        PlayerPrefs.DeleteAll();
    }
}
