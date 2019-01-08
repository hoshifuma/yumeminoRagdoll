using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace AgonyCube.Result
{
    public class SceneController : MonoBehaviour
    {

        public int starValue = 3;
        // 「Star」を左から順番に指定します。
        public GameObject[] stars = new GameObject[3];

        


        // Use this for initialization
        void Start()
        {
            //Mainシーンから読み込むスワップ数
            //int swap_num = (MainSceneのスクリプトのクラス名).swap;

            //Mainシーンから読み込むスピン数
            //int spin_num = (MainSceneのスクリプトのクラス名).spin;

           int swap = Data.instance.swap;
           int swapMin = Data.instance.swapMin;
           int spin = Data.instance.spin;
           int spinMin = Data.instance.spinMin;


            // 「Star」を全部非表示に設定
            foreach (var star in stars) {
                star.SetActive(false);
            }
            Debug.Log(PlayerPrefs.GetInt("clearStageNo", 0));
            Debug.Log(Data.instance.stageNum);
            if(PlayerPrefs.GetInt("clearStageNo", 0) <= Data.instance.stageNum + 1) {
                PlayerPrefs.SetInt("clearStageNo", Data.instance.stageNum + 2);
            }
            var clear = PlayerPrefs.GetInt(Data.instance.stageName[Data.instance.stageNum], 0);
            
            //swapとspinの判定
            if (swapMin < swap && spinMin < spin) {
                stars[0].SetActive(true);
                if (1 > clear) {
                    PlayerPrefs.SetInt(Data.instance.stageName[Data.instance.stageNum], 1);
                }
            }
            else if (swapMin < swap || spinMin < spin) {
                stars[1].SetActive(true);
                stars[0].SetActive(true);
                if (2 > clear) {
                    PlayerPrefs.SetInt(Data.instance.stageName[Data.instance.stageNum], 2);
                }
            }
            else if (swapMin >= swap && spinMin >= spin) {
                stars[2].SetActive(true);
                stars[1].SetActive(true);
                stars[0].SetActive(true);
                if (3 > clear) {
                    PlayerPrefs.SetInt(Data.instance.stageName[Data.instance.stageNum], 3);
                }
            }


        }

        // 『はい』ボタンを押したら実行されます。
        public void OnClickNextButton()
        {
            SceneManager.LoadScene("MainScene");
            Data.instance.stageNum++;
        }

        public void OnClickSelectButton()
        {
            SceneManager.LoadScene("StageSelector");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
