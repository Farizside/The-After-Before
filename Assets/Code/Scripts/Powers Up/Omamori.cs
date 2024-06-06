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
            enemyAIMovement.Stunned(true, _stunTime);
            AudioManager.Instance.PlaySound3D("PUStun", other.transform.position);

            PowerUVFX powerUVFX = GetComponent<PowerUVFX>();
            if (powerUVFX != null)
            {
                powerUVFX.StopPUVFX();
            }

            Debug.Log("Enemy Stunned");
            Destroy(gameObject);
        }
    }
}
