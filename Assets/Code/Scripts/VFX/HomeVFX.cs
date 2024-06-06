using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeVFX : MonoBehaviour
{
    public GameObject VFXHomePrefab;
    public Transform VFXHomePosition;

    private GameObject spawnedVFX;

    public void PlayHomeVFX()
    {
        if (VFXHomePrefab != null && VFXHomePosition != null)
        {
            spawnedVFX = Instantiate(VFXHomePrefab, VFXHomePosition.position, VFXHomePosition.rotation);
            spawnedVFX.transform.parent = VFXHomePosition;
            Debug.Log("VFX spawned at: " + VFXHomePosition.position);
        }
        else
        {
            Debug.LogError("VFX Prefab atau VFX Position tidak diatur dengan benar.");
        }
    }

    public void StopHomeVFX()
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

