using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager _input;

    private bool _isPaused = false;
    private int _soulCollected { get; set; } = 0;

    public int SoulCollected
    {
        get => _soulCollected;
        set
        {
            _soulCollected = value;
            Debug.Log(_soulCollected);
        }
    }

    public static GameManager Instance { get; set; }

    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
        
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _input.PauseEvent += HandlePause;
        _input.ResumeEvent += HandleResume;
    }

    private void HandlePause()
    {
        Debug.Log("Paused");
        Time.timeScale = 0;
    }

    private void HandleResume()
    {
        Debug.Log("Resumed");
        Time.timeScale = 1;
    }
}
