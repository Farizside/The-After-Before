using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _powersUpPrefabs;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _curPowersUp;

    private List<int> _selectedSpawnPointIndexes = new List<int>();

    private void Start()
    {
        InitializeSelectedSpawnPoints();
        //SpawnPowersUpForWave();
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.Gameplay)
        {
            SpawnPowersUpForWave();
        }
    }

    private void InitializeSelectedSpawnPoints()
    {
        _selectedSpawnPointIndexes.Clear();
    }

    private void SpawnPowersUpForWave()
    {
        for (int i = 0; i < _curPowersUp; i++)
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

            Instantiate(powerUpPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
