using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnRate = 5f;
    [SerializeField] private List<GameObject> _soulGOList;
    [SerializeField] private GameEvent _onSoulSpawned;
    public int MaxSouls = 5;
    [SerializeField] private List<GameObject> _souls;
    private bool canSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        MaxSouls = WaveManager.Instance.CurWaveMaxSouls;
        _spawnRate = WaveManager.Instance.CurWaveInterval;
        for (int i = 0; i < MaxSouls; i++)
        {
            GameObject soulGO = _soulGOList[Random.Range(0, _soulGOList.Count)];
            GameObject soul = Instantiate(soulGO, transform.position, Quaternion.identity);
            _souls.Add(soul);
            soul.SetActive(false);
        }
        int initialSoul = WaveManager.Instance.CurWaveInitialSoul;
        for(int i=0; i<initialSoul; i++)
        {
            Spawn();
        }
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
                soul.transform.position = transform.position;
                soul.GetComponent<SoulTypeController>().SoulType = SoulType.LOST;
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

    public void onSubmitSoul(Component sender, object data)
    {
        canSpawn = true;
    }
}
