using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningTrigger : MonoBehaviour
{
    [SerializeField]
    private Vector3 _newPlayerRotation;

    private bool _isActive = true;

    private void OnTriggerEnter(Collider other)
    {
        if (_isActive)
        {
            MainCharacterController characterController = other.GetComponent<MainCharacterController>();
            characterController.ChangeRotationAndDirection(new Vector3(0, 0, 0), Vector3.forward, new Vector3(10, -8, -11), new Vector3(0, 7, -12));
            _isActive = false;
        }
    }

    // forvard Vector 44, -19, -11
    // right Vector 37.86f, 86, 2
}
