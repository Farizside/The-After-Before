using UnityEngine;

public class Omamori : MonoBehaviour
{
    [SerializeField] private float _stunTime;
    private EnemyAIMovement enemyAIMovement;

    private void Start() 
    {
        enemyAIMovement = FindObjectOfType<EnemyAIMovement>();
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            //enemyAIMovement.Stunned(true, _stunTime);

            Debug.Log("Enemy Stunned");
            Destroy(gameObject);
        }
    }
}
