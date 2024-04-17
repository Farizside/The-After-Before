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
    public SoulType SoulType {
        get => _soulType;
        set
        {
            if(value == SoulType.PURE)
            {
                GetComponent<MeshRenderer>().material = _pureMaterial;
            }
            else
            {
                GetComponent<MeshRenderer>().material = _lostMaterial;
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
