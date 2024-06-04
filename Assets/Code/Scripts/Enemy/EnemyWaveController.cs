using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;

    private WaveManager _waveManager;
    void Start()
    {
        _waveManager = FindObjectOfType<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_waveManager.CurWaveId >= 1 &&_waveManager.CurWaveId <=15)
        {
            enemy1.SetActive(true);
            // enemy2.SetActive(false);
            // enemy3.SetActive(false);
        }

        if (_waveManager.CurWaveId >= 4 && _waveManager.CurWaveId <= 15 )
        {
            enemy2.SetActive(true);
            // enemy3.SetActive(false);
        }
        else
        {
            enemy2.SetActive(false);
            // enemy3.SetActive(true);
        }

         if (_waveManager.CurWaveId >= 9 && _waveManager.CurWaveId <= 15 )
        {
            // enemy2.SetActive(true);
            enemy3.SetActive(true);
        }
        else
        {
            // enemy2.SetActive(false);
            enemy3.SetActive(false);
        }
        

    }
}
