using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Camera _mainCamera;

    [SerializeField] private int _damageAmount = 5;

    [SerializeField] private GameObject[] _bloodSpatterPrefabs;

    [SerializeField] private LayerMask _layer;

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

        if (Physics.Raycast(ray, out hit, 50f, _layer, QueryTriggerInteraction.Ignore))
        {
            Debug.Log("Passed through");
            Debug.Log(hit.collider.name);
            Health targetHealth = hit.collider.gameObject.GetComponent<Health>();
            if (targetHealth != null)
            {
                InstantiateBloodSpatter(hit);
                targetHealth.Damage(_damageAmount);
            }
        }      
        else
        {
            Debug.Log("Ignored");
        }

        //if (Physics.Raycast(ray, out hit))
        //{
        //    Debug.Log(hit.collider.name);
        //    Health targetHealth = hit.collider.gameObject.GetComponent<Health>();
        //    if (targetHealth != null)
        //    {
        //        InstantiateBloodSpatter(hit);
        //        targetHealth.Damage(_damageAmount);
        //    }
        //}
    }

    private void InstantiateBloodSpatter(RaycastHit hit)
    {
        GameObject _spatterPrefab = _bloodSpatterPrefabs[Random.Range(0, _bloodSpatterPrefabs.Length)];
        GameObject spatter = Instantiate(_spatterPrefab, hit.point + (hit.normal * 0.01f), Quaternion.LookRotation(hit.normal));
        spatter.transform.localScale = spatter.transform.localScale * 0.5f;
        spatter.transform.parent = hit.transform;
    }
}
