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
        }
    }

    public void StopHomeVFX()
    {
        if (spawnedVFX != null)
        {
            Destroy(spawnedVFX);
            spawnedVFX = null;
        }
    }
}

