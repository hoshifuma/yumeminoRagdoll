using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace AgonyCubeMainStage {
    public class GameDirector : MonoBehaviour {

        public LayerMask Cube;
        public GameObject Choice1;
        public GameObject Choice2;
        public GameObject Player;
        public GameObject MainCamera;
        public GameObject PlayerMovecolliders;
        private GameObject Step;
        public StageController stage;
        public bool CheckMode = false;
        float tapTime;

        GameState currentState = null;


        private class MainScene : GameState {
            protected GameDirector gameDirector;

            public MainScene(GameDirector gameDirector) {
                this.gameDirector = gameDirector;
            }
        }

        private class IdleState : MainScene {

            public IdleState(GameDirector gameDirector) : base(gameDirector) {

            }
            public override void Start() {

            }

            public override void Update() {
                if (Input.GetMouseButtonDown(0)) {
                    gameDirector.Choice1 = gameDirector.CheckBlockClick();
                }

                if (Input.GetMouseButton(0)) {
                    if (gameDirector.Choice1 != null) {
                        gameDirector.tapTime += Time.deltaTime;
                        if (gameDirector.tapTime > 1) {


                            gameDirector.tapTime = 0;
                            gameDirector.ChangeState(new SpinState(gameDirector));
                        }
                    }
                }

                if (Input.GetMouseButtonUp(0)) {
                    gameDirector.tapTime = 0;
                    if (gameDirector.Choice1 != null) {
                        gameDirector.ChangeState(new SwapState(gameDirector));
                    }
                }
            }
        }

        private class SwapState : MainScene {
            public SwapState(GameDirector gameDirector) : base(gameDirector) {
                
            }
            public override void Start() {
                gameDirector.stage.InvisibleBlock(gameDirector.Choice1);
                gameDirector.Choice1 = null;
            }

            public override void Update() {
                if (Input.GetMouseButtonDown(0)) {
                    if (gameDirector.Choice1 == null) {
                        gameDirector.Choice1 = gameDirector.CheckBlockClick();
                        if (gameDirector.Choice1 != null) {
                            gameDirector.Choice1.GetComponent<LineRenderer>().enabled = true;
                        }
                    }
                    else {
                        if (!(gameDirector.Choice1 == null)) {
                            if (gameDirector.SwapCube()) {
                                gameDirector.ChangeState(new IdleState(gameDirector));
                            }
                        }
                    }
                }
            }

            public override void Exsit() {
                gameDirector.stage.UnInvisibleBlock(gameDirector.Choice1);
                gameDirector.Choice1 = null;
                
            }
        }

        private class SpinState : MainScene {

            public SpinState(GameDirector gameDirector) : base(gameDirector) {

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
            //if (CheckMode == false) {
            //    if (Input.GetMouseButtonDown(0)) {
            //        if (Choice1 == null) {
            //            Ray mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //            RaycastHit hit;
            //            if (Physics.Raycast(mouseray, out hit, 10.0f, Cube)) {
            //                if (Vector3.Distance(Player.transform.position, hit.transform.gameObject.transform.position) >= 1.5) {
            //                    Choice1 = hit.transform.gameObject;

            //                    Choice1.GetComponent<LineRenderer>().enabled = true;
            //                }
            //            }
            //        }
            //        else {
            //            if (!(Choice1 == null)) {
            //                SwapCube();
            //            }
            //        }
            //    }
            //}
            //if (CheckMode == true) {
            //    if (Player.GetComponent<PlayerController>().checkPleyrMove == false) {
            //        var horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            //        var vertical = CrossPlatformInputManager.GetAxis("Vertical");

            //        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical)) {
            //            if (horizontal > 0.5f) {
            //                CameraRootPlayerMoveChanger(2);
            //            }
            //            else if (horizontal < -0.5f) {
            //                CameraRootPlayerMoveChanger(3);
            //            }
            //        }
            //        else {
            //            if (vertical > 0.5f) {
            //                CameraRootPlayerMoveChanger(0);
            //            }
            //            else if (vertical < -0.5f) {
            //                CameraRootPlayerMoveChanger(1);
            //            }
            //        }
            //    }
            //}

        }
        //クリックされたものがBlockの場合クリックされたBlockを返す
        private GameObject CheckBlockClick() {
            Ray mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(mouseray, out hit, 10.0f, Cube)) {
                if (Vector3.Distance(Player.transform.position, hit.transform.gameObject.transform.position) >= 1.5) {
                    //PlayerがいないBlockのBlockを返す
                    return hit.transform.gameObject;
                }
            }
            return null;
        }

        //キューブのSwap機能用の関数 Swapできた場合trueを返す
        private bool SwapCube() {
            Ray mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;


            //rayがキューブに当たった際に起動
            if (Physics.Raycast(mouseray, out hit, 10.0f, Cube)) {
                //rayが当たったキューブがplayerのいるキューブじゃないことを確認
                if (Vector3.Distance(Player.transform.position, hit.transform.position) >= 1.5) {
                    Choice2 = hit.transform.gameObject;
                    //選択されたものが同じものだった場合選択状態を解除し変数を初期状態に変更
                    if (Choice1 == Choice2) {
                        Choice1.GetComponent<LineRenderer>().enabled = false;
                        Choice1 = null;
                        Choice2 = null;
                    }
                    else {
                        //選択された2つのキューブが隣り合っていた場合positionの交換をし選択状態の解除、変数の初期化
                        for (int index = 0; index < 4; index++) {
                            if (Choice1.GetComponent<Block>().adjacentBlock[index] != null) {

                                if (Choice1.GetComponent<Block>().adjacentBlock[index].blockNumber == Choice2.GetComponent<Block>().blockNumber) {
                                    Vector3 pos1 = Choice1.transform.position;
                                    Vector3 pos2 = Choice2.transform.position;

                                    Choice1.transform.position = pos2;
                                    Choice2.transform.position = pos1;

                                    Choice1.GetComponent<LineRenderer>().enabled = false;

                                    
                                    Choice2 = null;

                                    stage.UpdateGridData();
                                    return true;
                                }
                            }
                        }
                        Choice2 = null;
                    }
                }
            }
            return false;
        }
        //playerの移動用の関数
        private void PlayerMove(GameObject target) {

            bool movecheck = false;
            if (target.gameObject.tag == "Block") {
                movecheck = target.GetComponent<CubeController>().MoveCheck();
            }
            else if (target.gameObject.tag == "Clear") {
                movecheck = target.GetComponent<ClearController>().ClearMoveCheck();
            }
            else if (target.gameObject.tag == "Step") {
                movecheck = target.GetComponent<StepController>().StepMoveCheck();
            }

            if (movecheck == true) {

                Player.GetComponent<PlayerController>().SetPlayerTarget(target.gameObject);
                if (target.gameObject.tag == "Step") {
                    Step = target;
                }

            }

        }

        public void ChangeMode() {
            CheckMode = !CheckMode;
            if (!(Choice1 == null)) {
                Choice1.GetComponent<LineRenderer>().enabled = false;
                Choice1 = null;
            }

        }

        public void CameraRootPlayerMoveChanger(int input) {
            if (MainCamera.transform.localEulerAngles.y == 90) {
                Debug.Log(MainCamera.transform.localEulerAngles.y);
                if (input == 0) {
                    Vector3 pos = Player.transform.position;
                    pos.y += 1;
                    pos.x += 2;
                    PlayerMovecolliders.transform.position = pos;
                }
                else if (input == 1) {
                    Vector3 pos = Player.transform.position;
                    pos.y += 1;
                    pos.x -= 2;
                    PlayerMovecolliders.transform.position = pos;
                }
                else if (input == 2) {
                    Vector3 pos = Player.transform.position;
                    pos.y += 1;
                    pos.z -= 2;
                    PlayerMovecolliders.transform.position = pos;
                }
                else if (input == 3) {
                    Vector3 pos = Player.transform.position;
                    pos.y += 1;
                    pos.z += 2;
                    PlayerMovecolliders.transform.position = pos;
                }
            }
            else if (MainCamera.transform.localEulerAngles.y == 270) {
                Debug.Log("270");
                if (input == 0) {
                    Vector3 pos = Player.transform.position;
                    pos.y += 1;
                    pos.x -= 2;
                    PlayerMovecolliders.transform.position = pos;
                }
                else if (input == 1) {
                    Vector3 pos = Player.transform.position;
                    pos.y += 1;
                    pos.x += 2;
                    PlayerMovecolliders.transform.position = pos;
                }
                else if (input == 2) {
                    Vector3 pos = Player.transform.position;
                    pos.y += 1;
                    pos.z += 2;
                    PlayerMovecolliders.transform.position = pos;
                }
                else if (input == 3) {
                    Vector3 pos = Player.transform.position;
                    pos.y += 1;
                    pos.z -= 2;
                    PlayerMovecolliders.transform.position = pos;
                }
            }
            else if (MainCamera.transform.localEulerAngles.y == 180) {
                Debug.Log("180");
                if (input == 0) {
                    Vector3 pos = Player.transform.position;
                    pos.y += 1;
                    pos.z -= 2;
                    PlayerMovecolliders.transform.position = pos;
                }
                else if (input == 1) {
                    Vector3 pos = Player.transform.position;
                    pos.y += 1;
                    pos.z += 2;
                    PlayerMovecolliders.transform.position = pos;
                }
                else if (input == 2) {
                    Vector3 pos = Player.transform.position;
                    pos.y += 1;
                    pos.x -= 2;
                    PlayerMovecolliders.transform.position = pos;
                }
                else if (input == 3) {
                    Vector3 pos = Player.transform.position;
                    pos.y += 1;
                    pos.x += 2;
                    PlayerMovecolliders.transform.position = pos;
                }
            }
            else {
                Debug.Log(MainCamera.transform.localEulerAngles.y);
                if (input == 0) {
                    Vector3 pos = Player.transform.position;
                    pos.y += 1;
                    pos.z += 2;
                    PlayerMovecolliders.transform.position = pos;
                }
                else if (input == 1) {
                    Vector3 pos = Player.transform.position;
                    pos.y += 1;
                    pos.z -= 2;
                    PlayerMovecolliders.transform.position = pos;
                }
                else if (input == 2) {
                    Vector3 pos = Player.transform.position;
                    pos.y += 1;
                    pos.x += 2;
                    PlayerMovecolliders.transform.position = pos;
                }
                else if (input == 3) {
                    Vector3 pos = Player.transform.position;
                    pos.y += 1;
                    pos.x -= 2;
                    PlayerMovecolliders.transform.position = pos;
                }
            }



        }

        public void SetMove(GameObject moveblock) {
            if (Step == null) {
                if (!(moveblock == null)) {
                    PlayerMove(moveblock);
                }
            }
            else {
                if (!(Step.GetComponent<StepController>().StayStepMove(1) == null)) {
                    Player.GetComponent<PlayerController>().SetPlayerTarget(Step.GetComponent<StepController>().StayStepMove(1));
                    Step = null;
                }
            }
        }
    }
}
