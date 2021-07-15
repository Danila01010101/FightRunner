using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StraightBar : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    //private void Start()
    //{
    //    _slider = gameObject.GetComponent<Slider>();
    //}
    public void SetMaxBarValue(int value)
    {
        _slider.maxValue = value;
    }
    public void SetStraight(int straightValue)
    {
        _slider.value = straightValue;
    }

    
}
