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
        public GameObject content;
        public string[] stageName;

        private void Start()
        {
            stageName = new string[content.transform.childCount];
            int i = 0;
            foreach(Transform child in content.transform) {
                stageName[i] = child.gameObject.name;

                Debug.Log(stageName[i]);
                var starNum = PlayerPrefs.GetInt(stageName[i], 0);
                Debug.Log(PlayerPrefs.GetInt(stageName[i], 0));
                i += 1;
                int index = 0;
                foreach(Transform star in child.transform) {

                    if (index < starNum) {
                        star.gameObject.SetActive(true);
                    }
                    index += 1;
                }
                
            }
            
            
            if (PlayerPrefs.GetInt("clearStageNo", 0) == 0) {
                PlayerPrefs.SetInt("clearStageNo", 2);
            }
            // 以下はデバッグ用の仮セーブ
            // ゲームクリア―した時にそのステージ番号までを保存する
            //PlayerPrefs.SetInt("clearStageNo", 3);

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

        // 『ステージ1』ボタンを押したら実行されます。
        public void OnClickABCButton() {
            Score.instance.stageName = stageName[0];
            Debug.Log(stageName[0]);
            LoadScene.nextScene = stageName[0];
            LoadScene.nextScene = "Story1-1";
            SceneManager.LoadScene("NowLoading");
        }

        // 『ステージ2』ボタンを押したら実行されます。
        public void OnClickDEFButton()
        {
            Score.instance.stageName = stageName[1];
            Debug.Log(stageName[1]);
            LoadScene.nextScene = stageName[1];
            LoadScene.nextScene = "tutorial01";
            SceneManager.LoadScene("NowLoading");
        }

        // 『ステージ3』ボタンを押したら実行されます。
        public void OnClickEventButton()
        {
            Score.instance.stageName = stageName[2];
            Debug.Log(stageName[2]);
            LoadScene.nextScene = "Story2-1";
            SceneManager.LoadScene("NowLoading");
        }
    }
}