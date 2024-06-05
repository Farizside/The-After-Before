using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerLevel : MonoBehaviour
{
    public AudioMixer MasterMixer;
    public Slider MasterSlider;
    private bool _isMuted = false;

    public void Start()
    {
        if (PlayerPrefs.HasKey("MasterVol"))
        { 
            LoadVolume(); 
        }
        else
        {
            SetMaster();
        }

        
    }
    public void SetMaster()
    {
        float volume = MasterSlider.value;
        MasterMixer.SetFloat("MasterVol", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("MasterVol", volume);
    }
    

    public void LoadVolume()
    {
        MasterSlider.value = PlayerPrefs.GetFloat("MasterVol");

        SetMaster();
        
    }
    public void ToggleMaster() 
    {
        _isMuted = !_isMuted;

        if (_isMuted)
        {
            MasterMixer.SetFloat("MasterVol", -80f); 
        }
        else
        {
            MasterMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol")); 
        }
    }
}
