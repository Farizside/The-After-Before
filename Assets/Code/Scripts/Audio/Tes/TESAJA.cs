using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody rb;
    private AudioManager audioManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioManager = AudioManager.Instance;
    }

    void FixedUpdate()
    {
       
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

       
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * moveSpeed);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Soul"))
        {
            if (audioManager != null)
            {
                audioManager.PlaySound3D("Attract", other.transform.position);
            }
            else
            {
                Debug.LogWarning("AudioManager instance is not assigned to PlayerController!");
            }
        }
        else if (other.gameObject.CompareTag("Ofuda Room"))
        {
            if (audioManager != null)
            {
                audioManager.PlaySound3D("Submit", other.transform.position);
            }
            else
            {
                Debug.LogWarning("AudioManager instance is not assigned to PlayerController!");
            }
        }
    }
}
