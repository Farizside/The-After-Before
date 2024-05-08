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
    }
    public List<UpgradeData> Upgrades;
    public readonly List<int> UpgradeLevel;
    public readonly List<int> Upgradeable;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if(UpgradeLevel.Count == 0)
        {
            for(int i=0; i<Upgrades.Count; i++)
            {
                UpgradeLevel.Add(0);
                Upgradeable.Add(i);
            }
        }
    }

    public List<int> GetOptionsIndex(int numberOfOptions)
    {
        List<int> optionsIndex = new();
        for(int i=0; i<numberOfOptions; i++)
        {
            int idx = Random.Range(0, Upgradeable.Count);
            optionsIndex.Add(Upgradeable[idx]);
        }
        return optionsIndex;
    }

    public void ChooseUpgrade(int upgradeIndex)
    {
        UpgradeData upgrade = Upgrades[upgradeIndex];
        
        List<UpgradeEffect> effects = upgrade.upgradeEffects;
        foreach(UpgradeEffect effect in effects)
        {
            switch (effect.EffectType)
            {
                case UpgradeEffectType.AddWaveTime:
                    // Nambah Wave Time saat ini
                    break;
                case UpgradeEffectType.IncreasePlayerSpeed:

                    break;
                case UpgradeEffectType.IncreasePlayerLightRadius:
                    break;
                case UpgradeEffectType.DecreasePlayerSlowedSpeed:
                    break;
                case UpgradeEffectType.AddPowerUps:
                    break;
                default:
                    break;
            }
        }

        UpgradeLevel[upgradeIndex]++;
        if (UpgradeLevel[upgradeIndex] >= MaxUpgrade)
        {
            Upgradeable.Remove(upgradeIndex);
        }

    }
    
}
