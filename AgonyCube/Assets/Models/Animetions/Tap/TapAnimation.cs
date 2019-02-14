using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapAnimation : MonoBehaviour {

    //生成するタップエフェクトのprefabをしてします。
    [SerializeField]
    GameObject tapEffect;
    [SerializeField]
    GameObject cameraRoot;

    GameObject tapEffectObject;
    private void Start() {
        if (cameraRoot == null) {
            cameraRoot = gameObject;
            var position = Camera.main.transform.localPosition;
            position.z = 20;
            cameraRoot.transform.localPosition = position;
        }
    }
    // Update is called once per frame
    void Update() {
        //画面上でタップした場合、以下の処理を行います。
        if (Input.GetMouseButtonDown(0)) {
            // 変数"position"にマウスの位置座標を取得します。
            var position = Input.mousePosition;

            // Z軸の修正
            //ステージ中心点とメインカメラの中央に生成するように変更　19/2/14 hf
            position.z = (cameraRoot.transform.localPosition.z - Camera.main.transform.localPosition.z) / 2;

            // マウスの位置座標をスクリーン座標からワールド座標に変換します。
            var tapPosition = Camera.main.ScreenToWorldPoint(position);

            if (tapEffectObject != null) {
                Destroy(tapEffectObject);
            }
            // 指定したタップエフェクトのprefabを生成します。
            //ステージのの中心点からのカメラの向きに応じてエフェクトの角度を変更するように変更　19/2/14 hf
            tapEffectObject = Instantiate(tapEffect, tapPosition, cameraRoot.transform.localRotation);

            // 生成したprefabを1.5秒後に削除します。
           Destroy(tapEffectObject, 1.5f);
        }
        if (Input.GetMouseButton(0)) {
            if (tapEffectObject != null) {
                var position = Input.mousePosition;
                position.z = (cameraRoot.transform.localPosition.z - Camera.main.transform.localPosition.z) / 2;
                position = Camera.main.ScreenToWorldPoint(position);
               
                tapEffectObject.transform.localPosition = position;
                tapEffectObject.transform.localRotation = cameraRoot.transform.localRotation;
            }
        }
    }
}
