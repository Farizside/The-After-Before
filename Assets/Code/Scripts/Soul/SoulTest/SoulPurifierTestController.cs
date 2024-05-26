using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulPurifierTestController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Soul")
        {
            other.GetComponent<SoulTypeController>().SoulType = SoulType.PURE;
        }
    }
}
