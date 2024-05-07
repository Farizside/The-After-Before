using System.Collections;
using UnityEngine;

public class CameraOpenMap : MonoBehaviour
{
    [SerializeField] private GameObject cameraZoom;

    public void ZoomCamera(){
        if(!cameraZoom.activeSelf){
            cameraZoom.SetActive(true);

            StartCoroutine(DeactivateAfterDelay(2f));
        }
    }

    IEnumerator DeactivateAfterDelay(float delay){
        yield return new WaitForSeconds(delay);

        cameraZoom.SetActive(false);
    }
}
