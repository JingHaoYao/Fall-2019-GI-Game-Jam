using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveInfo : MonoBehaviour
{
    [System.Serializable]
    public struct Wave
    {
        [System.Serializable]
        public struct WaveGroup
        {
            public int numberCircles;
            public int numberTriangles;
            public int numberSquares;
        }
        public WaveGroup[] waveGroups;
        public float waitInterval;
    }

    public Wave[] waveList;

    public int startingRedPixels;
    public int startingGreenPixels;
    public int startingBluePixels;
}
