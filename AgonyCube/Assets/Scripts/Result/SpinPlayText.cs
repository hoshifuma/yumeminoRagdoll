using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class SpinPlayText : MonoBehaviour {

    private Text targetText3;
    public Text spinMin;
    void spin()
    {
        this.targetText3 = this.GetComponent<Text>();
        string resultSpinPlay = Convert.ToString(Data.instance.spin);
        this.targetText3.text = resultSpinPlay;
        spinMin.text = Convert.ToString(Data.instance.spinMin);
    }
    // Use this for initialization
    void Start () {
        spin();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
