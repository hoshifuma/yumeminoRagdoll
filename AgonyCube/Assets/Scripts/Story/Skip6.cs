using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip6 : MonoBehaviour {

	void Start () {
		
	}
	
	public void SkipButton()
    {
        SceneManager.LoadScene("Story2-3");
	}
 }
