using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIVision : MonoBehaviour
{
    [Header("Settings")]
    public EnemyAIMovement EnemyAgent;
    public float Radius;
    public float ViewDistance;

    [SerializeField] private float _stoppingDistance;
    [SerializeField] private float _rotationSpeed;

    private Transform _target;
    void Start()
    {
        EnemyAgent = GetComponent<EnemyAIMovement>();
    }

    void Update()
    {
        _target = FindClosestSoul();
        if (_target != null)
        {
            Vision();
            StopDistance(_stoppingDistance);
            if(!EnemyAgent.IsStunned())
            {
                EnemyAgent.IsStopped(false);
            }
            else if(EnemyAgent.IsStunned())
            {
                EnemyAgent.IsStopped(true);
            }
        }
        else
        {
            EnemyAgent.SetPureSoulDetected(false);
            if(!EnemyAgent.IsStunned())
            {
                EnemyAgent.IsStopped(false);
            }
        }
    }

    private void Vision()
    {
        Vector3 _different = _target.position - transform.position;
        float angleToTarget = Vector3.Angle(transform.forward, _different);

        if (angleToTarget < Radius / 2 && _different.magnitude <= ViewDistance)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, _different, out hit, ViewDistance))
            {
                
                SoulMovementController soulMovement = hit.collider.gameObject.GetComponent<SoulMovementController>();
                if (soulMovement != null && soulMovement.IsAttracted && hit.collider.gameObject.CompareTag("Soul"))
                {
                    SoulTypeController soulType = hit.collider.gameObject.GetComponent<SoulTypeController>();
                    if (soulType != null && soulType.SoulType == SoulType.PURE)
                    {
                        EnemyAgent.SetPureSoulDetected(true);
                        EnemyAgent.SetNewDestination(_target.position);
                    }
                }
            }
        }

        if(angleToTarget >= Radius / 2 || _different.magnitude > ViewDistance)
        {
            EnemyAgent.SetPureSoulDetected(false);
        }
    }

    private void StopDistance(float distance)
    {
        if (_target != null && _target.TryGetComponent(out SoulTypeController soulType))
        {
            if (soulType.SoulType == SoulType.PURE)
            {
                float distanceToTarget = (_target.position - transform.position).magnitude;
                if (distanceToTarget <= distance)
                {
                    EnemyAgent.IsStopped(true);
                    EnemyRotate(_rotationSpeed);
                }
                else
                {
                    EnemyAgent.IsStopped(false);
                }
            }
            else if (soulType.SoulType == SoulType.LOST)
            {
                EnemyAgent.IsStopped(false);
            }
        }
        else
        {
            EnemyAgent.IsStopped(false);
        }
    }

    private void EnemyRotate(float speed)
    {
        Quaternion targetRotation = Quaternion.LookRotation(_target.position - transform.position);
        targetRotation.x = 0; 
        targetRotation.z = 0; 

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);
    }

    private Transform FindClosestSoul()
    {
        GameObject[] souls = GameObject.FindGameObjectsWithTag("Soul");
        Transform closestSoul = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject soul in souls)
        {
            SoulTypeController soulType = soul.GetComponent<SoulTypeController>();
            if (soulType != null && soulType.SoulType == SoulType.PURE)
            {
                float distance = Vector3.Distance(transform.position, soul.transform.position);
                if (distance < closestDistance)
                {
                    closestSoul = soul.transform;
                    closestDistance = distance;
                }
            }
        }

        return closestSoul;
    }
}
