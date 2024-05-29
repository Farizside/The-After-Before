using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Wave Data")]
public class WaveData : ScriptableObject
{
    public List<Waves> wavesData;
    
    [Serializable]
    public struct Waves
    {
        public int id;
        public int target;
        public float time;
        public int initialSoul;
        public float interval;
        public float pureRate;
        public int maxSouls;
    }
}
