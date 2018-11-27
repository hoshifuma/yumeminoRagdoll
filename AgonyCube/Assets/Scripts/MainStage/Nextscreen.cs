using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nextscreen : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("ゴール");
            SceneManager.LoadScene("Story1-2");
        }
    }

}