using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private bool _isAttacking;
    //public EnemyAIMovement enemyAIMovement;

    void Start ()
    {
        _isAttacking = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Soul") )
        {
            if (other.GetComponent<SoulMovementController>().IsAttracted&&other.gameObject.GetComponent<SoulTypeController>().SoulType == SoulType.PURE)
            {  
                //other.GetComponent<SoulAttackController>().Attack();

            }
            //enemyAIMovement.Stunned(true, 3f);
        }
    }

}
