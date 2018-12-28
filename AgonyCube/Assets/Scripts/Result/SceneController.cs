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

           int swap = Score.instance.swap;
           int swapMin = Score.instance.swapMin;
           int spin = Score.instance.spin;
           int spinMin = Score.instance.spinMin;


            // 「Star」を全部非表示に設定
            foreach (var star in stars) {
                star.SetActive(false);
            }

            var clear = PlayerPrefs.GetInt(Score.instance.stageName, 0);
            
            //swapとspinの判定
            if (swapMin < swap && spinMin < spin) {
                stars[0].SetActive(true);
                if (1 > clear) {
                    PlayerPrefs.SetInt(Score.instance.stageName, 1);
                }
            }
            else if (swapMin < swap || spinMin < spin) {
                stars[1].SetActive(true);
                stars[0].SetActive(true);
                if (2 > clear) {
                    PlayerPrefs.SetInt(Score.instance.stageName, 2);
                }
            }
            else if (swapMin >= swap || spinMin >= spin) {
                stars[2].SetActive(true);
                stars[1].SetActive(true);
                stars[0].SetActive(true);
                if (3 > clear) {
                    PlayerPrefs.SetInt(Score.instance.stageName, 3);
                }
            }


        }

        // 『はい』ボタンを押したら実行されます。
        public void OnClickNextButton()
        {
            SceneManager.LoadScene("MainScene");
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
