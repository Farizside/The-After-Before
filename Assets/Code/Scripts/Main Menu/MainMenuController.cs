using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    void Start()
    {
        Destroy(GameObject.Find("Game Manager"));
        Destroy(GameObject.Find("Wave Manager"));
        Destroy(GameObject.Find("UI Manager"));
        Destroy(GameObject.Find("Upgrade Manager"));
        Destroy(GameObject.Find("Event System"));
    }
}
