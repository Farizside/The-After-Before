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
    [SerializeField] private GameObject _tutorial1;
    [SerializeField] private GameObject _tutorial2;
    
    private GameObject _tutorial;
    
    private WaveManager _wave;
    private UIManager _ui;
    private UpgradeManager _upgrade;
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

    private void OnEnable()
    {
        _input.PauseEvent += HandlePause;
        _input.ResumeEvent += HandleResume;
    }

    private void OnDisable()
    {
        _input.PauseEvent -= HandlePause;
        _input.ResumeEvent -= HandleResume;
    }

    private void Start()
    {
        _wave = WaveManager.Instance;
        _ui = UIManager.Instance;
        _upgrade = UpgradeManager.Instance;

        if (_wave.CurWaveId == 0)
        {
            _tutorial = _tutorial1;
            UpdateGameState(GameState.Tutorial);
        }else if (_wave.CurWaveId == 4)
        {
            _tutorial = _tutorial2;
            UpdateGameState(GameState.Tutorial);
        }
        else
        {
            UpdateGameState(GameState.Gameplay);
        }
        
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
        _ui.HideAllCanvases();
        _tutorial.SetActive(true);
        InputManager.SetTutorial();
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
        AudioManager.Instance.PauseAudio(); //AudioManager
    }

    public void HandleResume()
    {
        if (State == GameState.Upgrade || State == GameState.Tutorial) return;
        UpdateGameState(GameState.Gameplay);
        InputManager.SetGameplay();
        AudioManager.Instance.ResumeAudio(); //AudioManager

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
        _upgrade.StartWave();
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
