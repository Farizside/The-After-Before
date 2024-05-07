using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] PowerUp;
    [SerializeField] private GameObject[] point;
    [SerializeField] private float time = 5f;
    private GameObject spawnedObject;
    
    private void Start(){
        StartCoroutine(SpawnObjectWithDelay());
    }

    private IEnumerator SpawnObjectWithDelay()
    {
        while (true){
            yield return new WaitForSeconds(time);
            if (spawnedObject == null){
                int randomNumber = Random.Range(0, PowerUp.Length);
                int randomIndex = Random.Range(0, point.Length);
                GameObject randomPoint = point[randomIndex];
                GameObject randomPowerUp = PowerUp[randomNumber];

                spawnedObject = Instantiate(randomPowerUp, randomPoint.transform.position, Quaternion.identity);
            }
        }
    }

    public void ObjectHitByPlayer(){
        Destroy(spawnedObject);

        StartCoroutine(SpawnObjectWithDelay());
    }
}
