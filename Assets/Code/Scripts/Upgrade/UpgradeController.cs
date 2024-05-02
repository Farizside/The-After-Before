using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    public Upgrade upgrade; 
    
    void Choose()
    {
        upgrade.Choose();
    }
}
