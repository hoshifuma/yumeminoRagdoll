using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public static string nextScene;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(LoadNextScene());
	}

    IEnumerator LoadNextScene()
    { 

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(nextScene);
    }
}
