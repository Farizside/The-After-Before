using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum UpgradeEffectType
{
    AddWaveTime,
    IncreasePlayerSpeed,
    IncreasePlayerLightRadius,
    DecreasePlayerSlowedSpeed,
    AddPowerUps
}

[CreateAssetMenu(menuName = "Upgrade/Upgrade Effect")]
public class UpgradeEffect : ScriptableObject
{
    public UpgradeEffectType EffectType;
    public float value;

    public void Run()
    {
        switch (EffectType)
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
        Debug.Log($"The value : {value}");
    }
}
