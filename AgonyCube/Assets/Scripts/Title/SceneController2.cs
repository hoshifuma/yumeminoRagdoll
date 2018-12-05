using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AgonyCube.Title
{
    // 『タイトル画面』のシーン遷移を管理します。
    public class SceneController2 : MonoBehaviour
    {
        private void Start()
        {
            SceneManager.LoadScene("StageSelector");
        }

    }
}
