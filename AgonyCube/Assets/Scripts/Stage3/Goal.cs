using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AgonyCube.Title
{
    // 『タイトル画面』のシーン遷移を管理します。
    public class Goal : MonoBehaviour
    {
        // 画面をタップしたら実行されます。
        void Update()
        {
            if (Input.GetKey("space")) {
                SceneManager.LoadScene("TestResult");
            }
        }
    }
}