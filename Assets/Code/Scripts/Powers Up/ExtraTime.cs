using UnityEngine;

public class ExtraTime : MonoBehaviour
{
    [SerializeField] private float _extraTimeAmount = 10f;
    private WaveManager waveManager;

    private void Start() 
    {
        waveManager = FindObjectOfType<WaveManager>();
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            if (waveManager != null)
            {
                waveManager.AddExtraTime(_extraTimeAmount);
                
            }
            AudioManager.Instance.PlaySound3D("PUTimes", other.transform.position);

            PowerUVFX powerUVFX = GetComponent<PowerUVFX>();
            if (powerUVFX != null)
            {
                powerUVFX.StopPUVFX();
            }

            Debug.Log("Wave Time is Increased");
            Destroy(gameObject);
        }
    }
}
