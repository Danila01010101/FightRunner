using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 1;
    void Update()
    {
        transform.Rotate(Vector3.forward, 1 * _rotationSpeed);
    }
}
