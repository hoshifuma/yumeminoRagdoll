using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace AgonyCubeMainStage {
    public class GameDirector : MonoBehaviour {

        public LayerMask cube;
        public LayerMask wall;
        public GameObject choice1;
        public GameObject choice2;
        public GameObject player;
        public GameObject mainCamera;
        //public GameObject PlayerMovecolliders;
        //private GameObject Step;
        public StageController stage;
        //public bool CheckMode = false;

        GameState currentState = null;


        private class MainScene : GameState {
            protected GameDirector gameDirector;

            public MainScene(GameDirector gameDirector) {
                this.gameDirector = gameDirector;
            }
        }
        //通常状態
        private class IdleState : MainScene {
            //クリックされている時間を保存する変数
            float tapTime = 0;
            public IdleState(GameDirector gameDirector) : base(gameDirector) {

            }
            public override void Start() {

            }

            public override void Update() {
                if (Input.GetMouseButtonDown(0)) {
                    //マウスがクリックされた時にクリックされたBlockを保存
                    gameDirector.choice1 = gameDirector.CheckBlockClick();
                }

                if (Input.GetMouseButton(0)) {
                    if (gameDirector.choice1 != null) {
                        gameDirector.choice1 = gameDirector.CheckBlockClick();
                        //マウスがクリックされたときにBlockが保存されていた場合
                        tapTime += Time.deltaTime;
                        if (tapTime > 1) {
                            //ボタンが押され続けて一定時間経過された場合
                            gameDirector.ChangeState(new SpinState(gameDirector));
                        }
                    }
                }

                if (Input.GetMouseButtonUp(0)) {
                    //tapTimeが一定値を超える前にマウスのボタンが離された場合
                    if (gameDirector.choice1 != null) {
                        gameDirector.ChangeState(new SwapState(gameDirector));
                    }


                }
            }


        }
        //Swapモード
        private class SwapState : MainScene {
            //クリック開始時のマウス位置を保存する変数
            Vector3 startMousePosi;
            //マウスを離す直前のマウス位置を保存する変数
            Vector3 lastMousePosi;
            public SwapState(GameDirector gameDirector) : base(gameDirector) {

            }
            public override void Start() {
                //Swapする段以外のBlockを非表示
                gameDirector.stage.InvisibleBlock(gameDirector.choice1);
                gameDirector.choice1 = null;
                
            }

            public override void Update() {
                if (Input.GetMouseButtonDown(0)) {
                    //クリック開始時のマウスポジションを保存
                    startMousePosi = Input.mousePosition;
                    startMousePosi = Camera.main.ScreenToViewportPoint(startMousePosi);
                    if (gameDirector.choice1 == null) {
                        gameDirector.choice1 = gameDirector.CheckBlockClick();
                        if (gameDirector.choice1 != null) {
                            //Blockがクリックされていた場合選択状態に変更
                            gameDirector.choice1.GetComponent<LineRenderer>().enabled = true;
                        }
                    }
                    else {
                        if (gameDirector.SwapCube()) {
                            //Swapが成功した場合
                            gameDirector.ChangeState(new IdleState(gameDirector));
                        }
                    }
                }
                else if (Input.GetMouseButton(0)) {
                    //最新のマウスポジションを保存
                    lastMousePosi = Input.mousePosition;

                    lastMousePosi = Camera.main.ScreenToViewportPoint(lastMousePosi);
                }
                else if (Input.GetMouseButtonUp(0)) {
                    //マウス入力の差を求める
                    var mousePosi = lastMousePosi - startMousePosi;

                    mousePosi = new Vector2(Mathf.Abs(mousePosi.x), Mathf.Abs(mousePosi.y));
                    Debug.Log(mousePosi);
                    if (gameDirector.choice1 == null) {
                        if (mousePosi.x < 0.01 && mousePosi.y < 0.01) {
                            //マウスの入力がクリック時から変更がない場合
                            gameDirector.ChangeState(new IdleState(gameDirector));
                        }
                    }
                }
            }

            public override void Exsit() {
                gameDirector.stage.UnInvisibleBlock();
                if(gameDirector.choice1 != null) {
                    gameDirector.choice1.GetComponent<LineRenderer>().enabled = false;
                    gameDirector.choice1 = null;
                }
            }
        }


        //Spinモード
        private class SpinState : MainScene {
            //SpinState開始時のマウス位置を保存する変数
            Vector3 startMousePosi;
            //マウスを離す直前のマウス位置を保存する変数
            Vector3 lastMousePosi;
            //クリックされているBlockの面を保存
            GameObject wall;
            public SpinState(GameDirector gameDirector) : base(gameDirector) {

            }

            public override void Start() {
                //初期のマウスポジションを保存
                startMousePosi = Input.mousePosition;
                startMousePosi = Camera.main.ScreenToViewportPoint(startMousePosi);
                wall = gameDirector.CheckWallClick();
            }

            public override void Update() {
                if (Input.GetMouseButton(0)) {
                   
                    Debug.Log(wall);
                    //最新のマウスポジションを保存
                    lastMousePosi = Input.mousePosition;
                    lastMousePosi = Camera.main.ScreenToViewportPoint(lastMousePosi);
                }

                if (Input.GetMouseButtonUp(0)) {
                    //マウス入力の差を求める
                    var mousePosi = lastMousePosi - startMousePosi;

                    mousePosi = new Vector2(Mathf.Abs(mousePosi.x), Mathf.Abs(mousePosi.y));
                    Debug.Log("b" + mousePosi);
                    if (mousePosi.x > 0.1 || mousePosi.y > 0.1) {
                        //マウス入力の差が上下の場合
                        if (mousePosi.x < mousePosi.y) {
                            Debug.Log("y");
                            if(wall.tag == "RightAndLeft") {
                                gameDirector.stage.ZSpinBlock(gameDirector.choice1);
                            }else if(wall.tag == "FrontAndBehind") {
                                gameDirector.stage.XSpinBlock(gameDirector.choice1);
                            }else if(wall.tag == "UpAndDown") {
                                if(gameDirector.mainCamera.transform.localEulerAngles.y % 180 == 0) {
                                    gameDirector.stage.XSpinBlock(gameDirector.choice1);
                                }
                                else {
                                    gameDirector.stage.ZSpinBlock(gameDirector.choice1);
                                }
                            }
                            
                        }
                        else if (mousePosi.x > mousePosi.y) {
                            //マウス入力の差が左右の場合
                            if (wall.tag == "RightAndLeft") {
                                gameDirector.stage.YSpinBlock(gameDirector.choice1);
                            }
                            else if (wall.tag == "FrontAndBehind") {
                                gameDirector.stage.YSpinBlock(gameDirector.choice1);
                            }
                            else if (wall.tag == "UpAndDown") {
                                if (gameDirector.mainCamera.transform.localEulerAngles.y % 180 == 0) {
                                    gameDirector.stage.ZSpinBlock(gameDirector.choice1);
                                }
                                else {
                                    gameDirector.stage.XSpinBlock(gameDirector.choice1);
                                }
                            }
                        }
                    }
                    gameDirector.ChangeState(new IdleState(gameDirector));
                }
            }

            public override void Exsit() {
                gameDirector.choice1 = null;
                gameDirector.stage.UpdateGridData();
            }
        }

        private class PlayerMoveState : MainScene {
            public PlayerMoveState(GameDirector gameDirector) : base(gameDirector) {

            }

        }

        void ChangeState(GameState newState) {
            if (currentState != null) {
                currentState.Exsit();
            }
            currentState = newState;
            currentState.Start();
        }

        // Use this for initialization
        void Start() {
            ChangeState(new IdleState(this));
        }



        // Update is called once per frame
        void Update() {
            if (currentState != null) {
                currentState.Update();
            }
        }


        //クリックされたものがBlockの場合クリックされたBlockを返す
        private GameObject CheckBlockClick() {
            Ray mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(mouseray, out hit, 10.0f, cube)) {
                if (Vector3.Distance(player.transform.position, hit.transform.gameObject.transform.position) >= 1.5) {
                    //PlayerがいないblockのBlockを返す
                    return hit.transform.gameObject;
                }
            }
            return null;
        }

        private GameObject CheckWallClick() {
            Ray mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(mouseray, out hit, 10.0f, wall)) {
                return hit.transform.gameObject;
            }
            return null;
        }
        //キューブのSwap機能用の関数 Swapできた場合trueを返す
        private bool SwapCube() {
            Ray mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;


            //rayがキューブに当たった際に起動
            if (Physics.Raycast(mouseray, out hit, 10.0f, cube)) {
                //rayが当たったキューブがplayerのいるキューブじゃないことを確認
                if (Vector3.Distance(player.transform.position, hit.transform.position) >= 1.5) {
                    choice2 = hit.transform.gameObject;
                    //選択されたものが同じものだった場合選択状態を解除し変数を初期状態に変更
                    if (choice1 == choice2) {
                        choice1.GetComponent<LineRenderer>().enabled = false;
                        choice1 = null;
                        choice2 = null;
                    }
                    else {
                        //選択された2つのキューブが隣り合っていた場合positionの交換をし選択状態の解除、変数の初期化
                        for (int index = 0; index < 4; index++) {
                            if (choice1.GetComponent<Block>().adjacentBlock[index] != null) {

                                if (choice1.GetComponent<Block>().adjacentBlock[index].blockNumber == choice2.GetComponent<Block>().blockNumber) {
                                    Vector3 pos1 = choice1.transform.position;
                                    Vector3 pos2 = choice2.transform.position;
                                    choice1.transform.position = pos2;
                                    choice2.transform.position = pos1;
                                    choice2 = null;
                                    stage.UpdateGridData();
                                    return true;
                                }
                            }
                        }
                        choice2 = null;
                    }
                }
            }
            return false;
        }
        //playerの移動用の関数
        //private void PlayerMove(GameObject target) {

        //    bool movecheck = false;
        //    if (target.gameObject.tag == "Block") {
        //        movecheck = target.GetComponent<CubeController>().MoveCheck();
        //    }
        //    else if (target.gameObject.tag == "Clear") {
        //        movecheck = target.GetComponent<ClearController>().ClearMoveCheck();
        //    }
        //    else if (target.gameObject.tag == "Step") {
        //        movecheck = target.GetComponent<StepController>().StepMoveCheck();
        //    }

        //    if (movecheck == true) {

        //        player.GetComponent<PlayerController>().SetPlayerTarget(target.gameObject);
        //        if (target.gameObject.tag == "Step") {
        //            Step = target;
        //        }

        //    }

        //}

        //public void ChangeMode() {
        //    CheckMode = !CheckMode;
        //    if (!(choice1 == null)) {
        //        choice1.GetComponent<LineRenderer>().enabled = false;
        //        choice1 = null;
        //    }

        //}

        //public void CameraRootPlayerMoveChanger(int input) {
        //    if (mainCamera.transform.localEulerAngles.y == 90) {
        //        Debug.Log(mainCamera.transform.localEulerAngles.y);
        //        if (input == 0) {
        //            Vector3 pos = Player.transform.position;
        //            pos.y += 1;
        //            pos.x += 2;
        //            PlayerMovecolliders.transform.position = pos;
        //        }
        //        else if (input == 1) {
        //            Vector3 pos = Player.transform.position;
        //            pos.y += 1;
        //            pos.x -= 2;
        //            PlayerMovecolliders.transform.position = pos;
        //        }
        //        else if (input == 2) {
        //            Vector3 pos = Player.transform.position;
        //            pos.y += 1;
        //            pos.z -= 2;
        //            PlayerMovecolliders.transform.position = pos;
        //        }
        //        else if (input == 3) {
        //            Vector3 pos = Player.transform.position;
        //            pos.y += 1;
        //            pos.z += 2;
        //            PlayerMovecolliders.transform.position = pos;
        //        }
        //    }
        //    else if (mainCamera.transform.localEulerAngles.y == 270) {
        //        Debug.Log("270");
        //        if (input == 0) {
        //            Vector3 pos = Player.transform.position;
        //            pos.y += 1;
        //            pos.x -= 2;
        //            PlayerMovecolliders.transform.position = pos;
        //        }
        //        else if (input == 1) {
        //            Vector3 pos = Player.transform.position;
        //            pos.y += 1;
        //            pos.x += 2;
        //            PlayerMovecolliders.transform.position = pos;
        //        }
        //        else if (input == 2) {
        //            Vector3 pos = Player.transform.position;
        //            pos.y += 1;
        //            pos.z += 2;
        //            PlayerMovecolliders.transform.position = pos;
        //        }
        //        else if (input == 3) {
        //            Vector3 pos = Player.transform.position;
        //            pos.y += 1;
        //            pos.z -= 2;
        //            PlayerMovecolliders.transform.position = pos;
        //        }
        //    }
        //    else if (mainCamera.transform.localEulerAngles.y == 180) {
        //        Debug.Log("180");
        //        if (input == 0) {
        //            Vector3 pos = Player.transform.position;
        //            pos.y += 1;
        //            pos.z -= 2;
        //            PlayerMovecolliders.transform.position = pos;
        //        }
        //        else if (input == 1) {
        //            Vector3 pos = Player.transform.position;
        //            pos.y += 1;
        //            pos.z += 2;
        //            PlayerMovecolliders.transform.position = pos;
        //        }
        //        else if (input == 2) {
        //            Vector3 pos = Player.transform.position;
        //            pos.y += 1;
        //            pos.x -= 2;
        //            PlayerMovecolliders.transform.position = pos;
        //        }
        //        else if (input == 3) {
        //            Vector3 pos = Player.transform.position;
        //            pos.y += 1;
        //            pos.x += 2;
        //            PlayerMovecolliders.transform.position = pos;
        //        }
        //    }
        //    else {
        //        Debug.Log(mainCamera.transform.localEulerAngles.y);
        //        if (input == 0) {
        //            Vector3 pos = Player.transform.position;
        //            pos.y += 1;
        //            pos.z += 2;
        //            PlayerMovecolliders.transform.position = pos;
        //        }
        //        else if (input == 1) {
        //            Vector3 pos = Player.transform.position;
        //            pos.y += 1;
        //            pos.z -= 2;
        //            PlayerMovecolliders.transform.position = pos;
        //        }
        //        else if (input == 2) {
        //            Vector3 pos = Player.transform.position;
        //            pos.y += 1;
        //            pos.x += 2;
        //            PlayerMovecolliders.transform.position = pos;
        //        }
        //        else if (input == 3) {
        //            Vector3 pos = Player.transform.position;
        //            pos.y += 1;
        //            pos.x -= 2;
        //            PlayerMovecolliders.transform.position = pos;
        //        }
        //    }



        //}

        //public void SetMove(GameObject moveblock) {
        //    if (Step == null) {
        //        if (!(moveblock == null)) {
        //            PlayerMove(moveblock);
        //        }
        //    }
        //    else {
        //        if (!(Step.GetComponent<StepController>().StayStepMove(1) == null)) {
        //            player.GetComponent<PlayerController>().SetPlayerTarget(Step.GetComponent<StepController>().StayStepMove(1));
        //            Step = null;
        //        }
        //    }
        //}
    }
}
