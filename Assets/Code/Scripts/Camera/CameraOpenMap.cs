using System.Collections;
using UnityEngine;

public class CameraOpenMap : MonoBehaviour
{
    [SerializeField] private GameObject _cameraZoom;

    public void ZoomCamera(){
        if(!_cameraZoom.activeSelf){
            _cameraZoom.SetActive(true);

            StartCoroutine(DeactivateAfterDelay(2f));
        }
    }

    IEnumerator DeactivateAfterDelay(float delay){
        yield return new WaitForSeconds(delay);

        _cameraZoom.SetActive(false);
    }
}
