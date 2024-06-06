using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _powersUpPrefabs;
    [SerializeField] private Transform[] _spawnPoints;
    private WaveManager _waveManager;

    private List<int> _selectedSpawnPointIndexes = new List<int>();

    private void Awake() {
        _waveManager = FindObjectOfType<WaveManager>();
    }

    private void Start() 
    {
        if (_waveManager.CurWaveId >= 5)
        {
            InitializeSelectedSpawnPoints();
            SpawnPowersUpForWave();
        }
    }

    private void InitializeSelectedSpawnPoints()
    {
        _selectedSpawnPointIndexes.Clear();
    }

    private void SpawnPowersUpForWave()
    {
        for (int i = 0; i < _waveManager.CurPowerUps; i++)
        {
            int randomIndex;

            if (_selectedSpawnPointIndexes.Count == _spawnPoints.Length)
            {
                Debug.Log("All spawn points used!");
                return;
            }

            do
            {
                randomIndex = Random.Range(0, _spawnPoints.Length);
            } while (_selectedSpawnPointIndexes.Contains(randomIndex));

            _selectedSpawnPointIndexes.Add(randomIndex);

            GameObject powerUpPrefab = _powersUpPrefabs[Random.Range(0, _powersUpPrefabs.Length)];
            Transform spawnPoint = _spawnPoints[randomIndex];

            GameObject spawnedPowerUp = Instantiate(powerUpPrefab, spawnPoint.position, Quaternion.identity);

            PowerUVFX powerUVFX = spawnedPowerUp.GetComponent<PowerUVFX>();
            if (powerUVFX != null)
            {
                powerUVFX.PlayPUVFX();
            }
        }
    }
}
