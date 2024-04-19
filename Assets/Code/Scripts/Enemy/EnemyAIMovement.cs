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
    private Animator _animator;
    private bool _isStunned;
    private int _isMovingHash;
    private bool _isPureSoulDetected;
    void Start()
    {
        EnemyAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _isMovingHash = Animator.StringToHash("isMoving");
        _isStunned = false;
        _isPureSoulDetected = false;
    }

    void Update()
    {
        Animation();
        Speed(_moveSpeedEnemy);
    }

    private void Animation()
    {
        bool isMoving = EnemyAgent.velocity.magnitude > 0.1f && !_isStunned;
        _animator.SetBool(_isMovingHash, isMoving);
    }

    private void MovePatrolRandom()
    {

        if(IsDestination())
        {
            Vector3 point;
            if(SetRandomPoint(CenterPoint.position, _range, out point))
            {
                EnemyAgent.SetDestination(point);
            }
        }
    }

    private bool IsDestination()
    {
        bool isDestination = !_isPureSoulDetected || EnemyAgent.remainingDistance <= EnemyAgent.stoppingDistance;
        return isDestination;
    }

    private bool SetRandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPositionInSphere = Random.insideUnitSphere * range;
        Vector3 randomPoint = center + randomPositionInSphere ;
        
        NavMeshHit hit;
        if(NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            
            result = hit.position;
            return true;
        }
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
            IsStopped(true);
            StartCoroutine(StunnedDuration(duration));
        }
        else
        {
            IsStopped(false);
        }
    }

    private IEnumerator StunnedDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        IsStopped(false);
        _isStunned = false;
    }

    public void SetNewDestination(Vector3 destination)
    {
        EnemyAgent.SetDestination(destination);
    }

    public void SetPureSoulDetected(bool detected)
    {
        _isPureSoulDetected = detected;
        if (!_isPureSoulDetected)
        {
            IsStopped(false);
            MovePatrolRandom();
        }
        else
        {
            IsStopped(false);
        }
        
    }

    public void IsStopped(bool stop)
    {
        EnemyAgent.isStopped = stop;
    }
}