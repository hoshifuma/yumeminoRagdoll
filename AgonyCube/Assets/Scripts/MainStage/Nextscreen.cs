using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AgonyCube.MainStage
{
    public class Nextscreen : MonoBehaviour {

        [SerializeField]
        string nextScene = "Story1-2";


        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Player")) {
                Debug.Log("ゴール");
                //[Player]のStateChange
                other.GetComponent<PlayerController>().GetHeart();
                StartCoroutine(EnterGoal());
            }
        }
        
        IEnumerator EnterGoal() {
            yield return new WaitForSeconds(3.0f);
            SceneManager.LoadScene(nextScene);
        }

    }
}