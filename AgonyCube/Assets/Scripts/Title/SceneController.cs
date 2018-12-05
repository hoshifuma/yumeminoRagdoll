using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AgonyCube.Title
{
    // 『タイトル画面』のシーン遷移を管理します。
    public class SceneController : MonoBehaviour
    {
        // 画面をタップしたら実行されます。
        public void OnClickTAPButton() {
            SceneManager.LoadScene("NowLoading");
        }
    }
}
