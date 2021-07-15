using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCenter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        MainCharacterController player = other.GetComponent<MainCharacterController>();
        player._canControll = false;
        player._endOfLevelPos = gameObject.transform.position.x;
        if (player._isInBersercMode)
        {
            player._isInBersercMode = false;
        }
    }
}
