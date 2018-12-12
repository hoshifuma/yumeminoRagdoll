using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace AgonyCubeMainStage {
    public class CameraController : MonoBehaviour {

        //マウスの入力に応じた回転させる速度
        public Vector2 RotationSpeed;
        //x軸の最大回転角度
        public float MaxXRotation;
        //前回の角度を保存
        private Vector2 LastRotation;
        //前のフレームのマウス位置を保存
        private Vector2 LastMousePosition;
        //アングルの変更を保存
        private Vector2 newAngle = new Vector2(30, 0);
        //Y軸の次のアングル
        private float nextAngle = 0;
        //
        private float angleFlame;
        //
        public float angleSecond;
        //前のフレームと現在のマウスの入力差を保存
        private float XRotation = 0;
        private float YRotarion = 0;
        //マウス入力の誤差
        public float errorRange;
        //クリックされたのがBlockかを判定
        private int HitCheck = 0;
        //Blockのレイヤー
        public LayerMask Cube;
        //現在のモードの管理　falseの場合パズルモード
        // public bool CheckMode = false;
        //falseの場合９０度回転
        // public bool CheckSpin = false;
        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            // UIのオブジェクトに交差している場合はHitCheck = 0;
            var horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            var vertical = CrossPlatformInputManager.GetAxis("Vertical");

            if (!(Mathf.Abs(horizontal) > 0f && Mathf.Abs(vertical) > 0f)) {
                if (Input.GetMouseButtonDown(0)) {
                    LastRotation = newAngle;
                    LastMousePosition = Input.mousePosition;
                    // Cubeと交差している場合
                    Ray Mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(Mouseray, 10.0f, Cube) == true) {
                        HitCheck = 0;
                    }
                    else {
                        HitCheck = 1;
                    }



                }
                if (HitCheck == 1) {
                    if (Input.GetMouseButton(0)) {

                        //パズルモードの場合
                        if (LastRotation.y == newAngle.y) {
                            //xのマウス入力がない場合
                            //マウスのｘ入力を求める
                            XRotation = LastMousePosition.y - Input.mousePosition.y;
                            if (Mathf.Abs(XRotation) > errorRange) {
                                //マウスがyに一定以上の入力がある場合
                               
                                newAngle.x += (XRotation - errorRange) * RotationSpeed.x;
                                if (newAngle.x > MaxXRotation) {
                                    //角度が最大値より大きい場合最大値に変更
                                    newAngle.x = MaxXRotation;
                                }
                                else if (newAngle.x < -MaxXRotation) {
                                    //角度が最小値より小さい場合最小値に変更
                                    newAngle.x = -MaxXRotation;
                                }
                            }
                        }

                        // if (CheckSpin == true) {
                        if (LastRotation.x == newAngle.x) {
                            //yのマウス入力がない場合
                            //マウスのy入力を求める
                            YRotarion = LastMousePosition.x - Input.mousePosition.x;
                            if (Mathf.Abs(YRotarion) > errorRange) {
                                //マウス入力が誤差より大きい場合
                                angleFlame = 0;
                                //newAngle.yを変更
                                newAngle.y -= YRotarion * RotationSpeed.y;
                            }
                        }
                        //  }
                        
                        LastMousePosition = Input.mousePosition;
                      
                    }

                    
                }
            }
            else {
                // UIに入力がある場合
                // horizontal -1.0f～1.0f
                HitCheck = 0;
            }

            if (Input.GetMouseButtonUp(0)) {

                if (newAngle.y > 360) {

                    while (newAngle.y > 360) {
                        newAngle.y -= 360;
                    }
                }
                else if (newAngle.y < 0) {

                    while (newAngle.y < 0) {
                        newAngle.y += 360;
                    }
                }

                Debug.Log(newAngle.y);
                if (45 <= newAngle.y && newAngle.y < 135) {
                    nextAngle = 90;
                    angleFlame = nextAngle - newAngle.y;
                    angleFlame /= angleSecond;
                }
                else if (135 <= newAngle.y && newAngle.y < 225) {
                    nextAngle = 180;
                    angleFlame = nextAngle - newAngle.y;
                    angleFlame /= angleSecond;
                }
                else if (225 <= newAngle.y && newAngle.y < 315) {
                    nextAngle = 270;
                    angleFlame = nextAngle - newAngle.y;
                    angleFlame /= angleSecond;
                }
                else {
                    nextAngle = 0;
                    if (newAngle.y > 45) {
                        newAngle.y -= 360;
                    }
                    angleFlame = nextAngle - newAngle.y;
                    angleFlame /= angleSecond;
                }
                // }
            }
            if (Mathf.Abs(newAngle.y - nextAngle) < 0.5) {
                newAngle.y = nextAngle;
                angleFlame = 0;
            }
            else {
                newAngle.y += angleFlame;
            }

            transform.localEulerAngles = newAngle;
        }

        
    }
}
