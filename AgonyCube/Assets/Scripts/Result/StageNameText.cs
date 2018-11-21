using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageNameText : MonoBehaviour {
    private Text targetText1;
    
    

    void stag()
    {
        this.targetText1 = this.GetComponent<Text>();
        string resultStageName = StageState.GetStageName();
        this.targetText1.text = resultStageName;
    }

    

    
    // Use this for initialization
    void Start () {
        //Mainシーンから読み込む文字列（ステージ名）
        stag();
        
        
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
