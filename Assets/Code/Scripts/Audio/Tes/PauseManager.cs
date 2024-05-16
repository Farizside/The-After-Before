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
    public AudioMixer MasterMixer;
    public Slider MasterSlider;
    public AudioMixerSnapshot Paused;
    public AudioMixerSnapshot Unpaused;
    private bool _prevTimeScale = true;
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false; 
    }

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
            _prevTimeScale = isPaused;
            if (isPaused)
            {
                Paused.TransitionTo(.001f);
            }
            else
            {
                Unpaused.TransitionTo(.001f);
            }
        }
    }
    public void SetMaster(float MasterLvl)
    {
        MasterMixer.SetFloat("MasterVol", MasterLvl);
    }
    public void SaveVolume()
    {
        MasterMixer.GetFloat("MusicVolume", out float musicVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    public void LoadVolume()
    {
        MasterSlider.value = PlayerPrefs.GetFloat("MusicVolume");

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
