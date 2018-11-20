using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip : MonoBehaviour {

	void Start () {
		
	}
	
	public void SkipButton()
    {
        SceneManager.LoadScene("Tutorial02");
	}
 }
