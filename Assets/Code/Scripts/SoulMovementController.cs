using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SoulMovementController : MonoBehaviour
{
    public Vector3 TargetPosition;

    [Header("Soul status")]
    public bool IsAttracted = false;

    [Header("Attracted soul variables")]
    [SerializeField] private float _minDist = 1.5f;
    [SerializeField] private float _speed = 1.0f;

    [Header("Unattracted soul variables")]
    [SerializeField] private float _rangeDist = 3.0f;
    private bool _isTargetReached = false;
    private bool _isLookingForTarget = true;
    private float _idleDuration = 3.0f;
    private float _randomMoveSpeed = 1.0f;

    private float eps = 1e-4f;

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
        if (!_isTargetReached)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, _randomMoveSpeed * Time.deltaTime);
            if(Vector3.Distance(transform.position, TargetPosition) < eps)
            {
                _isTargetReached = true;
                _isLookingForTarget = false;
            }
        }
        else if (!_isLookingForTarget)
        {
            StartCoroutine(Idling(_idleDuration));
        }
        else
        {
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

    public void OnAttract(Component sender, object data)
    {
        if(sender.gameObject.tag == "Player" && data is int)
        {
            if((int) data == gameObject.GetInstanceID())
            {
                IsAttracted = true;
            }
        }
    }

    public void OnDeattract(Component sender, object data)
    {
        if(sender.gameObject.tag == "Player")
        {
            IsAttracted = false;
        }
    }
}
