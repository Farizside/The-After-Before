using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    private UpgradeManager _upgrade;
    private WaveManager _wave;
    void Start()
    {
        _upgrade = UpgradeManager.Instance;
        
        _upgrade.StartWave();
    }
}
