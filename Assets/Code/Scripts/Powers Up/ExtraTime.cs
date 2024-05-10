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

            Debug.Log("Wave Time is Increased");
            Destroy(gameObject);
        }
    }
}
