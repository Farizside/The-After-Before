using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private InputManager _input;
    [SerializeField] private float _movemenentSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _movementSpeedLimit;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashCooldown;

    public float MovementSpeed
    {
        get => _movemenentSpeed;
        set => _movemenentSpeed = value;
    }
    public float MovementSpeedLimit => _movementSpeedLimit;

    private Animator _animator;
    private CharacterController _characterController;
    
    private int _isWalkingHash;
    private Vector3 _currentMovement;
    private bool _isMovementPressed;
    private Vector3 _faceTransform;
    private bool _isDashCooldown = false;
    private float _dashCurrentCooldown;
    private int _isDashingHash;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        
        _isWalkingHash = Animator.StringToHash("isWalking");
        _isDashingHash = Animator.StringToHash("isDash");
        
        _input.MoveEvent += HandleMove;
        _input.DashEvent += HandleDash;

        _dashCurrentCooldown = _dashCooldown;
    }

    private void Update()
    {
        HandleAnimation();
        HandleRotation();
        HandleGravity();
        HandleDirection();
        HandleCooldown();
        Move();
    }

    private void HandleAnimation()
    {
        bool isWalking = _animator.GetBool(_isWalkingHash);

        if (_isMovementPressed && !isWalking)
        {
            _animator.SetBool(_isWalkingHash, true);
        }else if (!_isMovementPressed && isWalking)
        {
            _animator.SetBool(_isWalkingHash, false);
        }
    }

    private void HandleMove(Vector2 dir)
    {
        _currentMovement = new Vector3(dir.x, 0, dir.y);
        _isMovementPressed = dir.x != 0 || dir.y != 0;
    }

    private void Move()
    {
        if (_currentMovement == Vector3.zero)
        {
            return;
        }

        _characterController.Move(_currentMovement * (_movemenentSpeed * Time.deltaTime));
    }

    private void HandleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = _currentMovement.x;
        positionToLookAt.y = 0;
        positionToLookAt.z = _currentMovement.z;
        
        Quaternion currentRotation = transform.rotation;

        if (_isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    private void HandleGravity()
    {
        if (_characterController.isGrounded)
        {
            float groundedGravity = -.05f;
            _currentMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -9.8f;
            _currentMovement.y += gravity;
        }
    }

    private void HandleDirection()
    {
        if (_isMovementPressed)
        {
            _faceTransform = _currentMovement;
        }
    }

    private void HandleCooldown()
    {
        if (!_isDashCooldown) return;
        if (_dashCurrentCooldown > 0)
        {
            _dashCurrentCooldown -= Time.deltaTime;
        }
        else
        {
            _dashCurrentCooldown = _dashCooldown;
            _isDashCooldown = false;
        }
    }

    private void HandleDash()
    {
        if (_isDashCooldown) return;
        StartCoroutine(Dash());
        _isDashCooldown = true;
    }
    
    private IEnumerator Dash()
    {
        _animator.SetTrigger(_isDashingHash);
        float startTime = Time.time;

        while (Time.time < startTime + _dashTime)
        {
            _characterController.Move(_faceTransform * ((_movemenentSpeed * _dashSpeed) * Time.deltaTime));

            yield return null;
        }
        
    }
}
