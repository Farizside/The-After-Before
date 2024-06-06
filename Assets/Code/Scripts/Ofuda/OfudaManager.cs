using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfudaManager : MonoBehaviour
{
    [SerializeField] private Transform[] _ofudaLocations;
    [SerializeField] private GameObject _ofudaRoom;
    void Start()
    {
        Instantiate(_ofudaRoom,  _ofudaLocations[Random.Range(0, _ofudaLocations.Length)]);
    }
}
