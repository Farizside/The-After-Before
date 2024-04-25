using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurificationController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Soul"))
        {
            if (other.GetComponent<SoulMovementController>().IsAttracted)
            {
                other.GetComponent<SoulTypeController>().SoulType = SoulType.PURE;
            }
        }
    }
}
