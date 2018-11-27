using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip3 : MonoBehaviour {

	void Start () {
		
	}
	
	public void SkipButton()
    {
        SceneManager.LoadScene("MainScene");
	}
 }
