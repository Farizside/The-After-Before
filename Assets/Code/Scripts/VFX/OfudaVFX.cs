using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfudaVFX : MonoBehaviour
{
    public GameObject VFXOfudaPrefab;
    public Transform VFXOfudaPosition;

    private GameObject spawnedVFX;

    public void PlayOfudaVFX()
    {
        if (VFXOfudaPrefab != null && VFXOfudaPosition != null)
        {
            spawnedVFX = Instantiate(VFXOfudaPrefab, VFXOfudaPosition.position, VFXOfudaPosition.rotation);
            spawnedVFX.transform.parent = VFXOfudaPosition;
            Debug.Log("VFX spawned at: " + VFXOfudaPosition.position);
        }
        else
        {
            Debug.LogError("VFX Prefab atau VFX Position tidak diatur dengan benar.");
        }
    }
}
