using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfudaController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerSoulGuidance>();
            player.SubmitSoul();
        }
    }
}
