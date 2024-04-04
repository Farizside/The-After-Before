using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnRate = 5f;
    [SerializeField] private GameObject _soulGO;
    [SerializeField] private GameEvent _onSoulSpawned;
    private bool canSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        Spawn(1);
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpawn) StartCoroutine(Spawner());
    }

    void Spawn(int numberOfSoulsSpawned)
    {
        for(int i=0; i<numberOfSoulsSpawned; i++)
        {
            GameObject soul = Instantiate(_soulGO, transform.position, Quaternion.identity);
            _onSoulSpawned.Raise(this, soul);
        }
    }

    private IEnumerator Spawner()
    {
        canSpawn = false;
        yield return new WaitForSeconds(_spawnRate);
        canSpawn = true;
        Spawn(1);
    }
}
