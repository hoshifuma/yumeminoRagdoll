using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public readonly static Data instance = new Data();
    //playerの初期位置を保存
    public Vector3Int playerGrid;
    //プレイするステージの名前を保存
    public string[] stageName;
    //プレイするステージの番号を保存
    public int stageNum;
    //スピンした回数を保存
    public int spin;
    //ステージのスピンの可能数を保存
    public int spinMin;
    //スワップをした回数を保存
    public int swap;
    //ステージのスワップの可能数を保存
    public int swapMin;
}
