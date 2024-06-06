using UnityEngine;

public class Omamori : MonoBehaviour
{
    [SerializeField] private float _stunTime;
    private EnemyWaveController enemyAIMovement;

    private void Start() 
    {
        enemyAIMovement = FindObjectOfType<EnemyWaveController>();
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            enemyAIMovement.StunAllEnemies(_stunTime);
            Destroy(gameObject);
        }
    }
}