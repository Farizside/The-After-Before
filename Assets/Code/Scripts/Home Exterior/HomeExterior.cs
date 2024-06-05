using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeExterior : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject Home;
    // Start is called before the first frame update
    void Start()
    {
        HomeExternal();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetObject.SetActive(!targetObject.activeSelf); 
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            targetObject.SetActive(!targetObject.activeSelf); 
    }

     private void HomeExternal()
    {
        Home.SetActive(true);
    }
}
