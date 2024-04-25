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
    //[SerializeField] private RuntimeAnimatorController _lostAnimatorController;
    //[SerializeField] private RuntimeAnimatorController _pureAnimatorController;
    //[SerializeField] private Avatar _lostAvatar;
    //[SerializeField] private Avatar _pureAvatar;
    public Animator Animator;
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
                transform.Find("Lost").gameObject.SetActive(false);
                transform.Find("Pure").gameObject.SetActive(true);
                _timeRemaining = _timeToLost;
            }
            else
            {
                transform.Find("Pure").gameObject.SetActive(false);
                transform.Find("Lost").gameObject.SetActive(true);
            }
            Animator = GetComponentInChildren<Animator>();
            _soulType = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //_animator = GetComponent<Animator>();
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
