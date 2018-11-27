using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip5 : MonoBehaviour {

	void Start () {
		
	}
	
	public void SkipButton()
    {
        SceneManager.LoadScene("Story2-2");
	}
 }
