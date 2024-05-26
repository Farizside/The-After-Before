using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIMovement : MonoBehaviour
{
    [Header("Settings")]
    public NavMeshAgent EnemyAgent;
    public Transform[] Waypoints;
    [SerializeField] private float _moveSpeedEnemy;
    private Animator _animator;
    private Vector3 _targetWaypoints;
    private int _index;
    private bool _isStunned;
    private int _isMovingHash;
    private int _isStunnedHash;
    private bool _isPureSoulDetected;
    
    void Start()
    {
        EnemyAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _isMovingHash = Animator.StringToHash("isMoving");
        _isStunnedHash = Animator.StringToHash("isStunned");
        _isStunned = false;
        _isPureSoulDetected = false;
        UpdateDestination();
    }

    void Update()
    {
        Animation();
        Speed(_moveSpeedEnemy);
        MovePatrolWaypoints();
    }

    private void MovePatrolWaypoints()
    {
        if (!_isStunned && !_isPureSoulDetected && Vector3.Distance(transform.position, _targetWaypoints) < 1)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
    }

    private void IterateWaypointIndex()
    {
        _index++;
        if (_index == Waypoints.Length)
        {
            _index = 0;
        }
    }

    private void UpdateDestination()
    {
        _targetWaypoints = Waypoints[_index].position;
        EnemyAgent.SetDestination(_targetWaypoints);
    }
    private void Animation()
    {
        bool isMoving = EnemyAgent.velocity.magnitude > 0.1f && !_isStunned;
        _animator.SetBool(_isMovingHash, isMoving);
        _animator.SetBool(_isStunnedHash, _isStunned);

    }

    private void Speed(float moveSpeedEnemy)
    {
        EnemyAgent.speed = moveSpeedEnemy;
    }

    public void Stunned(bool stun, float duration)
    {
        _isStunned = stun;
        IsStopped(stun);
        if(_isStunned)
        {
            StartCoroutine(StunnedDuration(duration));
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
            // IsStopped(false);
            // MovePatrolRandom();
            UpdateDestination();
            MovePatrolWaypoints();
        }
    }

    public void IsStopped(bool stop)
    {
        EnemyAgent.isStopped = stop;
    }

    public bool IsStunned()
    {
        return _isStunned;
    }
}