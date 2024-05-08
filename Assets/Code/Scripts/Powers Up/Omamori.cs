using UnityEngine;

public class Omamori : MonoBehaviour
{
    [SerializeField] private float _stunTime = 5f;

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            EnemyAIMovement enemyAIMovement = FindObjectOfType<EnemyAIMovement>();
            //enemyAIMovement.Stunned(true, _stunTime);

            Debug.Log("Enemy Stunned");
            Destroy(gameObject);
        }
    }
}
