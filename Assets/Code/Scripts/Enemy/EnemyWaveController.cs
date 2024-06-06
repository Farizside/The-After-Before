using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour
{
    public GameObject[] enemies;

    private WaveManager _waveManager;

    void Start()
    {
        _waveManager = FindObjectOfType<WaveManager>();
    }

    void Update()
    {
        if (_waveManager.CurWaveId >= 1 && _waveManager.CurWaveId <= 15)
        {
            enemies[0].SetActive(true);
        }
        else
        {
            enemies[0].SetActive(false);
        }

        if (_waveManager.CurWaveId >= 4 && _waveManager.CurWaveId <= 15)
        {
            enemies[1].SetActive(true);
        }
        else
        {
            enemies[1].SetActive(false);
        }

        if (_waveManager.CurWaveId >= 9 && _waveManager.CurWaveId <= 15)
        {
            enemies[2].SetActive(true);
        }
        else
        {
            enemies[2].SetActive(false);
        }
    }

    public void StunAllEnemies(float stunDuration)
    {
        foreach (GameObject enemy in enemies)
        {
            EnemyAIMovement enemyAI = enemy.GetComponent<EnemyAIMovement>();
            if (enemyAI != null)
            {
                enemyAI.Stunned(true, stunDuration);
            }
        }
    }
}