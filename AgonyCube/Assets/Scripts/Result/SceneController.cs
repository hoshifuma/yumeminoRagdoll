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

        int swap = 0;
        int swapmin = 0;
        int spin = 0;
        int spinmin = 0;


        // Use this for initialization
        void Start()
        {
            //Mainシーンから読み込むスワップ数
            //int swap_num = (MainSceneのスクリプトのクラス名).swap;

            //Mainシーンから読み込むスピン数
            //int spin_num = (MainSceneのスクリプトのクラス名).spin;

            swap = Score.instance.swap;
            swapmin = 3;
            spin = Score.instance.spin;
            spinmin = 1;


            // 「Star」を全部非表示に設定
            foreach (var star in stars) {
                star.SetActive(false);
            }

            var clear = PlayerPrefs.GetInt(Score.instance.stageName, 1);

            if(clear == 0) {
               var clearStageNo = PlayerPrefs.GetInt("clearStageNo", 0);
                clearStageNo += 1;
                PlayerPrefs.SetInt("clearStageNo", clearStageNo);
            }
            //swapとspinの判定
            if (swapmin < swap && spinmin < spin) {
                stars[0].SetActive(true);
                PlayerPrefs.SetInt(Score.instance.stageName, 1);
            }
            else if (swapmin < swap || spinmin < spin) {
                stars[1].SetActive(true);
                stars[0].SetActive(true);
                PlayerPrefs.SetInt(Score.instance.stageName, 2);
                
            }
            else if (swapmin >= swap || spinmin >= spin) {
                stars[2].SetActive(true);
                stars[1].SetActive(true);
                stars[0].SetActive(true);
                PlayerPrefs.SetInt(Score.instance.stageName, 3);
               
            }


        }

        // 『はい』ボタンを押したら実行されます。
        public void OnClickTAPButton()
        {
            SceneManager.LoadScene("NextStage");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
