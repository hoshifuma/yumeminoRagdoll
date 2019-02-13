using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SEVol : MonoBehaviour {



    public UnityEngine.Audio.AudioMixer mixer;

    public void SE(Slider slider)
    {
        mixer.SetFloat("SEVol", slider.value);
    }
}
