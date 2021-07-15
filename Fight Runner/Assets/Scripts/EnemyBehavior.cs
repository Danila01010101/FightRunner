using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _hitEffect;
    public bool _isAlive = true;
    public int straightLevel;
    [SerializeField]
    private TextMeshProUGUI _straightText;
    public bool _isFighting = false;
    private string _animIsFightingBool = "IsFighting";
    private string _animIsAliveBool = "IsAlive";
    private string _enemyLevelMultiplierName = "EnemyLevelMultiplier";

    public Animator _animator;
    private void Start()
    {
        straightLevel = Random.Range(9, 8*3) + PlayerPrefs.GetInt(_enemyLevelMultiplierName);
        _animator = gameObject.GetComponent<Animator>();
        _straightText.GetComponent<TextMeshProUGUI>().text = straightLevel.ToString();
        if (PlayerPrefs.GetInt("Damage") == 0)
        {
            PlayerPrefs.SetInt("Damage", 3);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_isAlive)
        {
            _isFighting = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isFighting = false;
        _animator.SetBool(_animIsFightingBool, _isFighting);
    }

    public void TakeHit()
    {
        if (_isFighting)
        {
            _animator.SetBool(_animIsFightingBool,_isFighting);
            straightLevel -= PlayerPrefs.GetInt("Damage");
            if (straightLevel < 0)
                straightLevel = 0;
            _straightText.text = straightLevel.ToString(); 
            Instantiate(_hitEffect,
                 new Vector3(gameObject.transform.position.x + Random.Range(-130, 130) /100, gameObject.transform.position.y + Random.Range(-130, 130) /100 + 0.8f, gameObject.transform.position.z + Random.Range(-130, 130) /100),
                 _hitEffect.transform.rotation);
        }
    }

    public void LoseFight()
    {
        Destroy(_straightText);
        _isAlive = false;
        _isFighting = false;
        _animator.SetBool(_animIsAliveBool,_isAlive);
    }


    private void WinFight()
    {

    }
}
