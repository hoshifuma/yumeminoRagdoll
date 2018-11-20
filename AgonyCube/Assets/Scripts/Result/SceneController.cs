using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            //Mainシーンから読み込む文字列（ステージ名）
            //char stagename[] = (MainSceneのスクリプトのクラス名).etc

            swap = 6;
            swapmin = 3;
            spin = 2;
            spinmin = 1;


            // 「Star」を全部非表示に設定
            foreach (var star in stars)
            {
                star.SetActive(false);
            }

            for (int index = 0; index < starValue; index++)
            {
                stars[index].SetActive(true);
                //swapとspinの判定
                if (swapmin + 2 < swap || spinmin + 2 < spin)
                {
                    stars[0].SetActive(true);

                }
                else if (swapmin < swap || spinmin < swap)
                {
                    stars[1].SetActive(true);
                }
                else if (swapmin == swap || spinmin == spin)
                {
                    stars[2].SetActive(true);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
