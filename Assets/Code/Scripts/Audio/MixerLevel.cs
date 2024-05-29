using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerLevel : MonoBehaviour
{
    public AudioMixer MasterMixer;
    public Slider MasterSlider;

    public void SetMaster(float MasterLvl)
    {
        MasterMixer.SetFloat("MasterVol", MasterLvl);
    }
    public void SaveVolume()
    {
        MasterMixer.GetFloat("MasterVol", out float MasterLvl);
        PlayerPrefs.SetFloat("MasterVol", MasterLvl);
    }

    public void LoadVolume()
    {
        MasterSlider.value = PlayerPrefs.GetFloat("MasterVol");
 
    }
}
