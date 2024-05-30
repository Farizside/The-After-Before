using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MusicTrack
{
    public string TrackName;
    public AudioClip Clip;
}

public class MusicLibrary : MonoBehaviour
{
    public MusicTrack[] Tracks;

    public AudioClip GetClipFromName(string TrackName)
    {
        foreach (var track in Tracks)
        {
            if (track.TrackName == TrackName)
            {
                return track.Clip;
            }
        }
        return null;
    }
}