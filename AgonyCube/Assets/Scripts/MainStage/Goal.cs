using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AgonyCube.MainStage
{
    //『ゴール』時の操作を表します。
    public class Goal : MonoBehaviour
    {

        //入れ替えるプレイヤーオブジェクトを指定します。
        [SerializeField]
        GameObject player;
        [SerializeField]
        GameObject playerGoal;

        [SerializeField]
        ZoomCamera zoomCamera;
        //遷移するストーリー名を指定します。
        [SerializeField]
        string nextScene = "Story1-2";

        public GameDirector gameDirector;
        //Playerタグを持つオブジェクトが衝突した際の処理を表します。
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player")) {
                Debug.Log("ゴール");
                Score.instance.spin = gameDirector.spin;
                Score.instance.swap = gameDirector.swap;

                //[Player]のStateChange
                other.GetComponent<PlayerController>().GetHeart();

                //元のオブジェクトを非表示にします。
                player.transform.gameObject.SetActive(false);


                var renderers = GetComponentsInChildren<Renderer>();
                foreach (var renderer in renderers) {
                    renderer.enabled = false;
                }

                //入れ替えるオブジェクトの生成位置、回転を指定します。
                Vector3 goalPosition = player.transform.position;
                Quaternion q = new Quaternion(0, 180, 0, 0);

                zoomCamera.StartGoalMotion();
                //生成した後にコルーチンを呼び出します。
                Instantiate(playerGoal, goalPosition, q);
                StartCoroutine(EnterGoal());
            }
        }

        //次のシーンへ移る遷移処理を表します。
        IEnumerator EnterGoal()
        {
            //３秒後に
            yield return new WaitForSeconds(3.0f);
            SceneManager.LoadScene(nextScene);
        }

    }
}