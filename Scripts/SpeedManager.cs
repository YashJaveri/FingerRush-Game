using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : MonoBehaviour {
    static public SpeedManager singleton;
    public float obsSpeed;
    public int initialScore;
    public float incriment = -0.2f;

    private void Awake()
    {
        singleton = this;
    }
    private void OnEnable()
    {
        incriment = -0.2f;
        initialScore = (PlayerPrefs.GetInt("RevivedScore", 0) / 5) * 5;
        obsSpeed = incriment * (initialScore / 5.0f) - 8.0f;
 
        
    }

}
