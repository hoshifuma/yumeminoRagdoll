using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip11 : MonoBehaviour {

	void Start () {
		
	}
	
	public void SkipButton()
    {
        SceneManager.LoadScene("Ending");
	}
 }
