using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip4 : MonoBehaviour {

	void Start () {
		
	}
	
	public void SkipButton()
    {
        SceneManager.LoadScene("Result");
	}
 }
