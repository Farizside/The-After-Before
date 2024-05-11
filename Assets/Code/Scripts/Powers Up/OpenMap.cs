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

            Debug.Log("Enlighten all of the map in 2 secs");
            Destroy(gameObject);
        }
    }
}
