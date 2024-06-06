using UnityEngine;

public class OpenMap : MonoBehaviour
{
    [SerializeField] private float _openMapTimer;
    private CameraOpenMap cameraOpenMap;

    private void Start() 
    {
        cameraOpenMap = FindObjectOfType<CameraOpenMap>();
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            cameraOpenMap.ZoomCamera(_openMapTimer);
            AudioManager.Instance.PlaySound3D("PUOpenMap", other.transform.position);
            PowerUVFX powerUVFX = GetComponent<PowerUVFX>();
            if (powerUVFX != null)
            {
                powerUVFX.StopPUVFX();
            }

            Debug.Log("Enlighten all of the map in 2 secs");
            Destroy(gameObject);
        }
    }
}
