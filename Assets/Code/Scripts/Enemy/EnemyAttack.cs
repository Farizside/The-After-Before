using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Animator AnimatorController;
    //public EnemyAIMovement enemyAIMovement;

    [SerializeField] private float _animationDuration;

    private bool _isAttacking;
    private int _isAttackingHash;
    private Collider _currentTarget;

    void Start ()
    {
        _isAttacking = false;
        _isAttackingHash = Animator.StringToHash("isAttacking");
    }

    private void OnTriggerEnter(Collider other)
    {
        {
            if (other.CompareTag("Soul"))
            {
                SoulMovementController soulMovement = other.GetComponent<SoulMovementController>();
                SoulTypeController soulType = other.GetComponent<SoulTypeController>();
               EnemyAIMovement enemyAIMovement = GetComponentInParent<EnemyAIMovement>();

                if (soulMovement.IsAttracted && soulType.SoulType == SoulType.PURE)
                {  
                    if(!enemyAIMovement.IsStunned())
                    {
                        _isAttacking = true;

                        if (AnimatorController != null)
                        {
                            AnimatorController.SetBool(_isAttackingHash, _isAttacking);
                        }

                        _currentTarget = other;

                        StartCoroutine(AnimationDuration());

                    }
                }
                else
                {
                    _isAttacking = false;
                }
            }
            
        }
    }

    private IEnumerator AnimationDuration()
    {
        yield return new WaitForSeconds(_animationDuration/2);
        _currentTarget.GetComponent<SoulAttackController>().Attack();
        _isAttacking = false;

        if (AnimatorController != null)
        {
            AnimatorController.SetBool(_isAttackingHash, _isAttacking);
        }
    }

    private void OnTriggerExit()
    {
        if (AnimatorController != null)
        {
            _isAttacking = false;
            AnimatorController.SetBool(_isAttackingHash, _isAttacking);
        }
    }

    public bool IsAttacking()
    {
        return _isAttacking;
    }
}
