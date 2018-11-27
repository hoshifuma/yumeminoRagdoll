using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipE : MonoBehaviour {

	void Start () {
		
	}
	
	public void SkipButton()
    {
        SceneManager.LoadScene("Title");
	}
 }
