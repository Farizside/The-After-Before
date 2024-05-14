using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private WaveData _waveData;

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

    public static WaveManager Instance;

    public float ExtraWaveTime;
    
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
        
        SetCurrentData();
        
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.Upgrade)
        {
            SetCurrentData();
        }
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
            yield return null;
        }
        
        if (GameManager.Instance.SoulCollected <= _curWaveTarget)
        {
            if (_curWaveId == _waveData.wavesData.Count)
            {
                GameManager.Instance.UpdateGameState(GameState.Victory);
            }
            else
            {
                GameManager.Instance.UpdateGameState(GameState.Upgrade);
                _curWaveId += 1;
                SetCurrentData();
            }
        }
        else
        {
            GameManager.Instance.UpdateGameState(GameState.Lose);
        }
    }

    public void AddExtraTime(float extraTime)
    {
        _curWaveTime += extraTime;
    }

    private void SetCurrentData()
    {
        _curWaveTime = _waveData.wavesData[_curWaveId].time + ExtraWaveTime;
        _curWaveTarget = _waveData.wavesData[_curWaveId].target;
        _curWaveInitialSoul = _waveData.wavesData[_curWaveId].initialSoul;
        _curWaveInterval = _waveData.wavesData[_curWaveId].interval;
        _curWavePureRate = _waveData.wavesData[_curWaveId].pureRate;
        _curWaveMaxSouls = _waveData.wavesData[_curWaveId].maxSouls;
    }
}
