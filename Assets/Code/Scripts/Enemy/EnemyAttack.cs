using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _delayAttackDuration;
    private bool _isAttacking;
    void Start ()
    {
        _isAttacking = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Soul") && !_isAttacking)
        {
            StartCoroutine(AttackRoutine(_delayAttackDuration));
        }
    }

    private IEnumerator AttackRoutine(float duration)
    {
        _isAttacking = true;
        while (_isAttacking)
        {
            //enemy attack
            yield return new WaitForSeconds(duration);
        }
    }

}
