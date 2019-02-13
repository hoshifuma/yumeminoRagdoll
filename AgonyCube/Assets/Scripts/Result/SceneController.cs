using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace AgonyCube.Result {
    public class SceneController : MonoBehaviour {

        public GameObject nextStage;

        public int starValue = 3;

        // 「Star」を左から順番に指定します。
        public GameObject[] stars = new GameObject[3];



        // Use this for initialization
        void Start() {
            //Mainシーンから読み込むスワップ数
            //int swap_num = (MainSceneのスクリプトのクラス名).swap;

            //Mainシーンから読み込むスピン数
            //int spin_num = (MainSceneのスクリプトのクラス名).spin;

            int swap = Data.instance.swap;
            int swapMin = Data.instance.swapMin;
            int spin = Data.instance.spin;
            int spinMin = Data.instance.spinMin;

            int stageNum = Data.instance.stageNum;

            if (PlayerPrefs.GetInt("clearStageNo", 0) <= stageNum + 1) {
                PlayerPrefs.SetInt("clearStageNo", stageNum + 2);
            }
            if (Data.instance.stageName == null) {
                Data.instance.stageName = new string[1];
                Data.instance.stageName[stageNum] = "test";
                PlayerPrefs.SetInt(Data.instance.stageName[stageNum], 2);
            }

            if (Data.instance.stageName.Length == stageNum + 1) {
                nextStage.SetActive(false);
            }

            var clear = PlayerPrefs.GetInt(Data.instance.stageName[stageNum], 0);

            //swapとspinの判定
            if (swapMin < swap && spinMin < spin) {
                stars[0].SetActive(true);

                if (1 > clear) {
                    PlayerPrefs.SetInt(Data.instance.stageName[stageNum], 1);
                }
            }
            else if (swapMin < swap || spinMin < spin) {
                stars[1].SetActive(true);
                stars[0].SetActive(true);

                if (2 > clear) {
                    PlayerPrefs.SetInt(Data.instance.stageName[stageNum], 2);
                }
            }
            else if (swapMin >= swap && spinMin >= spin) {
                stars[2].SetActive(true);
                stars[1].SetActive(true);
                stars[0].SetActive(true);

                if (3 > clear) {
                    PlayerPrefs.SetInt(Data.instance.stageName[stageNum], 3);
                }
            }


        }

        // 『はい』ボタンを押したら実行されます。
        public void OnClickNextButton() {
            Data.instance.stageNum++;
            SceneManager.LoadScene("MainScene");
            
        }

        public void OnClickSelectButton() {
            SceneManager.LoadScene("StageSelector");
        }

        // Update is called once per frame
        void Update() {

        }
    }
}
