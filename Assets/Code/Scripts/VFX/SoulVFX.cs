using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulVFX : MonoBehaviour
{
    public GameObject VFXPrefab;
    public Transform VFXPosition;

    private GameObject spawnedVFX;

    public void PlayVFX()
    {
        if (VFXPrefab != null && VFXPosition != null)
        {
            spawnedVFX = Instantiate(VFXPrefab, VFXPosition.position, VFXPosition.rotation);
            spawnedVFX.transform.parent = VFXPosition;
            Debug.Log("VFX spawned at: " + VFXPosition.position);
        }
        else
        {
            Debug.LogError("VFX Prefab atau VFX Position tidak diatur dengan benar.");
        }
    }

    public void StopVFX()
    {
        if (spawnedVFX != null)
        {
            Destroy(spawnedVFX);
            spawnedVFX = null;
            Debug.Log("VFX stopped.");
        }
        else
        {
            Debug.LogWarning("No VFX currently playing to stop.");
        }
    }
}