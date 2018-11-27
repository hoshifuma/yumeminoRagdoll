using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageState : MonoBehaviour {
    public static string stageName = "ステージ１";
    public static string swapPlay = "3";
    public static string spinPlay = "1";

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
        swapPlay = "2";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
