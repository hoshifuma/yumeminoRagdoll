using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip10 : MonoBehaviour {

	void Start () {
		
	}
	
	public void SkipButton()
    {
        SceneManager.LoadScene("Story4-2");
	}
 }
