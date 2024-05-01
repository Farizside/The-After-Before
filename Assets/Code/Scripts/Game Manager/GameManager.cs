using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager _input;
    [SerializeField] [CanBeNull] private GameObject _uiCanvas;
    [SerializeField] [CanBeNull] private TMP_Text _soulCollectedText;

    public static event Action<GameState> OnGameStateChanged; 
    
    public GameState State;
    
    private int _soulCollected = 0;

    public int SoulCollected => _soulCollected;

    public static GameManager Instance;

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
    }

    private void Start()
    {
        _input.PauseEvent += HandlePause;
        _input.ResumeEvent += HandleResume;
        
        UpdateGameState(GameState.Gameplay);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.Gameplay:
                HandleGameplay();
                break;
            case GameState.UI:
                break;
            case GameState.Upgrade:
                Debug.Log("Upgrade");
                HandleUpgrade();
                break;
            case GameState.Victory:
                Debug.Log("Victory");
                break;
            case GameState.Lose:
                Debug.Log("Lose");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        
        OnGameStateChanged?.Invoke(newState);
    }

    private async void HandleUpgrade()
    {
        await Task.Delay(5000);
        
        UpdateGameState(GameState.Gameplay);
    }

    private void HandleGameplay()
    {
        
    }

    private void HandlePause()
    {
        if (_uiCanvas != null) _uiCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void HandleResume()
    {
        if (_uiCanvas != null) _uiCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void SubmitSoul()
    {
        _soulCollected += 1;
        _soulCollectedText.text = _soulCollected.ToString();
    }
}

public enum GameState
{
    Gameplay,
    UI,
    Upgrade,
    Victory,
    Lose
}
