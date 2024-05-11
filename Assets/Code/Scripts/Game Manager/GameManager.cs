using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager _input;
    
    private WaveManager _wave;
    
    // To Do: Pindahin ke UI Manager
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
            DontDestroyOnLoad(this);
        } 
    }

    private void Start()
    {
        _input.PauseEvent += HandlePause;
        _input.ResumeEvent += HandleResume;

        _wave = WaveManager.Instance;
        
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
                HandleUI();
                break;
            case GameState.Upgrade:
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

    private void HandleLose()
    {
        
    }
    
    private void HandleVictory()
    {
        
    }
    
    private void HandleUpgrade()
    {
        InputManager.SetUI();
        if (_uiCanvas) _uiCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    private void HandleGameplay()
    {
        if (_uiCanvas) _uiCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    private void HandleUI()
    {
        if (_uiCanvas) _uiCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    private void HandlePause()
    {
        UpdateGameState(GameState.UI);
        InputManager.SetUI();
    }

    public void HandleResume()
    {
        if (State == GameState.Upgrade) return;
        UpdateGameState(GameState.Gameplay);
        InputManager.SetGameplay();
    }

    public void SubmitSoul()
    {
        _soulCollected += 1;
        _soulCollectedText.text = _soulCollected.ToString();
    }

    public void OnNextWaveClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        InputManager.SetGameplay();
        UpdateGameState(GameState.Gameplay);
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
