using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapPlayText : MonoBehaviour {

    private Text targetText2;

    void swap()
    {
        this.targetText2 = this.GetComponent<Text>();
        string resultSwapPlay = StageState.GetSwapPlay();
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
