using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesLine : MonoBehaviour
{
    public int _lineIndex;

    public void DesableLine()
    {
        for (int i = 0; i < 4; i++)
        {
            gameObject.transform.GetChild(i).GetComponent<EnemyBehavior>()._isFighting = false;
        }
    }
}
