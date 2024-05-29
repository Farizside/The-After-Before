using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovementTarget : MonoBehaviour
{
    public float moveSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
            transform.eulerAngles = new Vector3(0, 0, 0); 
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * Time.deltaTime * moveSpeed;
            transform.eulerAngles = new Vector3(0, 180, 0); 
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * Time.deltaTime * moveSpeed;
            transform.eulerAngles = new Vector3(0, -90, 0); 
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * Time.deltaTime * moveSpeed;
            transform.eulerAngles = new Vector3(0, 90, 0); 
        }
    }
}
