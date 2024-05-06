using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class PauseManager : MonoBehaviour
{
    Canvas canvas;
    public AudioMixerSnapshot Paused;
    public AudioMixerSnapshot Unpaused;
    private bool prevTimeScale = true;
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false; // Hide the canvas on Start
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.enabled = !canvas.enabled;
            Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        Lowpass();
    }

    void Lowpass()
    {
        bool isPaused = Time.timeScale == 0;
        {
            prevTimeScale = isPaused;
            if (isPaused)
            {
                Paused.TransitionTo(.01f);
            }
            else
            {
                Unpaused.TransitionTo(.01f);
            }
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
