using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapAnimation : MonoBehaviour {

    //生成するタップエフェクトのprefabをしてします。
    [SerializeField]
    GameObject tapEffect;


    // Update is called once per frame
    void Update()
    {
        //画面上でタップした場合、以下の処理を行います。
        if (Input.GetMouseButtonDown(0)) {
            // 変数"position"にマウスの位置座標を取得します。
            var position = Input.mousePosition;
            // Z軸の修正
            position.z = 10f;
            // マウスの位置座標をスクリーン座標からワールド座標に変換します。
            var tapPosition = Camera.main.ScreenToWorldPoint(position);
            // 指定したタップエフェクトのprefabを生成します。
            var tapEffectObject = Instantiate(tapEffect, tapPosition, tapEffect.transform.rotation);
            // 生成したprefabを1.5秒後に削除します。
            Destroy(tapEffectObject, 1.5f);
        }
    }
}
