using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject BtnPause, BtnResume;
    public GameObject PauseUI;
    public GameObject HUD;
    public GameObject Lose;
    public GameObject Win;
    public GameObject Upgrade;

    public GameObject LoseButton;
    public GameObject WinButton;

    public List<Sprite> UpgradeCard;
    public GameObject PrefabCard;

    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _soulText;
    [SerializeField] private TMP_Text _waveText;
    [SerializeField] private List<GameObject> _upgradeButtons;

    private int _time;
    private int _soul;
    private int _target;
    private int _curWave;
    private int _waveLength;
    
    private WaveManager _wave;
    private GameManager _gm;
    private UpgradeManager _upgrade;
    private EventSystem _eventSystem;
    
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
        
        if (_eventSystem == null)
        {
            _eventSystem = FindObjectOfType<EventSystem>();
            DontDestroyOnLoad(_eventSystem);
        }
        else
        {
            Destroy(_eventSystem);
        }
    }
    
    void Start()
    {
        HUDCanvas();
        _wave = WaveManager.Instance;
        _gm = GameManager.Instance;
        _upgrade = UpgradeManager.Instance;
        BtnPause.GetComponent<Button>().onClick.AddListener(_gm.HandlePause);
        BtnResume.GetComponent<Button>().onClick.AddListener(_gm.HandleResume);
        
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
        HUD.SetActive(true);
        PauseUI.SetActive(true);
        _eventSystem.SetSelectedGameObject(BtnResume);
    }

    public void HUDCanvas()
    {
        HideAllCanvases();
        HUD.SetActive(true);
    }

    public void LoseCanvas()
    {
        HideAllCanvases();
        Lose.SetActive(true);
        _eventSystem.SetSelectedGameObject(LoseButton);
    }

    public void WinCanvas()
    {
        HideAllCanvases();
        Win.SetActive(true);
        _eventSystem.SetSelectedGameObject(WinButton);
    }

    public void UpgradeCanvas()
    {
        HideAllCanvases();
        HUD.SetActive(true);
        Upgrade.SetActive(true);
        _upgrade.SetUpgradeOptions(3);
        for(int i=0; i< _upgrade.UpgradesToChoose.Count; i++)
        {
            _upgradeButtons[i].GetComponent<Button>().image.sprite = _upgrade.UpgradesToChoose[i].image;
            // _upgradeButtons[i].onClick.AddListener(delegate{_upgrade.ChooseUpgrade(_upgrade.UpgradesToChoose[i]);});
            UpgradeData upg = _upgrade.UpgradesToChoose[i];
            _upgradeButtons[i].GetComponent<Button>().onClick.AddListener(delegate {
                _upgrade.ChooseUpgrade(upg); 
            }) ;
            _upgradeButtons[i].GetComponent<Button>().onClick.AddListener(_gm.OnNextWaveClicked);
        }
        _eventSystem.SetSelectedGameObject(_upgradeButtons[0]);
    }

    public void HideAllCanvases()
    {
        PauseUI.SetActive(false);
        HUD.SetActive(false);
        Lose.SetActive(false);
        Win.SetActive(false);
        Upgrade.SetActive(false);
    }

    public void ShowUIText()
    {
        _timerText.text = $"{_time}";
        _soulText.text = $"{_soul}/{_target}";
        _waveText.text = $"{_curWave}/{_waveLength}";
    }
}
