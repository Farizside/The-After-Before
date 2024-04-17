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
    [SerializeField] private Material _pureMaterial;
    [SerializeField] private Material _lostMaterial;

    [SerializeField] private SoulType _soulType;
    private MeshRenderer _mesh => GetComponent<MeshRenderer>();
    
    public SoulType SoulType {
        get => _soulType;
        set
        {
            if(value == SoulType.PURE)
            {
                _mesh.material = _pureMaterial;
            }
            else
            {
                _mesh.material = _lostMaterial;
            }
            _soulType = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SoulType = SoulType.LOST;
    }
}
