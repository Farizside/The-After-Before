using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIMovement : MonoBehaviour
{
    [Header("Settings")]
    public NavMeshAgent EnemyAgent;
    public Transform CenterPoint;
    [SerializeField] private float _range;
    [SerializeField] private float _moveSpeedEnemy;

    private bool _isStunned;
    private Animator _animator;
    private int _isMovingHash;
    
    void Start()
    {
        EnemyAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _isMovingHash = Animator.StringToHash("isMoving");
        _isStunned = false;
    }

    void Update()
    {
        Animation();
        MovePatrolRandom();
        Speed(_moveSpeedEnemy);
    }

    private void Animation()
    {
        bool isMoving = !IsDestination() && !_isStunned;

        _animator.SetBool(_isMovingHash, isMoving);
    }

    private void MovePatrolRandom()
    {
        if(IsDestination())
        {
            Vector3 point;
            if(SetRandomPoint(CenterPoint.position, _range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.red, 1.0f);
                EnemyAgent.SetDestination(point);
            }
        }

    }

    private bool IsDestination()
    {
        return EnemyAgent.remainingDistance <= EnemyAgent.stoppingDistance;
    }

    private bool SetRandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPositionInSphere = Random.insideUnitSphere * range;
        Vector3 randomPoint = center + randomPositionInSphere ;
        NavMeshHit hit;

        if(NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            //Debug.Log("Random point ditemukan");
            result = hit.position;
            return true;
        }
        //Debug.Log("Random point tidak valid");
        result = Vector3.zero;
        return false;
    }

    private void Speed(float moveSpeedEnemy)
    {
        EnemyAgent.speed = moveSpeedEnemy;
    }

    public void Stunned(bool stun, float duration)
    {
        _isStunned = stun;
        if(_isStunned)
        {
            EnemyAgent.isStopped = true;
            StartCoroutine(StunnedDuration(duration));
            //Debug.Log("Kena Stunned");
        }
        else
        {
            EnemyAgent.isStopped = false;
            //Debug.Log("Stunned selesai");
        }
    }

    private IEnumerator StunnedDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        EnemyAgent.isStopped = false;
        _isStunned = false;
    }

    public void SetNewDestination(Vector3 destination)
    {
        EnemyAgent.SetDestination(destination);
        //Debug.Log("Destination diatur ulang ke " + destination);
    }
}
