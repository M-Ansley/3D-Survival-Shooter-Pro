using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothIKLerp : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float _speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = target.transform.position;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _speed);
        transform.rotation = Quaternion.Euler(target.transform.rotation.eulerAngles);
    }
}
