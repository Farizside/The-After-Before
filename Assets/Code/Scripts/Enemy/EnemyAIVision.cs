using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIVision : MonoBehaviour
{
    [Header("Settings")]
    public GameObject PureSoul;
    public EnemyAIMovement EnemyAgent;
    public float Radius;
    public float ViewDistance;
    [SerializeField] private float _stoppingDistance;
    [SerializeField] private float _rotationSpeed;

    private Vector3 _different;

    void Start()
    {
        EnemyAgent = GetComponent<EnemyAIMovement>();
    }

    void Update()
    {
        _different = PureSoul.transform.position - transform.position;
        Vision();
        StopDistance(_stoppingDistance);
    }

    private void Vision()
    {
        float angleToTarget = Vector3.Angle(transform.forward, _different);

        if (angleToTarget < Radius / 2 &&_different.magnitude <= ViewDistance)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, _different, out hit, ViewDistance))
            {
                if (hit.collider.gameObject == PureSoul)
                {
                    EnemyAgent.SetPureSoulDetected(true);
                    EnemyAgent.SetNewDestination(PureSoul.transform.position);
                }
            }
        }

        if (angleToTarget >= Radius / 2 || _different.magnitude > ViewDistance)
        {
            EnemyAgent.SetPureSoulDetected(false);
        }
    }


    private void StopDistance(float distance)
    {
        float distanceToTarget = _different.magnitude;
        if (distanceToTarget <= distance)
        {
            EnemyAgent.IsStopped(true);
            EnemyRotate(_rotationSpeed);
 
        }
    }

    private void EnemyRotate(float speed)
    {
        Quaternion targetRotation = Quaternion.LookRotation(PureSoul.transform.position - transform.position);
        targetRotation.x = 0; 
        targetRotation.z = 0; 

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }
}
