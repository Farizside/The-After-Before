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
}
