using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button BtnPause, BtnResume;
    public Canvas PauseUI;
    public Canvas HUD;
    public Canvas Lose;
    public Canvas Win;
    public Canvas Upgrade;
    public Canvas Tutorial1;
    public Canvas Tutorial2;

    public List<Sprite> UpgradeCard;
    public GameObject PrefabCard;

    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _soulText;
    [SerializeField] private TMP_Text _waveText;
    [SerializeField] private List<Button> _upgradeButtons;

    private int _time;
    private int _soul;
    private int _target;
    private int _curWave;
    private int _waveLength;
    
    private WaveManager _wave;
    private GameManager _gm;
    private UpgradeManager _upgrade;
    
    public static UIManager Instance;

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
    
    void Start()
    {
        HUDCanvas();
        _wave = WaveManager.Instance;
        _gm = GameManager.Instance;
        _upgrade = UpgradeManager.Instance;
        BtnPause.onClick.AddListener(_gm.HandlePause);
        BtnResume.onClick.AddListener(_gm.HandleResume);
        
        _waveLength = _wave.WaveData.wavesData.Count;
    }

    private void Update()
    {
        _curWave = _wave.CurWaveId + 1;
        _time = (int)Math.Floor(_wave.CurWaveTime);
        _soul = _gm.SoulCollected;
        _target = _wave.CurWaveTarget;
        ShowUIText();
    }

    public void PauseCanvas()
    {
        HideAllCanvases();
        HUD.enabled = true;
        PauseUI.enabled = true;
    }

    public void HUDCanvas()
    {
        HideAllCanvases();
        HUD.enabled = true;
    }

    public void LoseCanvas()
    {
        HideAllCanvases();
        Lose.enabled = true;
    }

    public void WinCanvas()
    {
        HideAllCanvases();
        Win.enabled = true;
    }

    public void UpgradeCanvas()
    {
        HideAllCanvases();
        HUD.enabled = true;
        Upgrade.enabled = true;
        _upgrade.StartWave();
        for(int i=0; i<UpgradeManager.Instance.UpgradesToChoose.Count; i++)
        {
            _upgradeButtons[i].image.sprite = _upgrade.UpgradesToChoose[i].image;
            // _upgradeButtons[i].onClick.AddListener(delegate{_upgrade.ChooseUpgrade(_upgrade.UpgradesToChoose[i]);});
            UpgradeData upg = _upgrade.UpgradesToChoose[i];
            _upgradeButtons[i].onClick.AddListener(delegate {
                _upgrade.ChooseUpgrade(upg); 
            }) ;
            _upgradeButtons[i].onClick.AddListener(_gm.OnNextWaveClicked);
        }
    }

    private void Tutorial1Canvas()
    {
        HideAllCanvases();
        Tutorial1.enabled = true;
    }

    private void Tutorial2Canvas()
    {
        HideAllCanvases();
        Tutorial2.enabled = true;
    }

    void HideAllCanvases()
    {
        PauseUI.enabled = false;
        HUD.enabled = false;
        Lose.enabled = false;
        Win.enabled = false;
        Upgrade.enabled = false;
        Tutorial1.enabled = false;
        Tutorial2.enabled = false;
    }

    public void ShowUIText()
    {
        _timerText.text = $"{_time}";
        _soulText.text = $"{_soul}/{_target}";
        _waveText.text = $"{_curWave}/{_waveLength}";
    }
}
