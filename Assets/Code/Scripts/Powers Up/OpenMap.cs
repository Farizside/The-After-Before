using UnityEngine;

public class OpenMap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            CameraOpenMap cameraOpenMap = FindObjectOfType<CameraOpenMap>();
            cameraOpenMap.ZoomCamera();

            Debug.Log("Enlighten all of the map in 2 secs");
            Destroy(gameObject);
        }
    }
}
