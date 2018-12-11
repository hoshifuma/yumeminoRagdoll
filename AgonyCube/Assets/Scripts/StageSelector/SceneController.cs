using UnityEngine;
using UnityEngine.SceneManagement;

namespace AgonyCube.StageSelector
{
    // 『ステージ選択画面』のシーン遷移を管理します。
    public class SceneController : MonoBehaviour
    {
        public int clearStageNo = -1;
        public Transform stageRoot;
        public Transform[] stageButtons;

        private void Start()
        {
            // 以下はデバッグ用の仮セーブ
            // ゲームクリア―した時にそのステージ番号までを保存する
            PlayerPrefs.SetInt("clearStageNo", 3);

            // 現在のクリアーステージ番号をロードする。
            clearStageNo = PlayerPrefs.GetInt("clearStageNo", 0);

            // clearStageNoのところまでを開放する
            stageButtons = new Transform[stageRoot.childCount];
            for (int index = 0; index < stageButtons.Length; index++)
            {
                stageButtons[index] = stageRoot.GetChild(index);
                if (index < clearStageNo)
                {
                    stageButtons[index].gameObject.SetActive(true);
                }
                else
                {
                    stageButtons[index].gameObject.SetActive(false);
                }
            }
        }

        // 『もどる』ボタンを押したら実行する
        public void OnClickReturnButton() {
            LoadScene.nextScene = "Title";
            SceneManager.LoadScene("NowLoading");
        }

        // 『Tutorial1』ボタンを押したら実行されます。
        public void OnClickABCButton() {
            LoadScene.nextScene = "Tutorial01";
            SceneManager.LoadScene("NowLoading");
        }

        // 『Tutorial2』ボタンを押したら実行されます。
        public void OnClickDEFButton()
        {
            LoadScene.nextScene = "Story1-1";
            SceneManager.LoadScene("NowLoading");
        }

        // 『ステージ２』ボタンを押したら実行されます。
        public void OnClickEventButton()
        {
            LoadScene.nextScene = "Story2-1";
            SceneManager.LoadScene("NowLoading");
        }
    }
}