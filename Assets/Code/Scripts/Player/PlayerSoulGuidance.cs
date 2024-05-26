using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSoulGuidance : MonoBehaviour
{
    [SerializeField] private InputManager _input;
    
    [Header("Soul Management")]
    [SerializeField] private List<SoulMovementController> _soulsAttracted;

    [Header("Movement Variable While Attracted")]
    [SerializeField] private float _minDist = 1.5f;
    [SerializeField] private float _speed = 1.0f;

    [Header("Movement Variable While Unattracted")]
    [SerializeField] private float _rangeDist = 3.0f;

    private PlayerMovement _playerMovement;
    private SoulMovementController _currSoul;

    public float SlowingSpeed;
    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _input.DeattractEvent += DeattractSoul;
    }

    private void Attract()
    {
        if (!_currSoul.IsAttracted)
        {
            if(_soulsAttracted.Count == 0)
            {
                _currSoul.IsFirst = true;
            }
            AttractSoul(_currSoul);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Soul"))
        {
            _currSoul = other.GetComponent<SoulMovementController>();
            _input.AttractEvent += Attract;
            if (!_currSoul.IsAttracted)
            {
                AddOutline(other);   
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Soul"))
        {
            _currSoul = null;
            _input.AttractEvent -= Attract;
            RemoveOutline(other);
        }
    }

    private void Update()
    {
        UpdateTargetTransform();
    }

    private void AttractSoul(SoulMovementController soul)
    {
        if(!_soulsAttracted.Contains(soul))
        {
            soul.IsAttracted = true;
            _soulsAttracted.Add(soul);
            if (_playerMovement.MovementSpeed > _playerMovement.MovementSpeedLimit)
            {
                _playerMovement.MovementSpeed -= SlowingSpeed;
            }
        }
    }

    [ContextMenu("Deattract All Soul")]
    private void DeattractSoul()
    {
        foreach(SoulMovementController soul in _soulsAttracted)
        {
            soul.IsAttracted = false;
        }

        _playerMovement.MovementSpeed += _soulsAttracted.Count * SlowingSpeed;
        _soulsAttracted.Clear();
    }

    public void AttackSoul(SoulMovementController soulMovementController)
    {
        int index = _soulsAttracted.IndexOf(soulMovementController);
        if (index != -1)
        {
            List<SoulMovementController> attackedSouls = _soulsAttracted.GetRange(index, _soulsAttracted.Count - index);
            foreach (SoulMovementController soul in attackedSouls)
            {
                soul.IsAttracted = false;
                soul.GetComponent<SoulTypeController>().SoulType = SoulType.LOST;
            }
            _soulsAttracted.RemoveRange(index, _soulsAttracted.Count - index);
        }

    }

    public void SubmitSoul()
    {
        foreach(SoulMovementController soul in _soulsAttracted)
        {
            if (soul.GetComponent<SoulTypeController>().SoulType == SoulType.PURE)
            {
                soul.IsAttracted = false;
                soul.gameObject.SetActive(false);
                _playerMovement.MovementSpeed += SlowingSpeed;
                GameManager.Instance.SubmitSoul();
            }
            else
            {
                return;
            }
        }
        
        _soulsAttracted.Clear();
    }
    
    private void UpdateTargetTransform()
    {
        Vector3 targetPosition = transform.position;
        for(int i=0; i<_soulsAttracted.Count; i++)
        {
            _soulsAttracted[i].TargetPosition = targetPosition;
            targetPosition = _soulsAttracted[i].transform.position;
        }
    }

    private void AddOutline(Collider other)
    {
        var outline = other.GetComponent<Outline>() ? other.GetComponent<Outline>() : other.AddComponent<Outline>();
        outline.enabled = true;
        outline.OutlineWidth = 7f;
    }

    private void RemoveOutline(Collider other)
    {
        var outline = other.GetComponent<Outline>() ? other.GetComponent<Outline>() : other.AddComponent<Outline>();
        outline.enabled = false;
    }
}
