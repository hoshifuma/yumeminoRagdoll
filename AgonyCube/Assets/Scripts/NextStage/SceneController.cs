using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // 『はい』ボタンを押したら実行されます。
    public void OnClickYesButton()
    {
        SceneManager.LoadScene("Stage2");
    }

    // 『いいえ』ボタンを押したら実行されます。
    public void OnClickNoButton()
    {
        SceneManager.LoadScene("StageSelector");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
