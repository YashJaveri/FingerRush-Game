using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {
    static bool thisIsCreated = false;
    void Awake()
    {
        if (thisIsCreated == false)
        {
            DontDestroyOnLoad(this.gameObject);
            thisIsCreated = true;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
}
