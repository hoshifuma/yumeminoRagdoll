using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class SwapPlayText : MonoBehaviour {

    private Text targetText2;
    public Text swapMin;
    void swap()
    {
        this.targetText2 = this.GetComponent<Text>();
        string resultSwapPlay = Convert.ToString(Data.instance.swap);
        this.targetText2.text = resultSwapPlay;
        swapMin.text = Convert.ToString(Data.instance.swapMin);
    }
    // Use this for initialization
    void Start () {
        swap();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
