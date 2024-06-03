using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameplayController : MonoBehaviour
{
    private UpgradeManager _upgrade;
    private WaveManager _wave;
    private EventSystem _eventSystem;
    void Awake()
    {
        _eventSystem = FindFirstObjectByType<EventSystem>();
        _eventSystem.firstSelectedGameObject = GameObject.FindGameObjectWithTag("Resume");
        // Debug.Log(GameObject.FindAnyObjectByTag("Resume"));
    }
}
