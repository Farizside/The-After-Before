using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnRate = 5f;
    [SerializeField] private GameObject _soulGO;
    [SerializeField] private GameEvent _onSoulSpawned;
    public int MaxSouls = 5;
    [SerializeField] private List<GameObject> _souls;
    private bool canSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < MaxSouls; i++)
        {
            GameObject soul = Instantiate(_soulGO, transform.position, Quaternion.identity);
            _souls.Add(soul);
            soul.SetActive(false);
        }
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpawn) StartCoroutine(Spawner());
    }

    void Spawn()
    {
        foreach(GameObject soul in _souls)
        {
            if (!soul.activeSelf)
            {
                soul.SetActive(true);
                _onSoulSpawned.Raise(this, soul);
                return;
            }
        }
        canSpawn = false;
    }

    private IEnumerator Spawner()
    {
        canSpawn = false;
        yield return new WaitForSeconds(_spawnRate);
        canSpawn = true;
        Spawn();
    }
}
