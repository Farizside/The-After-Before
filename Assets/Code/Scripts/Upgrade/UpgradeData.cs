using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade/Upgrade Data")]
public class UpgradeData : ScriptableObject
{
    [Header("Common Data")]
    public int id;
    public string name;
    [TextArea(15,20)]
    public string description;
    public List<UpgradeEffect> upgradeEffects;

    public void ResolveEffect()
    {
        foreach(UpgradeEffect effect in upgradeEffects)
        {
            effect.Run();
        }
    }
}
