using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager _input;

    private bool _isPaused = false;

    private void Start()
    {
        _input.PauseEvent += HandlePause;
        _input.ResumeEvent += HandleResume;
    }

    private void HandlePause()
    {
        Debug.Log("Paused");
    }

    private void HandleResume()
    {
        Debug.Log("Resumed");
    }
}
