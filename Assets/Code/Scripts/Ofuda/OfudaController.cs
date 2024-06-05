using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfudaController : MonoBehaviour
{
    public GameEvent onSubmitSoul;
    public OfudaVFX ofudaVFX;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerSoulGuidance>();
            player.SubmitSoul();
            onSubmitSoul.Raise(this);

            if (ofudaVFX != null)
            {
                ofudaVFX.PlayOfudaVFX();
            }
            else
            {
                Debug.LogError("Referensi ke OfudaVFX belum diatur.");
            }
        }
    }
}
