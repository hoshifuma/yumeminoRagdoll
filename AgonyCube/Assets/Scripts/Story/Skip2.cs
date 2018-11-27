using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip2 : MonoBehaviour {

	void Start () {
		
	}
	
	public void SkipButton()
    {
        SceneManager.LoadScene("Result");
	}
 }
