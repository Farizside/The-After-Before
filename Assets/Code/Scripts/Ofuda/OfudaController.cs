using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfudaController : MonoBehaviour
{
    public GameEvent onSubmitSoul;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerSoulGuidance>();
            player.SubmitSoul();
            onSubmitSoul.Raise(this);
        }
    }
}
