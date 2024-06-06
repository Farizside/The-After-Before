using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private WaveData _waveData;

    public WaveData WaveData => _waveData;

    public int CurWaveId { get; set; }

    public float CurWaveTime { get; set; }

    public int CurWaveTarget { get; private set; }

    public int CurWaveInitialSoul { get; private set; }

    public float CurWaveInterval { get; private set; }

    public float CurWavePureRate { get; private set; }

    public int CurWaveMaxSouls { get; private set; }

    [SerializeField] private GameObject vfx10Seconds;

    private UIManager _ui;
    private UpgradeManager _upgrade;

    public static WaveManager Instance;

    public float ExtraWaveTime;

    public int CurPowerUps;

    private bool _vfxShown;//VFX

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
        bool _vfxShown = false;

        while (CurWaveTime > 0)
        {
            CurWaveTime -= Time.deltaTime;

            if (CurWaveTime <= 11.0f && !_vfxShown) //vfx
            {
                AudioManager.Instance.PlayTimesUpSFX();
                StartCoroutine(FlashVFX());
                _vfxShown = true;
            }

            if (GameManager.Instance.SoulCollected >= CurWaveTarget)
            {
                if (CurWaveId == _waveData.wavesData.Count)
                {
                    GameManager.Instance.UpdateGameState(GameState.Victory);
                    _ui.WinCanvas();
                }
                else
                {
                    GameManager.Instance.UpdateGameState(GameState.Upgrade);
                    CurWaveId += 1;
                }
            }
            yield return null;
        }

        if (GameManager.Instance.SoulCollected <= CurWaveTarget)
        {
            GameManager.Instance.UpdateGameState(GameState.Lose);
            _ui.LoseCanvas();
        }
    }

    private IEnumerator FlashVFX() //vfx
    {
        float flashDuration = 0.5f;
        int flashCount = 10;

        for (int i = 0; i < flashCount; i++)
        {
            vfx10Seconds.SetActive(true);
            yield return new WaitForSeconds(flashDuration);
            vfx10Seconds.SetActive(false);
            yield return new WaitForSeconds(flashDuration);
        }

        vfx10Seconds.SetActive(true);
    }

    public void AddExtraTime(float extraTime)
    {
        CurWaveTime += extraTime;
    }

    public void SetCurrentData()
    {
        CurWaveTime = _waveData.wavesData[CurWaveId].time + ExtraWaveTime + 6;
        CurWaveTarget = _waveData.wavesData[CurWaveId].target;
        CurWaveInitialSoul = _waveData.wavesData[CurWaveId].initialSoul;
        CurWaveInterval = _waveData.wavesData[CurWaveId].interval;
        CurWavePureRate = _waveData.wavesData[CurWaveId].pureRate;
        CurWaveMaxSouls = _waveData.wavesData[CurWaveId].maxSouls;
    }
}
