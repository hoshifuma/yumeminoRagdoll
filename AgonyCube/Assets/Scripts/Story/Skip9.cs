using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip9 : MonoBehaviour {

	void Start () {
		
	}
	
	public void SkipButton()
    {
        SceneManager.LoadScene("Story4-1");
	}
 }
