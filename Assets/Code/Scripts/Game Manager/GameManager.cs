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
    private UIManager _ui;
    public static event Action<GameState> OnGameStateChanged; 
    
    public GameState State;
    
    private int _soulCollected = 0;

    public int SoulCollected => _soulCollected;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _input.PauseEvent += HandlePause;
        _input.ResumeEvent += HandleResume;

        _wave = WaveManager.Instance;
        _ui = UIManager.Instance;
        
        UpdateGameState(GameState.Tutorial);
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
                HandleVictory();
                break;
            case GameState.Lose:
                HandleLose();
                break;
            case GameState.Tutorial:
                HandleTutorial();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        
        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleTutorial()
    {
        _ui.Tutorial1Canvas();
        InputManager.SetUI();
        Time.timeScale = 0;
    }
    
    private void HandleLose()
    {
        _ui.LoseCanvas();
        InputManager.SetUI();
        Time.timeScale = 0;
    }
    
    private void HandleVictory()
    {
        _ui.WinCanvas();
        InputManager.SetUI();
        Time.timeScale = 0;
    }
    
    private void HandleUpgrade()
    {
        _ui.UpgradeCanvas();
        InputManager.SetUI();
        Time.timeScale = 0;
    }

    private void HandleGameplay()
    {
        _ui.HUDCanvas();
        Time.timeScale = 1;
    }

    private void HandleUI()
    {
        _ui.PauseCanvas();
        Time.timeScale = 0;
    }

    public void HandlePause()
    {
        UpdateGameState(GameState.UI);
        InputManager.SetUI();
    }

    public void HandleResume()
    {
        if (State == GameState.Upgrade || State == GameState.Tutorial) return;
        UpdateGameState(GameState.Gameplay);
        InputManager.SetGameplay();
    }

    public void SubmitSoul()
    {
        _soulCollected += 1;
    }

    public void OnNextWaveClicked()
    {
        _soulCollected = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        InputManager.SetGameplay();
        UpdateGameState(GameState.Gameplay);
        _wave.SetCurrentData();
    }

    public void OnTutorialFinished()
    {
        UpdateGameState(GameState.Gameplay);
        InputManager.SetGameplay();
        _ui.HUDCanvas();
        Time.timeScale = 1;
    }
}

public enum GameState
{
    Gameplay,
    UI,
    Upgrade,
    Victory,
    Lose,
    Tutorial
}
