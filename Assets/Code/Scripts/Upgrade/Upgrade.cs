using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : ScriptableObject
{
    public readonly int id;
    public readonly string title;
    public readonly string description;
    public readonly Sprite sprite;

    public virtual void Choose()
    {
        Debug.Log($"Chosen {title} Upgrade");
    }
}
