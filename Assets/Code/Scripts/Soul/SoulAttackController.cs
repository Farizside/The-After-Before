using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulAttackController : MonoBehaviour
{
    // Soul is getting attacked
    void Attack()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSoulGuidance>().AttackSoul(GetComponent<SoulMovementController>());
    }
}
