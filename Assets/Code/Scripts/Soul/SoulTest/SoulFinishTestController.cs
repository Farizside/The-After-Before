using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulFinishTestController : MonoBehaviour
{
    public GameEvent onSoulFinish;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            onSoulFinish.Raise(this);
        }
    }
}
