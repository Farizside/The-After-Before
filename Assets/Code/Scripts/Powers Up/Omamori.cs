using UnityEngine;

public class Omamori : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            Spawner spawner = FindObjectOfType<Spawner>();

            EnemyAIMovement enemyAIMovement = FindObjectOfType<EnemyAIMovement>();
            //enemyAIMovement.Stunned(true, 5f);

            Debug.Log("Enemy Stunned");
            spawner.ObjectHitByPlayer();
        }
    }
}
