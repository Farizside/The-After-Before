using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoulMovementController : MonoBehaviour
{
    [Header("Soul Management")]
    [SerializeField] private GameObject[] _souls;
    [SerializeField] private List<GameObject> _soulsAttracted;
    private GameObject _playerGO;
    private Transform _playerTransform;

    [Header("Movement Variable")]
    [SerializeField] private float _minDist = 1.5f;
    [SerializeField] private float _speed = 1.0f;

    private void Start()
    {
        _souls = GameObject.FindGameObjectsWithTag("Soul");
        _playerGO = GameObject.FindGameObjectWithTag("Player");
        _playerTransform = _playerGO.transform;
    }

    private void Update()
    {
        UpdatePlayerTransform();
        Move();
    }

    private void AttractSoul(GameObject soul)
    {
        if(soul.tag == "Soul")
        {
            _soulsAttracted.Add(soul);
        }
    }
    [ContextMenu("Deattract All Soul")]
    private void DeattractSoul()
    {
        _soulsAttracted.Clear();
    }

    [ContextMenu("Attract All Soul")]
    private void AttractAllSoul()
    {
        foreach(GameObject soul in _souls)
        {
            AttractSoul(soul);
        }
    }

    private void Move()
    {
        Transform targetTransform = _playerTransform;
        for (int i= 0; i < _soulsAttracted.Count; i++)
        {
            GameObject soul = _soulsAttracted[i];
            float dist = Vector3.Distance(soul.transform.position, targetTransform.position);
            if (dist > _minDist)
            {
                float step = (dist - _minDist + 0.1f)* _speed * Time.deltaTime;
                soul.transform.position = Vector3.MoveTowards(soul.transform.position, targetTransform.position, step);
            }
                

            targetTransform = _soulsAttracted[i].transform;
        }
    }

    private void UpdatePlayerTransform()
    {
        _playerTransform = _playerGO.transform;
    }
}
