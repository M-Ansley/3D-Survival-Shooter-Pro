using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Camera _mainCamera;

    [SerializeField] private int _damageAmount = 5;

    [SerializeField] private GameObject[] _bloodSpatterPrefabs;
    
    void Start()
    {
        _mainCamera = Camera.main;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    private void Fire()
    {
        RaycastHit hit;
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.name);
            Health targetHealth = hit.collider.gameObject.GetComponent<Health>();
            if (targetHealth != null)
            {
                InstantiateBloodSpatter(hit);                
                targetHealth.Damage(_damageAmount);
            }
        }
    }

    private void InstantiateBloodSpatter(RaycastHit hit)
    {
        GameObject spatter = Instantiate(_bloodSpatterPrefabs[Random.Range(0, _bloodSpatterPrefabs.Length)], hit.point + (hit.normal * 0.01f), Quaternion.LookRotation(hit.normal));
        spatter.transform.localScale = spatter.transform.localScale * 0.5f;
        spatter.transform.parent = hit.transform;
    }
}
