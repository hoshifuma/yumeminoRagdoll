using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {
    float startTime;

    //時間を取得します。
	void Start () 
    {
        startTime = Time.time;	
    }
	
	
	void Update () 
    {
        //5秒経過したか
        if(Time.time - startTime > 5f) 
        {
            // シーン切り替え
            SceneManager.LoadScene("MainScene");

        }
    }
}
