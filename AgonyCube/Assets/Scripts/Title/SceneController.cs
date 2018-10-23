using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AgonyCube.Title
{
    // 『タイトル画面』のシーン遷移を管理します。
    public class SceneController : MonoBehaviour
    {
        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        // 『START』ボタンを押したら実行されます。
        public void OnClickStartButton() {
            SceneManager.LoadScene("MainScene");
        }

        // 『SELECT』ボタンを押したら実行されます。
        public void OnClickSelectButton() {
            SceneManager.LoadScene("StageSelector");
        }
    }
}
