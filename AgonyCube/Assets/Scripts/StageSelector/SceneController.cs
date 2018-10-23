using UnityEngine;
using UnityEngine.SceneManagement;

namespace AgonyCube.StageSelector
{
    // 『ステージ選択画面』のシーン遷移を管理します。
    public class SceneController : MonoBehaviour
    {
        // 『RETURN』ボタンを押したら実行する
        public void OnClickReturnButton() {
            SceneManager.LoadScene("Title");
        }
    }
}