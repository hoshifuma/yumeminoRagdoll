using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip7 : MonoBehaviour {

	void Start () {
		
	}
	
	public void SkipButton()
    {
        SceneManager.LoadScene("Story3-1");
	}
 }
