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
            DontDestroyOnLoad(gameObject);
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

    public bool ChooseUpgrade(UpgradeData upgradeData)
    {
        int idx = Upgrades.IndexOf(upgradeData);
        if (UpgradeLevel[idx] >= MaxUpgrade) return false;
        
        UpgradeLevel[idx]++;

        if (UpgradeLevel[idx] >= MaxUpgrade)
        {
            _upgradeable.Remove(idx);
        }
        return true;
    }

    [ContextMenu("Test Get 3 Upgrades")]
    public void Get3Upgrades()
    {
        SetUpgradeOptions(3);
    }

    [ContextMenu("Test Get first Upgrade")]
    public void GetFirstUpgrade()
    {
        if(ChooseUpgrade(UpgradesToChoose[0])) UpgradesToChoose[0].ResolveEffect();
    }
}
