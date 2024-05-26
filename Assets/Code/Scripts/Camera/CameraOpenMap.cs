using System.Collections;
using UnityEngine;

public class CameraOpenMap : MonoBehaviour
{
    [SerializeField] private GameObject _cameraZoom;

    public void ZoomCamera(float _openMapTimer){
        if(!_cameraZoom.activeSelf){
            _cameraZoom.SetActive(true);

            StartCoroutine(DeactivateAfterDelay(_openMapTimer));
        }
    }

    IEnumerator DeactivateAfterDelay(float delay){
        yield return new WaitForSeconds(delay);

        _cameraZoom.SetActive(false);
    }
}
