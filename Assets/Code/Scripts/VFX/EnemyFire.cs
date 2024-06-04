using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject FirePrefab;
    public Transform FirePosition;

    private GameObject _fireObject;

    void Start()
    {
        if (FirePosition == null)
        {
            Debug.LogError("Empty object untuk VFX tidak ditemukan!");
            return;
        }

        _fireObject = Instantiate(FirePrefab, FirePosition.position, FirePosition.rotation);
        _fireObject.transform.parent = FirePosition;
    }

    void FixedUpdate()
    {
        // Debug.Log("Character Position: " + transform.position);

        if (_fireObject != null)
        {
            // Debug.Log("VFX Position: " + _fireObject.transform.position);

            // Modify particle system settings to limit initial vertical movement
            ParticleSystem.MainModule mainModule = _fireObject.GetComponent<ParticleSystem>().main;
            mainModule.startSpeed = new ParticleSystem.MinMaxCurve(0f, mainModule.startSpeed.constantMax);

            // Modify velocity over lifetime settings to limit vertical movement
            ParticleSystem.VelocityOverLifetimeModule velocityModule = _fireObject.GetComponent<ParticleSystem>().velocityOverLifetime;
            velocityModule.y = new ParticleSystem.MinMaxCurve(0f, 0f); // Set both min and max y velocity to 0
        }
    }
}
