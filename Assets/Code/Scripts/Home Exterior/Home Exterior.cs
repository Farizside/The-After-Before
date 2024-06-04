using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeExterior : MonoBehaviour
{
    public GameObject targetObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetObject.SetActive(!targetObject.activeSelf); // Toggle status aktif
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            targetObject.SetActive(!targetObject.activeSelf); // Toggle status aktif
    }
}
