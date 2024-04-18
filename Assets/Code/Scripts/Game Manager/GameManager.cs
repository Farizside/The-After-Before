using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager _input;
    [SerializeField] private GameObject _uiCanvas;
    [SerializeField] private TMP_Text _soulCollectedText;

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
        
        // DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _input.PauseEvent += HandlePause;
        _input.ResumeEvent += HandleResume;
    }

    private void HandlePause()
    {
        _uiCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void HandleResume()
    {
        _uiCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void SubmitSoul()
    {
        _soulCollected += 1;
        _soulCollectedText.text = _soulCollected.ToString();
    }
}
