using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip8 : MonoBehaviour {

	void Start () {
		
	}
	
	public void SkipButton()
    {
        SceneManager.LoadScene("Story3-2");
	}
 }
