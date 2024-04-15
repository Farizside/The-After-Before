using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SoulMovementController : MonoBehaviour
{
    const float EPS = 1e-4f;
    public Vector3 TargetPosition;

    [Header("Soul status")]
    public bool IsAttracted = false;

    [Header("Attracted soul variables")]
    [SerializeField] private float _minDist = 1.5f;
    [SerializeField] private float _speed = 5.0f;

    [Header("Unattracted soul variables")]
    [SerializeField] private float _rangeDist = 5.0f;
    private bool _isTargetReached = false;
    private bool _isLookingForTarget = true;
    private float _idleDuration = 3.0f;
    private float _randomMoveSpeed = 1.0f;


    public float Velocity = 0f;

    private void Start()
    {
        float angleDirection = Random.Range(0.0f, 2 * Mathf.PI);
        TargetPosition = transform.position + new Vector3(Mathf.Sin(angleDirection), 0.0f, Mathf.Cos(angleDirection)) * Random.Range(0.0f, _rangeDist);
    }

    private void Update()
    {
        if (IsAttracted)
        {
            MoveWhileAttracted();
        }
        else
        {
            MoveWhileUnattracted();
        }
    }
    public void MoveWhileAttracted()
    {
        float dist = Vector3.Distance(transform.position, TargetPosition);
        if (dist > _minDist)
        {
            float step = (dist - _minDist + 0.1f) * _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, step);
        }
    }

    public void MoveWhileUnattracted()
    {
        // Move to Target Position
        if (!_isTargetReached)
        {
            Vector3 stepPosition = Vector3.MoveTowards(transform.position, TargetPosition, _randomMoveSpeed * Time.deltaTime);
            Velocity = Vector3.Distance(stepPosition, transform.position);
            transform.position = stepPosition;
            if(Vector3.Distance(transform.position, TargetPosition) < EPS)
            {
                _isTargetReached = true;
                _isLookingForTarget = false;
            }
        } // After reaching target position, idling for some seconds
        else if (!_isLookingForTarget)
        {
            Velocity = 0f;
            StartCoroutine(Idling(_idleDuration));
        } // After idling, soul is looking for random target position. After found the target position, souls is moving toward the target position
        else
        {
            Velocity = 0f;
            float angleDirection = Random.Range(0.0f, 2 * Mathf.PI);
            TargetPosition = transform.position + new Vector3(Mathf.Sin(angleDirection), 0.0f, Mathf.Cos(angleDirection)) * Random.Range(0.0f, _rangeDist);
            _isTargetReached = false;
        }
    }

    private IEnumerator Idling(float duration)
    {
        yield return new WaitForSeconds(duration);
        _isLookingForTarget = true;
    }
}
