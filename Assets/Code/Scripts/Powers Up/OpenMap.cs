using UnityEngine;

public class OpenMap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            Spawner spawner = FindObjectOfType<Spawner>();

            CameraOpenMap cameraOpenMap = FindObjectOfType<CameraOpenMap>();
            cameraOpenMap.ZoomCamera();

            Debug.Log("Enlighten all of the map in 2 secs");
            spawner.ObjectHitByPlayer();
        }
    }
}
