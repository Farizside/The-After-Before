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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetObject.SetActive(!targetObject.activeSelf);

            if (_homeVFX != null)
            {
                _homeVFX.StopHomeVFX();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetObject.SetActive(!targetObject.activeSelf);

            if (_homeVFX != null)
            {
                _homeVFX.PlayHomeVFX();
            }
        }
    }

    private void HomeExternal()
    {
        Home.SetActive(true);
        if (_homeVFX != null)
        {
            _homeVFX.PlayHomeVFX();
        }
    }
}
