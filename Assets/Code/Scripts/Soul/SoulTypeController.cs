using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SoulType
{
    PURE,
    LOST
};

public class SoulTypeController : MonoBehaviour
{
    // [SerializeField] private Material _pureMaterial;
    // [SerializeField] private Material _lostMaterial;

    [SerializeField] private SoulType _soulType;
    [SerializeField] private float _timeToLost;
    private float _timeRemaining;
    // private MeshRenderer _mesh => GetComponent<MeshRenderer>();
    
    public SoulType SoulType {
        get => _soulType;
        set
        {
            if(value == SoulType.PURE)
            {
                // _mesh.material = _pureMaterial;
                _timeRemaining = _timeToLost;
            }
            else
            {
                // _mesh.material = _lostMaterial;
            }
            _soulType = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SoulType = SoulType.LOST;
        _timeRemaining = _timeToLost;
    }

    private void Update()
    {
        if (SoulType == SoulType.PURE && !GetComponent<SoulMovementController>().IsAttracted)
        {
            if(_timeRemaining > 0f)
            {
                _timeRemaining -= Time.deltaTime;
            }
            else
            {
                SoulType = SoulType.LOST;
            }
        }
    }
}
