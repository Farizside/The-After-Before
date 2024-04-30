using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Kecepatan gerakan kapsul
    public AudioClip collisionSound; // Suara yang akan dimainkan saat kapsul menabrak

    private Rigidbody rb;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        // Mendapatkan input keyboard untuk menggerakkan kapsul
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Menggerakkan kapsul sesuai dengan input
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * moveSpeed);
    }

    // Ketika kapsul menabrak objek lain
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Soul")) // Cek apakah objek yang ditabrak adalah kotak
        {
            // Memainkan suara ketika kapsul menabrak
            if (collisionSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(collisionSound);
            }
        }
    }
}
