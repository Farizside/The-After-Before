using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoulManager : MonoBehaviour
{
    [Header("Soul Management")]
    [SerializeField] private List<GameObject> _soulsUnattracted;
    [SerializeField] private List<GameObject> _soulsAttracted;
    private GameObject _playerGO;

    [Header("Movement Variable While Attracted")]
    [SerializeField] private float _minDist = 1.5f;
    [SerializeField] private float _speed = 1.0f;

    [Header("Movement Variable While Unattracted")]
    [SerializeField] private float _rangeDist = 3.0f;

    private void Start()
    {
        GameObject[] souls = GameObject.FindGameObjectsWithTag("Soul");
        foreach (GameObject soul in souls)
        {
            _soulsUnattracted.Add(soul);
        }
        _playerGO = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        UpdateTargetTransform();
        Move();
    }

    private void AttractSoul(GameObject soul)
    {
        if(soul.tag == "Soul")
        {
            soul.GetComponent<SoulMovementController>().IsAttracted = true;
            _soulsAttracted.Add(soul);
            _soulsUnattracted.Remove(soul);
        }
    }

    [ContextMenu("Deattract All Soul")]
    private void DeattractSoul()
    {
        foreach(GameObject soul in _soulsAttracted)
        {
            soul.GetComponent<SoulMovementController>().IsAttracted = false;
            _soulsUnattracted.Add(soul);
        }
        _soulsAttracted.Clear();
    }

    [ContextMenu("Attract All Soul")]
    private void AttractAllSoul()
    {
        foreach(GameObject soul in _soulsUnattracted)
        {
            AttractSoul(soul);
        }
        _soulsUnattracted.Clear();
    }

    private void Move()
    {
        //Transform targetTransform = _playerTransform;
        //for (int i= 0; i < _soulsAttracted.Count; i++)
        //{
        //    GameObject soul = _soulsAttracted[i];
        //    float dist = Vector3.Distance(soul.transform.position, targetTransform.position);
        //    if (dist > _minDist)
        //    {
        //        float step = (dist - _minDist + 0.1f)* _speed * Time.deltaTime;
        //        soul.transform.position = Vector3.MoveTowards(soul.transform.position, targetTransform.position, step);
        //    }


        //    targetTransform = _soulsAttracted[i].transform;
        //}
        //for(int i=0; i<_soulsUnattracted.Count; i++)
        //{
        //    GameObject soul = _soulsUnattracted[i];
        //    soul.GetComponent<SoulMovementController>().MoveWhileUnattracted();
        //}
    }
    
    private void UpdateTargetTransform()
    {
        Vector3 _targetPosition = _playerGO.transform.position;
        for(int i=0; i<_soulsAttracted.Count; i++)
        {
            _soulsAttracted[i].GetComponent<SoulMovementController>().TargetPosition = _targetPosition;
            _targetPosition = _soulsAttracted[i].transform.position;
        }
    }
}
