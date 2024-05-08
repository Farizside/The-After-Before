using UnityEngine;

public class ExtraTime : MonoBehaviour
{
    [SerializeField] private float _extraTimeAmount = 10f;

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            WaveManager waveManager = FindObjectOfType<WaveManager>();
            if (waveManager != null)
            {
                waveManager.AddExtraTime(_extraTimeAmount);
            }

            Debug.Log("Wave Time is Increased");
            Destroy(gameObject);
        }
    }
}
