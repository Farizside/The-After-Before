using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Rendering.UI;

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

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _input.DeattractEvent += DeattractSoul;
    }

    private void Attract()
    {
        if (!_currSoul.IsAttracted)
        {
            AttractSoul(_currSoul);
            Debug.Log("Attracted");
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
                _playerMovement.MovementSpeed -= 1;
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

        _playerMovement.MovementSpeed += _soulsAttracted.Count;
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
