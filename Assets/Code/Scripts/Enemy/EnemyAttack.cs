using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private bool _isAttacking;

    void Start ()
    {
        _isAttacking = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Soul") )
        {
            if (other.GetComponent<SoulMovementController>().IsAttracted)
            {  
                // other.GetComponent<SoulTypeController>().SoulType = SoulType.LOST;
                // other.GetComponent<SoulMovementController>().IsAttracted = false;

            }
        }
    }

}
