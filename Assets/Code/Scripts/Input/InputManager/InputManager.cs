using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputAsset")]
public class InputManager : ScriptableObject, InputAssets.IGameplayActions, InputAssets.IUIActions,
    InputAssets.ITutorialActions
{
    private static InputAssets _inputAssets;

    private void OnEnable()
    {
        if (_inputAssets == null)
        {
            _inputAssets = new InputAssets();
            
            _inputAssets.Gameplay.SetCallbacks(this);
            _inputAssets.UI.SetCallbacks(this);
            _inputAssets.Tutorial.SetCallbacks(this);
            
            SetUI();
        }
    }

    public static void SetGameplay()
    {
        _inputAssets.Gameplay.Enable();
        _inputAssets.UI.Disable();
        _inputAssets.Tutorial.Disable();
    }
    
    public static void SetUI()
    {
        _inputAssets.Gameplay.Disable();
        _inputAssets.UI.Enable();
        _inputAssets.Tutorial.Disable();
    }

    public static void SetTutorial()
    {
        _inputAssets.Gameplay.Disable();
        _inputAssets.UI.Disable();
        _inputAssets.Tutorial.Enable();
    }
    
    public event Action<Vector2> MoveEvent;
    public event Action DashEvent; 
    public event Action AttractEvent;
    public event Action DeattractEvent;
    public event Action PauseEvent;
    public event Action ResumeEvent;
    public event Action BackEvent;
    public event Action NextEvent;

    public event Action SkipEvent;

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }
    
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            DashEvent?.Invoke();
        }
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
        }
    }

    public void OnResume(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ResumeEvent?.Invoke();
        }
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            BackEvent?.Invoke();
        }
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        
    }

    public void OnScrollWheel(InputAction.CallbackContext context)
    {
        
    }

    public void OnMiddleClick(InputAction.CallbackContext context)
    {
        
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        
    }

    public void OnTrackedDevicePosition(InputAction.CallbackContext context)
    {
        
    }

    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
    {
        
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            NextEvent?.Invoke();
        }
    }

    public void OnBack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            BackEvent?.Invoke();
        }
    }

    public void OnSkip(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            SkipEvent?.Invoke();
        }
    }
}
