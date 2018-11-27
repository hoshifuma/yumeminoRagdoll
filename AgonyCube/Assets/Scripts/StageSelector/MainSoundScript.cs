using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSoundScript : MonoBehaviour {

    public bool DontDestroyEnabled = true;

    // Use this for initialization
    void Start()
    {
        if (DontDestroyEnabled) {
          
            DontDestroyOnLoad(this);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}