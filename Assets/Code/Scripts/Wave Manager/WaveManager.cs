using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private WaveData _waveData;

    public WaveData WaveData => _waveData;

    private int _curWaveId;
    
    // To Do : Integrasi ke UI Manager
    private float _curWaveTime;
    private int _curWaveTarget;
    
    // To Do : Integrasi Ke Soul Spawner
    private int _curWaveInitialSoul;
    private float _curWaveInterval;
    private float _curWavePureRate;
    private int _curWaveMaxSouls;

    public int CurWaveId
    {
        get => _curWaveId;
        set => _curWaveId = value;
    }

    public float CurWaveTime => _curWaveTime;
    public int CurWaveTarget => _curWaveTarget;
    public int CurWaveInitialSoul => _curWaveInitialSoul;
    public float CurWaveInterval => _curWaveInterval;
    public float CurWavePureRate => _curWavePureRate;
    public int CurWaveMaxSouls => _curWaveMaxSouls;

    private UIManager _ui;
    private UpgradeManager _upgrade;
    
    public static WaveManager Instance;
    
    public float ExtraWaveTime;

    public int CurPowerUps;

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
        
        SetCurrentData();
        
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void Start()
    {
        _ui = UIManager.Instance;
        _upgrade = UpgradeManager.Instance;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        StopAllCoroutines();
        
        if (state == GameState.Gameplay)
        {
            StartCoroutine(WaveTimer());
        }
    }

    private IEnumerator WaveTimer()
    {
        while (_curWaveTime > 0)
        {
            _curWaveTime -= Time.deltaTime;

            if (GameManager.Instance.SoulCollected >= _curWaveTarget)
            {
                if (_curWaveId == _waveData.wavesData.Count)
                {
                    GameManager.Instance.UpdateGameState(GameState.Victory);
                    _ui.WinCanvas();
                }
                else
                {
                    GameManager.Instance.UpdateGameState(GameState.Upgrade);
                    _curWaveId += 1;
                }
            }

            yield return null;
        }
        
        if (GameManager.Instance.SoulCollected <= _curWaveTarget)
        {
            GameManager.Instance.UpdateGameState(GameState.Lose);
            _ui.LoseCanvas();
        }
    }

    public void AddExtraTime(float extraTime)
    {
        _curWaveTime += extraTime;
    }

    public void SetCurrentData()
    {
        _curWaveTime = _waveData.wavesData[_curWaveId].time + ExtraWaveTime;
        _curWaveTarget = _waveData.wavesData[_curWaveId].target;
        _curWaveInitialSoul = _waveData.wavesData[_curWaveId].initialSoul;
        _curWaveInterval = _waveData.wavesData[_curWaveId].interval;
        _curWavePureRate = _waveData.wavesData[_curWaveId].pureRate;
        _curWaveMaxSouls = _waveData.wavesData[_curWaveId].maxSouls;
    }
}
