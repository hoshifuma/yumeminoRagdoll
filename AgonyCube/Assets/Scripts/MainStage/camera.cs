using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace AgonyCubeMainStage {
    public class camera : MonoBehaviour {

        //マウスの入力に応じた回転させる速度
        public Vector2 RotationSpeed;
        //y軸の1度に回転させる角度
        public float RotationRadian;
        //x軸の最大回転角度
        public float MaxXRotation;
        private Vector2 LastRotation;
        private Vector2 LastMousePosition;
        private Vector2 InputMousePosition;
        private Vector2 NewAngle = new Vector2(0, 180);
        private float XRotation = 0;
        private float YRotarion = 0;

        public float ErrorRange;
        private int HitCheck = 0;
        public LayerMask Cube;
        //現在のモードの管理　falseの場合パズルモード
        public bool CheckMode = false;
        //falseの場合９０度回転
        public bool CheckSpin = false;
        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            // UIのオブジェクトに交差している場合はHitCheck = 0;
            var horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            var vertical = CrossPlatformInputManager.GetAxis("Vertical");

            if (Mathf.Abs(horizontal) < 0.1f && Mathf.Abs(vertical) < 0.1f) {
                if (Input.GetMouseButtonDown(0)) {
                    LastRotation = NewAngle;
                    LastMousePosition = Input.mousePosition;
                    InputMousePosition = Input.mousePosition;
                    // Cubeと交差している場合
                    Ray Mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(Mouseray, 10.0f, Cube) == true) {
                        HitCheck = 0;
                    }
                    else {
                        HitCheck = 1;
                    }

                    Debug.Log("hitcheck" + HitCheck);

                }
                if (HitCheck == 1) {
                    if (Input.GetMouseButton(0)) {
                        if (CheckMode == false) {
                            //パズルモードの場合
                            if (LastRotation.y == NewAngle.y) {
                                XRotation = LastMousePosition.y - Input.mousePosition.y;
                                if (XRotation > ErrorRange || XRotation < -ErrorRange) {
                                    //マウスがy軸に一定以上の入力がある場合
                                    NewAngle.x += XRotation * RotationSpeed.x;
                                    if (NewAngle.x > MaxXRotation) {
                                        //角度が最大値より大きい場合最大値に変更
                                        NewAngle.x = MaxXRotation;
                                    }
                                    else if (NewAngle.x < -MaxXRotation) {
                                        //角度が最小値より小さい場合最小値に変更
                                        NewAngle.x = -MaxXRotation;
                                    }
                                }
                            }
                        }
                        if (CheckSpin == true) {
                            if (LastRotation.x == NewAngle.x) {
                                YRotarion = LastMousePosition.x - Input.mousePosition.x;
                                if (YRotarion > ErrorRange || YRotarion < -ErrorRange) {
                                    NewAngle.y -= YRotarion * RotationSpeed.y;
                                }
                            }
                        }
                        LastMousePosition = Input.mousePosition;
                        transform.localEulerAngles = NewAngle;
                    }

                    if (Input.GetMouseButtonUp(0)) {
                        if (CheckSpin == false) {
                            if (LastRotation.x == NewAngle.x) {
                                //x軸の角度変更がない場合
                                if (InputMousePosition.x - LastMousePosition.x > 0) {
                                    NewAngle.y -= RotationRadian;

                                    InputMousePosition.x = Input.mousePosition.x;
                                }
                                else if (InputMousePosition.x - LastMousePosition.x < 0) {
                                    NewAngle.y += RotationRadian;

                                    InputMousePosition.x = Input.mousePosition.x;
                                }
                            }
                        }
                        else {
                            if (NewAngle.y <= 360) {
                                NewAngle.y -= 360;
                            }
                            else if (NewAngle.y < 0) {
                                NewAngle.y += 360;
                            }
                            if (45 <= NewAngle.y && NewAngle.y < 135) {
                                NewAngle.y = 90;
                            }
                            else if (135 <= NewAngle.y && NewAngle.y < 225) {
                                NewAngle.y = 180;
                            }
                            else if (225 <= NewAngle.y && NewAngle.y < 315) {
                                NewAngle.y = 270;
                            }
                            else if (315 <= NewAngle.y && NewAngle.y < 45) {
                                NewAngle.y = 0;
                            }
                        }
                        transform.localEulerAngles = NewAngle;
                    }
                }
            }
            else {
                // UIに入力がある場合
                // horizontal -1.0f～1.0f
                HitCheck = 0;
            }
        }

        //『モードチェンジ』ボタンが押されたら実行
        public void ChangeMode() {
            CheckMode = !CheckMode;
            //x軸の角度を0に戻す
            NewAngle.x = 0;
            transform.localEulerAngles = NewAngle;
        }

        public void ChangeSpin() {
            CheckSpin = !CheckSpin;
        }
    }
}
