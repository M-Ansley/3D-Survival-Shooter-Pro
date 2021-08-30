using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothIKLerp : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float _speed = 10f;

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * _speed);
        transform.rotation = Quaternion.Euler(target.transform.rotation.eulerAngles);
    }
}
