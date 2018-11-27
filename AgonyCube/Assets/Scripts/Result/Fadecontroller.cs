using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fadecontroller : MonoBehaviour
{

    float FadeSpeed = 0.02f;        //透明度が変わるスピードを管理
    float Red, Green, Blue, Alfa;   //パネルの色、不透明度を管理

    public bool isFadeOut = false;  //フェードアウト処理の開始、完了を管理するフラグ

    Image FadeImage;                //透明度を変更するパネルのイメージ

    void Start()
    {
        FadeImage = GetComponent<Image>();
        Red = FadeImage.color.r;
        Green = FadeImage.color.g;
        Blue = FadeImage.color.b;
        Alfa = FadeImage.color.a;
    }

    void Update()
    {
        if (isFadeOut)
        {
            StartFadeOut();
        }
    }

    void StartFadeOut()
    {
        FadeImage.enabled = true;  // a)パネルの表示をオンにする
        Alfa += FadeSpeed;         // b)不透明度を徐々にあげる
        SetAlpha();               // c)変更した透明度をパネルに反映する
        if (Alfa >= 1)
        {             // d)完全に不透明になったら処理を抜ける
            isFadeOut = false;
        }
    }

    void SetAlpha()
    {
        FadeImage.color = new Color(Red, Green, Blue, Alfa);
    }
}