using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public int MaxUpgrade = 6;
    private static UpgradeManager _instance;
    public static UpgradeManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindAnyObjectByType<UpgradeManager>();
            }

            if(_instance == null)
            {
                GameObject go = new GameObject("Upgrade Manager");
                go.AddComponent<UpgradeManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }
    public List<UpgradeData> Upgrades;
    public List<int> UpgradeLevel;
    private List<int> _upgradeable;

    [Header("Upgrades To Choose")]
    public List<UpgradeData> UpgradesToChoose;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        UpgradeLevel = new();
        _upgradeable = new();

        if(UpgradeLevel.Count == 0)
        {
            for(int i=0; i<Upgrades.Count; i++)
            {
                UpgradeLevel.Add(0);
                _upgradeable.Add(i);
            }
        }
    }

    public void SetUpgradeOptions(int numberOfOptions)
    {
        UpgradesToChoose = new();
        int n = Mathf.Min(numberOfOptions, _upgradeable.Count);
        List<int> possibleRandomIndex = new List<int>(_upgradeable);
        for (int i = 0; i < n; i++)
        {
            int idx = Random.Range(0, possibleRandomIndex.Count);
            UpgradesToChoose.Add(Upgrades[possibleRandomIndex[idx]]);
            possibleRandomIndex.RemoveAt(idx);
        }
    }

    public void ChooseUpgrade(UpgradeData upgradeData)
    {
        int idx = Upgrades.IndexOf(upgradeData);
        if (UpgradeLevel[idx] >= MaxUpgrade) return;
        
        foreach(UpgradeEffect effect in upgradeData.upgradeEffects)
        {
            switch (effect.EffectType)
            {
                case UpgradeEffectType.AddWaveTime:
                    Debug.Log("Add Wave Time");
                    break;
                case UpgradeEffectType.IncreasePlayerSpeed:
                    Debug.Log("Increase Player Speed");
                    break;
                case UpgradeEffectType.IncreasePlayerLightRadius:
                    Debug.Log("Increase Player Light Radius");
                    break;
                case UpgradeEffectType.DecreasePlayerSlowedSpeed:
                    Debug.Log("Decrease Player Slowed Speed");
                    break;
                case UpgradeEffectType.AddPowerUps:
                    Debug.Log("Add Power Ups");
                    break;
                default:
                    break;
            }
        }
        UpgradeLevel[idx]++;

        if (UpgradeLevel[idx] >= MaxUpgrade)
        {
            _upgradeable.Remove(idx);
        }
    }

    [ContextMenu("Test Get 3 Upgrades")]
    public void Get3Upgrades()
    {
        SetUpgradeOptions(3);
    }

    [ContextMenu("Test Get first Upgrade")]
    public void GetFirstUpgrade()
    {
        ChooseUpgrade(UpgradesToChoose[0]);
    }
}
