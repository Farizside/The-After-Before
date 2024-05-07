using UnityEngine;

public class ExtraTime : MonoBehaviour
{
    [SerializeField] private float extraTimeAmount = 10f;

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            Spawner spawner = FindObjectOfType<Spawner>();
            
            WaveManager waveManager = FindObjectOfType<WaveManager>(); // Mencari objek WaveManager
            
            if (waveManager != null)
            {
                waveManager.AddExtraTime(extraTimeAmount); // Memanggil fungsi untuk menambah waktu ekstra
            }

            Debug.Log("Wave Time is Increased");
            spawner.ObjectHitByPlayer();
        }
    }
}
