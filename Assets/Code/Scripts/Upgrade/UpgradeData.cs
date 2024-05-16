using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Upgrade/Upgrade Data")]
public class UpgradeData : ScriptableObject
{
    [Header("Common Data")]
    public int id;
    public Sprite image;
    public string name;
    [TextArea(15,20)]
    public string description;
    public List<UpgradeEffect> upgradeEffects;
}
