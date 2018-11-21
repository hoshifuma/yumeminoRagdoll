using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageState : MonoBehaviour {
    public static string stageName = "ステージ２";
    public static string swapPlay = "1";
    public static string spinPlay = "3";

    // getter
    public static string GetStageName()
    {
        return stageName;
    }

    public static string GetSwapPlay()
    {
        return swapPlay;
    }

    public static string GetSpinPlay()
    {
        return spinPlay;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
