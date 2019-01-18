﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
            Data.instance.stageName = new string[content.transform.childCount];
            int i = 0;
            foreach(Transform child in content.transform) {
                Debug.Log( child.gameObject.name);
               Data.instance.stageName[i] = child.gameObject.name;

                Debug.Log(Data.instance.stageName[i]);
                var starNum = PlayerPrefs.GetInt(Data.instance.stageName[i], 0);
               
                i += 1;
                int index = 0;
                foreach(Transform star in child.transform) {

                    if (index <= starNum) {
                        star.gameObject.SetActive(true);
                    }
                    index += 1;
                }
                
            }
            
            
            if (PlayerPrefs.GetInt("clearStageNo", 0) == 0) {
                PlayerPrefs.SetInt("clearStageNo", 1);
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
        public void OnClickStage01() {
            Data.instance.stageNum = 0;
         
            LoadScene.nextScene = "Tutorial01";
            StartCoroutine("NextScene");

        }

        // 『ステージ2』ボタンを押したら実行されます。
        public void OnClickStage02()
        {
            Data.instance.stageNum = 1;
            LoadScene.nextScene = "MainScene";
            StartCoroutine("NextScene");
        }

        // 『ステージ3』ボタンを押したら実行されます。
        public void OnClickStage03()
        {
            Data.instance.stageNum = 2;
            LoadScene.nextScene = "MainScene";
            StartCoroutine("NextScene");
        }

        // 『ステージ4』ボタンを押したら実行されます。
        public void OnClickStage04()
        {
            Data.instance.stageNum = 3;
            LoadScene.nextScene = "MainScene";
            StartCoroutine("NextScene");
        }

        // 『ステージ5』ボタンを押したら実行されます。
        public void OnClickStage05()
        {
            Data.instance.stageNum = 4;
            LoadScene.nextScene = "MainScene";
            StartCoroutine("NextScene");
        }

        // 『ステージ6』ボタンを押したら実行されます。
        public void OnClickStage06()
        {
            Data.instance.stageNum = 5;
            LoadScene.nextScene = "MainScene";
            StartCoroutine("NextScene");
        }

        // 『ステージ7』ボタンを押したら実行されます。
        public void OnClickStage07()
        {
            Data.instance.stageNum = 6;
            LoadScene.nextScene = "MainScene";
            StartCoroutine("NextScene");
        }

        // 『ステージ8』ボタンを押したら実行されます。
        public void OnClickStage08()
        {
            Data.instance.stageNum = 7;
            LoadScene.nextScene = "MainScene";
            StartCoroutine("NextScene");
        }

        // 『ステージ9』ボタンを押したら実行されます。
        public void OnClickStage09()
        {
            Data.instance.stageNum = 8;
            LoadScene.nextScene = "MainScene";
            StartCoroutine("NextScene");


        }

        // 『ステージ10』ボタンを押したら実行されます。
        public void OnClickStage10()
        {
            Data.instance.stageNum = 9;
            LoadScene.nextScene = "MainScene";
            StartCoroutine("NextScene");
            
        }

        IEnumerator NextScene()
        {
            
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("NowLoading");
        }
    }
}