using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeExterior : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject Home;

    [SerializeField] private HomeVFX _homeVFX;

    void Start()
    {
        HomeExternal();
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger");
            targetObject.SetActive(!targetObject.activeSelf);

            if (_homeVFX != null)
            {
                Debug.Log("Calling StopHomeVFX");
                _homeVFX.StopHomeVFX();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger");
            targetObject.SetActive(!targetObject.activeSelf);

            if (_homeVFX != null)
            {
                Debug.Log("Calling PlayHomeVFX");
                _homeVFX.PlayHomeVFX();
            }
        }
    }

    private void HomeExternal()
    {
        Home.SetActive(true);
        if (_homeVFX != null)
        {
            Debug.Log("Calling PlayHomeVFX");
            _homeVFX.PlayHomeVFX();
        }
    }
}
