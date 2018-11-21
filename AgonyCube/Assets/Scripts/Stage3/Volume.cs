using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour {


    public UnityEngine.Audio.AudioMixer mixer;

    public void BGMVol(Slider slider)
    {
        mixer.SetFloat("BGMVol", slider.value);
    }
}
