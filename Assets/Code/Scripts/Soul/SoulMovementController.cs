using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SoulMovementController : MonoBehaviour
{
    const float EPS = 1e-4f;
    private Vector3 _targetPosition;
    public Vector3 TargetPosition
    {
        get => _targetPosition;
        set
        {
            _targetPosition = value;
            if (Vector3.Distance(value, transform.position) > EPS)
            {
                transform.LookAt(value);
            }
        }
    }

    private Light _light;
    [Header("Soul status")]
    private bool _isAttracted = false;
    public bool IsAttracted { 
        get => _isAttracted; 
        set 
        { 
            _isAttracted = value;
            _light.enabled = _isAttracted;
        } 
    }

    [Header("Attracted soul variables")]
    [SerializeField] private float _minDist = 1.5f;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float playerOffset = 0.75f;
    public bool IsFirst = false;

    [Header("Unattracted soul variables")]
    [SerializeField] private float _rangeDist = 5.0f;
    private bool _isTargetReached = false;
    private bool _isLookingForTarget = true;
    private float _idleDuration = 3.0f;
    private float _randomMoveSpeed = 1.0f;


    public float Velocity = 0f;

    // Reference to the SoulVFX component
    private SoulVFX _vfxScript;

    private void Awake()
    {
        // Get the SoulVFX component
        _vfxScript = GetComponent<SoulVFX>();
        _light = GetComponentInChildren<Light>();
        if (_vfxScript == null)
        {
            Debug.LogError("SoulVFX script not found on soul.");
        }
        if (_light == null)
        {
            Debug.LogError("Light not found on soul.");
        }
    }

    private void Start()
    {
        float angleDirection = Random.Range(0.0f, 2 * Mathf.PI);
        TargetPosition = transform.position + new Vector3(Mathf.Sin(angleDirection), 0.0f, Mathf.Cos(angleDirection)) * Random.Range(0.0f, _rangeDist);
    }
    private void OnEnable()
    {
        if (_vfxScript != null && IsAttracted)
        {
            _vfxScript.PlayVFX();
        }
    }

    private void OnDisable()
    {
        if (_vfxScript != null)
        {
            _vfxScript.StopVFX();
        }
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
        Animator animator = GetComponent<SoulTypeController>().Animator;
        float dist = Vector3.Distance(transform.position, TargetPosition);
        if (dist > _minDist + (IsFirst ? 1 : 0) * playerOffset)
        {
            float step = (dist - _minDist + 0.1f) * _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, step);
            if (step > 0.27) animator.SetTrigger("Dash");
            else animator.SetBool("isMoving", dist - _minDist > EPS);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void MoveWhileUnattracted()
    {
        Animator animator = GetComponent<SoulTypeController>().Animator;
        // Move to Target Position
        if (!_isTargetReached)
        {
            Vector3 stepPosition = Vector3.MoveTowards(transform.position, TargetPosition, _randomMoveSpeed * Time.deltaTime);
            Velocity = Vector3.Distance(stepPosition, transform.position);
            transform.position = stepPosition;
            if (Vector3.Distance(transform.position, TargetPosition) < EPS)
            {
                _isTargetReached = true;
                _isLookingForTarget = false;
            }
        } // After reaching target position, idling for some seconds
        else if (!_isLookingForTarget)
        {
            animator.SetBool("isMoving", false);
            Velocity = 0f;
            StartCoroutine(Idling(_idleDuration));
        } // After idling, soul is looking for random target position. After found the target position, souls is moving toward the target position
        else
        {
            Velocity = 0f;
            float angleDirection = Random.Range(0.0f, 2 * Mathf.PI);
            TargetPosition = transform.position + new Vector3(Mathf.Sin(angleDirection), 0.0f, Mathf.Cos(angleDirection)) * Random.Range(0.0f, _rangeDist);
            _isTargetReached = false;
            animator.SetBool("isMoving", true);
        }
    }

    private IEnumerator Idling(float duration)
    {
        yield return new WaitForSeconds(duration);
        _isLookingForTarget = true;
    }
}