using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfudaVFX : MonoBehaviour
{
    public GameObject VFXPrefab;
    public Transform VFXPosition;

    public void PlayVFX()
    {
        if (VFXPrefab != null && VFXPosition != null)
        {
            Instantiate(VFXPrefab, VFXPosition.position, VFXPosition.rotation);
            Debug.Log("VFX spawned at: " + VFXPosition.position);
        }
        else
        {
            Debug.LogError("VFX Prefab atau VFX Position tidak diatur dengan benar.");
        }
    }
}
