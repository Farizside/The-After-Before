using UnityEngine;

public class BGMManager : MonoBehaviour
{
    // Start function to play BGM on game start
    void Start()
    {
        // Call MusicManager's PlayMusic method to start playing the "GamePlay" BGM
        AudioManager.Instance.PlayMusic("MainMenu", 0.5f);
    }
}
