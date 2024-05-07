using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _powerUp;
    [SerializeField] private GameObject[] _point;
    [SerializeField] private float _time = 5f;
    private GameObject _spawnedObject;
    
    private void Start(){
        StartCoroutine(SpawnObjectWithDelay());
    }

    private IEnumerator SpawnObjectWithDelay()
    {
        while (true){
            yield return new WaitForSeconds(_time);
            if (_spawnedObject == null){
                int randomNumber = Random.Range(0, _powerUp.Length);
                int randomIndex = Random.Range(0, _point.Length);
                GameObject randomPoint = _point[randomIndex];
                GameObject randomPowerUp = _powerUp[randomNumber];

                _spawnedObject = Instantiate(randomPowerUp, randomPoint.transform.position, Quaternion.identity);
            }
        }
    }

    public void ObjectHitByPlayer(){
        Destroy(_spawnedObject);

        StartCoroutine(SpawnObjectWithDelay());
    }
}
