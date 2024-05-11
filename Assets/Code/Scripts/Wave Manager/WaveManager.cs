using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private WaveData _waveData;

    private int _curWaveId = 0;
    private float _curWaveTime = 30f;
    private int _curWaveTarget;
    private int _curWaveInitialSoul;
    private float _curWaveInterval;
    private float _curWavePureRate;
    private int _curWaveMaxSouls;

    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        //StartCoroutine(WaveTimer());
    }

    private void Update() 
    {
        //Debug.Log(_curWaveTime);    
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        StopCoroutine(WaveTimer());
        if (state == GameState.Gameplay)
        {
            _curWaveTime = _waveData.wavesData[_curWaveId].time;
            _curWaveTarget = _waveData.wavesData[_curWaveId].target;
            _curWaveInitialSoul = _waveData.wavesData[_curWaveId].initialSoul;
            _curWaveInterval = _waveData.wavesData[_curWaveId].interval;
            _curWavePureRate = _waveData.wavesData[_curWaveId].pureRate;
            _curWaveMaxSouls = _waveData.wavesData[_curWaveId].maxSouls;

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
        
        if (GameManager.Instance.SoulCollected >= _curWaveTarget)
        {
            if (_curWaveId == _waveData.wavesData.Count)
            {
                GameManager.Instance.UpdateGameState(GameState.Victory);
            }
            else
            {
                GameManager.Instance.UpdateGameState(GameState.Upgrade);
                _curWaveId += 1;
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
}
