using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLineBehaviour : MonoBehaviour
{
    [SerializeField]
    private int _index;

    private void OnCollisionEnter(Collision collision)
    {
        GetComponentInParent<RevardCounter>().GetRevard(_index);
    }
}
