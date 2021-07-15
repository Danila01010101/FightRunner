using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSphereBehaciour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<MainCharacterController>().Heal();
        Destroy(gameObject);
    }
}
