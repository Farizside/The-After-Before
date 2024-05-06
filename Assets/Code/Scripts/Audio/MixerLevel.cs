using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerLevel : MonoBehaviour
{
    public AudioMixer MasterMixer;

    public void SetSfxLvl(float sfxLvl)
    {
        MasterMixer.SetFloat("SfxVol", sfxLvl);
    }
    public void SetMusicLvl(float musicLvl)
    {
        MasterMixer.SetFloat("MusicVol", musicLvl);
    }

}
