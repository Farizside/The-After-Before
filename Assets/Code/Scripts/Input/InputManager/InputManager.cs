using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputAsset")]
public class InputManager : ScriptableObject, InputAssets.IGameplayActions, InputAssets.IUIActions
{
    private InputAssets _inputAssets;

    private void OnEnable()
    {
        if (_inputAssets == null)
        {
            _inputAssets = new InputAssets();
            
            _inputAssets.Gameplay.SetCallbacks(this);
            _inputAssets.UI.SetCallbacks(this);
            
            // To Do: Change this later
            SetGameplay();
        }
    }

    public void SetGameplay()
    {
        _inputAssets.Gameplay.Enable();
        _inputAssets.UI.Disable();
    }
    
    public void SetUI()
    {
        _inputAssets.Gameplay.Disable();
        _inputAssets.UI.Enable();
    }
    
    public event Action<Vector2> MoveEvent;
    public event Action AttractEvent;
    public event Action DeattractEvent;
    public event Action PauseEvent;
    public event Action ResumeEvent;

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnAttract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            AttractEvent?.Invoke();
        }
    }

    public void OnDeattract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            DeattractEvent?.Invoke();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            PauseEvent?.Invoke();
            SetUI();
        }
    }

    public void OnResume(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ResumeEvent?.Invoke();
            SetGameplay();
        }
    }
}
