using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulDetectorController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _soulInArea = new();
    public GameEvent onAttract;
    public GameEvent onDeattract;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Soul")
        {
            _soulInArea.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Soul")
        {
            _soulInArea.Remove(other.gameObject);
        }
    }

    [ContextMenu("Attract Soul")]
    public void Attract()
    {
        onAttract.Raise(this, _soulInArea);
        // Use this approach if soul's rigidbody is removed
        //onAttract.Raise(this, transform.position);
    }
    [ContextMenu("Deattract Soul")]
    public void Deattract()
    {
        onDeattract.Raise(this);
    }
}
