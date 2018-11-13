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
        float time;
        public enum GameMode {
            //初期演出用モード
            None,
            //モード選択モード
            Idle,
            //Blockの交換モード
            Swap,
            //BLockの回転モード
            Spin,
            //Playerの移動モード
            PlayerMove,
        }

        public GameMode gameMode = GameMode.None;
        // Use this for initialization
        void Start() {
            gameMode = GameMode.Idle;
        }

        // Update is called once per frame
        void Update() {
            switch (gameMode) {
                case GameMode.None:
                    break;
                case GameMode.Idle: {
                        if (Input.GetMouseButton(0)) {
                            if (CheckBlockClick()) {
                                time += Time.deltaTime;
                                if (time > 1) {


                                    time = 0;
                                    gameMode = GameMode.Spin;
                                }
                            }
                            else {
                                Choice1 = null;
                            }
                        }
                        if (Input.GetMouseButtonUp(0)) {
                            time = 0;
                            if(Choice1 != null) {
                                Choice1 = null;
                                gameMode = GameMode.Swap;
                            }

                        }
                        
                    }
                    break;
                case GameMode.Swap: {
                        if (Input.GetMouseButtonDown(0)) {
                            if (Choice1 == null) {
                                if (CheckBlockClick()) {
                                    Choice1.GetComponent<LineRenderer>().enabled = true;
                                }
                            }
                            else {
                                if (!(Choice1 == null)) {
                                    SwapCube();
                                }
                            }
                        }
                    }
                    break;
                case GameMode.Spin:
                    break;
                case GameMode.PlayerMove:
                    break;
                default:
                    break;
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
        //クリックされたものがBlockか判定
        private bool CheckBlockClick() {
            Ray mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(mouseray, out hit, 10.0f, Cube)) {
                if (Vector3.Distance(Player.transform.position, hit.transform.gameObject.transform.position) >= 1.5) {
                    //Blockの場合choice1に格納
                    Choice1 = hit.transform.gameObject;
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }

        //キューブのSwap機能用の関数
        private void SwapCube() {
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

                                    Choice1 = null;
                                    Choice2 = null;

                                    stage.UpdateGridData();
                                    stage.CalcurateAdgency();

                                    index = 10;
                                }
                            }
                        }
                        Choice2 = null;
                    }



                }
            }
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

            //if (Step == null) {
            if (movecheck == true) {
                //Vector3 pos1 = target.transform.position;
                //Vector3 pos2 = Player.transform.position;



                //float ybalance = Mathf.Abs(pos2.y - pos1.y);
                //if (ybalance <= 1) {
                // float dis = Vector3.Distance(pos1, pos2);
                //float dis = Mathf.Abs(pos1.x - pos2.x);
                //dis += Mathf.Abs(pos1.z - pos2.z);


                // if (dis <= 2.5) {
                /*
                Vector3 Newposi;
                Newposi.x = pos1.x;
                Newposi.z = pos1.z;
                Newposi.y = pos2.y;

                Player.transform.position = Newposi;
                */
                Player.GetComponent<PlayerController>().SetPlayerTarget(target.gameObject);
                if (target.gameObject.tag == "Step") {
                    Step = target;
                }
                //  }
                //}
            }
            /*}      
            else {
                GameObject[] Block;
                Block = Step.GetComponent<StepController>().HitCheckCollider;
                for (int i = 0; i < 2; i++) {
                    if (Block[i].GetComponent<HitCheckCollider>().HitBlock == target) {
                        HitBlock = Block[i].GetComponent<HitCheckCollider>().HitBlock;
                    }
                }
                if (!(HitBlock == null)) {
                    if (movecheck == true) {
                        /*Vector3 Newposi = HitBlock.gameObject.transform.position;                        
                        Player.transform.position = Newposi;
                        Player.GetComponent<PlayerController>().SetPlayerTarget(HitBlock.gameObject);
                        if (target.gameObject.tag == "Step") {
                            Step = target;
                        }
                        else {
                            Step = null;
                        }
                    }
                }
            }*/
            //target.GetComponent<LineRenderer>().enabled = false;
            // target = null;
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
