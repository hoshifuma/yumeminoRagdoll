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
        
        // Use this for initialization
        void Start()
        {
            // 「Star」を全部非表示に設定
            foreach(var star in stars) {
                star.SetActive(false);
            }
            for (int index = 0; index < starValue; index++) {
                stars[index].SetActive(true);
            }
            //stars[0].SetActive(true);
            //stars[1].SetActive(true);
            //stars[2].SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
