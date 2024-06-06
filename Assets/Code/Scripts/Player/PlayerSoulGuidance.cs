using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSoulGuidance : MonoBehaviour
{
    [SerializeField] private InputManager _input;

    [Header("Soul Management")]
    [SerializeField] private List<SoulMovementController> _soulsAttracted;

    [Header("Movement Variable While Attracted")]
    [SerializeField] private float _minDist = 1.5f;
    [SerializeField] private float _speed = 1.0f;

    [Header("Movement Variable While Unattracted")]
    [SerializeField] private float _rangeDist = 3.0f;

    private PlayerMovement _playerMovement;
    private SoulMovementController _currSoul;
    private Animator _animator;

    private int _isAttractHash;
    private int _isDeattractHash;

    public float SlowingSpeed;


    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();

        _isAttractHash = Animator.StringToHash("isAttract");
        _isDeattractHash = Animator.StringToHash("isDeattract");

        _input.DeattractEvent += DeattractSoul;
        _input.AttractEvent += Attract;
    }

    private void OnDisable()
    {
        _input.DeattractEvent -= DeattractSoul;
        _input.AttractEvent -= Attract;
    }

    private void Attract()
    {
        if (_currSoul == null)
        {
            Debug.LogWarning("No current soul to attract.");
            return;
        }

        _animator.SetTrigger(_isAttractHash);

        if (!_currSoul.IsAttracted)
        {
            if (_soulsAttracted.Count == 0)
            {
                _currSoul.IsFirst = true;
            }
            AttractSoul(_currSoul);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Soul"))
        {
            _currSoul = other.GetComponent<SoulMovementController>();

            if (!_currSoul.IsAttracted)
            {
                AddOutline(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Soul"))
        {
            _currSoul = null;
            RemoveOutline(other);
        }
    }

    private void Update()
    {
        UpdateTargetTransform();
    }

    private void AttractSoul(SoulMovementController soul)
    {
        if (!_soulsAttracted.Contains(soul))
        {
            soul.IsAttracted = true;
            _soulsAttracted.Add(soul);

            // Play VFX when attracting a soul
            SoulVFX vfxScript = soul.GetComponent<SoulVFX>();
            if (vfxScript != null)
            {
                vfxScript.PlayVFX();
                Debug.Log("Playing VFX for attracted soul.");
            }
            else
            {
                Debug.LogError("SoulVFX component not found on the soul.");
            }

            if (_playerMovement.MovementSpeed > _playerMovement.MovementSpeedLimit)
            {
                _playerMovement.MovementSpeed -= SlowingSpeed;
            }
            AudioManager.Instance.PlaySound3D("Attract", soul.transform.position); //audio
        }
    }

    [ContextMenu("Deattract All Soul")]
    private void DeattractSoul()
    {
        _animator.SetTrigger(_isDeattractHash);

        foreach (SoulMovementController soul in _soulsAttracted)
        {
            soul.IsAttracted = false;
            AudioManager.Instance.PlaySound3D("Deattract", soul.transform.position); //audio

            SoulVFX vfxScript = soul.GetComponent<SoulVFX>();
            if (vfxScript != null)
            {
                vfxScript.StopVFX();
                Debug.Log("Stopping VFX for deattracted soul.");
            }
            else
            {
                Debug.LogError("SoulVFX component not found on the soul.");
            }
        }

        _playerMovement.MovementSpeed += _soulsAttracted.Count * SlowingSpeed;
        _soulsAttracted.Clear();
    }


    public void AttackSoul(SoulMovementController soulMovementController)
    {
        int index = _soulsAttracted.IndexOf(soulMovementController);
        if (index != -1)
        {
            List<SoulMovementController> attackedSouls = _soulsAttracted.GetRange(index, _soulsAttracted.Count - index);
            foreach (SoulMovementController soul in attackedSouls)
            {
                soul.IsAttracted = false;
                soul.GetComponent<SoulTypeController>().SoulType = SoulType.LOST;
                AudioManager.Instance.PlaySound3D("EnemyTouch", soul.transform.position);//audio
                SoulVFX vfxScript = soul.GetComponent<SoulVFX>();
                if (vfxScript != null)
                {
                    vfxScript.StopVFX();
                    Debug.Log("Stopping VFX for deattracted soul.");
                }
                else
                {
                    Debug.LogError("SoulVFX component not found on the soul.");
                }

                HitVFX hitScript = soul.GetComponent<HitVFX>();
                if (hitScript != null)
                {
                    hitScript.PlayHitVFX();
                    Debug.Log("Playing VFX for hit soul.");
                }
                else
                {
                    Debug.LogError("HitVFX component not found on the soul.");
                }
            }

            _soulsAttracted.RemoveRange(index, _soulsAttracted.Count - index);
        }

    }

    public void SubmitSoul()
    {
        foreach(SoulMovementController soul in _soulsAttracted)
        {
            if (soul.GetComponent<SoulTypeController>().SoulType == SoulType.PURE)
            {
                soul.IsAttracted = false;
                soul.gameObject.SetActive(false);
                _playerMovement.MovementSpeed += SlowingSpeed;
                GameManager.Instance.SubmitSoul();
                AudioManager.Instance.PlaySound3D("Submit", soul.transform.position);//audio

                OfudaVFX ofudaScript = soul.GetComponent<OfudaVFX>();
                if (ofudaScript != null)
                {
                    ofudaScript.PlayOfudaVFX();
                    Debug.Log("Playing VFX for submit soul.");
                }
                else
                {
                    Debug.LogError("SubmitlVFX component not found on the soul.");
                }

            }
            else
            {
                soul.IsAttracted = false;
                _playerMovement.MovementSpeed += SlowingSpeed;
            }
        }
        
        _soulsAttracted.Clear();
    }
    
    private void UpdateTargetTransform()
    {
        Vector3 targetPosition = transform.position;
        for(int i=0; i<_soulsAttracted.Count; i++)
        {
            _soulsAttracted[i].TargetPosition = targetPosition;
            targetPosition = _soulsAttracted[i].transform.position;
        }
    }

    private void AddOutline(Collider other)
    {
        var outline = other.GetComponent<Outline>() ? other.GetComponent<Outline>() : other.AddComponent<Outline>();
        outline.enabled = true;
        outline.OutlineWidth = 7f;
    }

    private void RemoveOutline(Collider other)
    {
        var outline = other.GetComponent<Outline>() ? other.GetComponent<Outline>() : other.AddComponent<Outline>();
        outline.enabled = false;
    }
}
