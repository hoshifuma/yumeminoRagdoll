
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PauseScript3 : MonoBehaviour
    {

        //　アイテムメニューを開くボタン
        [SerializeField]
        private GameObject optionButton;
        //　ゲーム再開ボタン
        [SerializeField]
        private GameObject reStartButton;
        //　アイテムメニューパネル
        [SerializeField]
        private GameObject itemPanel;

        public void StopGame()
        {
            Time.timeScale = 0f;
            optionButton.SetActive(false);
            reStartButton.SetActive(true);
            itemPanel.SetActive(true);
        }

        public void ReStartGame()
        {
            itemPanel.SetActive(false);
            reStartButton.SetActive(false);
            optionButton.SetActive(true);
            Time.timeScale = 1f;
        }
    }