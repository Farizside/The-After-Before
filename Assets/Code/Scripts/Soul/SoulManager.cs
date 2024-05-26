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
    }

    private void AttractSoul(GameObject soul)
    {
        if(soul.tag == "Soul" && !_soulsAttracted.Contains(soul))
        {
            soul.GetComponent<SoulMovementController>().IsAttracted = true;
            _soulsAttracted.Add(soul);
            _soulsUnattracted.Remove(soul);
        }
    }

    private void AttackSoulByIndex(int index)
    {
        List<GameObject> lostSouls = _soulsAttracted.GetRange(index, _soulsAttracted.Count - index);
        foreach(GameObject soul in lostSouls)
        {
            soul.GetComponent<SoulMovementController>().IsAttracted = false;
            soul.GetComponent<SoulTypeController>().SoulType = SoulType.LOST;
            _soulsUnattracted.Add(soul);
        }
        _soulsAttracted.RemoveRange(index, _soulsAttracted.Count - index);

    }

    // somehow this is not working idk why
    private void AttractSoulByPlayerPosition(Vector3 playerPosition)
    {
        List<GameObject> soulToAttract = new();
        foreach(GameObject soul in _soulsUnattracted)
        {
            if(Vector3.Distance(soul.transform.position, playerPosition) < _minDist)
            {
                soulToAttract.Add(soul);
            }
        }
        foreach(GameObject soul in soulToAttract)
        {
            AttractSoul(soul);
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
        List<GameObject> soulToAttract = new List<GameObject>(_soulsUnattracted);
        foreach(GameObject soul in soulToAttract)
        {
            AttractSoul(soul);
        }
        _soulsUnattracted.Clear();
    }
    
    private void UpdateTargetTransform()
    {
        Vector3 _targetPosition = _playerGO.transform.position;
        for(int i=0; i<_soulsAttracted.Count; i++)
        {
            GameObject soul = _soulsAttracted[i];
            SoulMovementController soulMovementController = soul.GetComponent<SoulMovementController>();
            if (Vector3.Distance(soulMovementController.TargetPosition, _targetPosition) > 0.1f)
            {
                soulMovementController.TargetPosition = _targetPosition;
            }
            _targetPosition = soul.transform.position;
        }
    }

    // Event Listener
    public void OnAttract(Component sender, object data)
    {
        Debug.Log(sender is SoulDetectorController);
        if (sender is SoulDetectorController && data is List<GameObject>)
        {
            foreach (GameObject soul in (List<GameObject>)data)
            {
                AttractSoul(soul);
            }
        }

        // This approach can be used if soul's rigidbody is removed
        //if (sender is SoulDetectorController && data is Vector3)
        //{
        //    AttractSoulByPlayerPosition((Vector3)data);
        //}
    }

    public void OnDeattract(Component sender, object data)
    {
        if (sender is SoulDetectorController)
        {
            DeattractSoul();
        }
    }

    public void OnSpawned(Component sender, object data)
    {
        if(sender is SoulSpawner && data is GameObject && ((GameObject)data).tag == "Soul")
        {
            _soulsUnattracted.Add((GameObject)data);
        }
    }

    public void OnSubmitted(Component sender, object data)
    {
        List<GameObject> submittedSoul = new(_soulsAttracted);
        _soulsAttracted.Clear();
        foreach(GameObject soul in submittedSoul)
        {
            soul.GetComponent<SoulMovementController>().IsAttracted = false;
            soul.SetActive(false);
        }
    }

    public void OnSoulAttacked(Component sender, object data)
    {
        if(data is GameObject && ((GameObject)data).tag == "Soul")
        {
            GameObject soul = (GameObject)data;
            int idx = _soulsAttracted.IndexOf(soul);
            AttackSoulByIndex(idx);
        }
    }

    public void OnSoulPurified(Component sender, object data)
    {
        foreach(GameObject soul in _soulsAttracted)
        {
            soul.GetComponent<SoulTypeController>().SoulType = SoulType.PURE;
        }
    }
}
