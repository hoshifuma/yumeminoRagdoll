﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainSceneChang : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    public void OnClickButton()
    {

        //シーンを変える
        SceneManager.LoadScene("MainScene");

    }
}