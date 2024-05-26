using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStun : MonoBehaviour
{
    // private bool _isActive = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyAIMovement>().Stunned(true, 5.0f);
            StartCoroutine(DisableAfterDelay(5.0f));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyAIMovement>().Stunned(false, 0f);
        }
    }

    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
        // _collider.enabled = false;
    }
}
