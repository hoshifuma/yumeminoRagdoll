using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class SwapPlayText : MonoBehaviour {

    private Text targetText2;

    void swap()
    {
        this.targetText2 = this.GetComponent<Text>();
        string resultSwapPlay = Convert.ToString(Score.instance.swap);
        this.targetText2.text = resultSwapPlay;
    }
    // Use this for initialization
    void Start () {
        swap();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
