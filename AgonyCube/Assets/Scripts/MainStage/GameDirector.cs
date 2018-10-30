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
        public GameObject[] PlayerMovecolliders;
        private GameObject Step;
       
        public bool CheckMode = false;
        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            if (CheckMode == false) {
                if (Input.GetMouseButtonDown(0)) {
                    if (Choice1 == null) {
                        Ray mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);

                        RaycastHit hit;
                        if (Physics.Raycast(mouseray, out hit, 10.0f, Cube)) {
                            if (Vector3.Distance(Player.transform.position, hit.transform.gameObject.transform.position) >= 1.5) {
                                Choice1 = hit.transform.gameObject;

                                Choice1.GetComponent<LineRenderer>().enabled = true;
                            }
                        }


                    }
                    else {

                        if (!(Choice1 == null)) {
                            SwapCube();
                        }


                    }
                }
            }

            if (CheckMode == true) {
                if (Player.GetComponent<PlayerController>().checkPleyrMove == false) {
                    var horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
                    var vertical = CrossPlatformInputManager.GetAxis("Vertical");

                    if (Mathf.Abs(horizontal) > Mathf.Abs(vertical)) {

                        if (horizontal > 0.5f) {
                            if (!(PlayerMovecolliders[2].GetComponent<PlayerCollider>().MoveBlock == null)) {
                                PlayerMove(PlayerMovecolliders[2].GetComponent<PlayerCollider>().MoveBlock);
                            }                                                        
                        }
                        else if (horizontal < -0.5f) {
                            if (!(PlayerMovecolliders[3].GetComponent<PlayerCollider>().MoveBlock == null)) {
                                PlayerMove(PlayerMovecolliders[3].GetComponent<PlayerCollider>().MoveBlock);
                            }
                        }
                    }
                    else {

                        if (vertical > 0.5f) {
                            if (Step == null) {
                                if (!(PlayerMovecolliders[0].GetComponent<PlayerCollider>().MoveBlock == null)) {
                                    PlayerMove(PlayerMovecolliders[0].GetComponent<PlayerCollider>().MoveBlock);
                                }
                            }
                            else {
                                if (!(Step.GetComponent<StepController>().StayStepMove(0) == null)) {
                                    Player.GetComponent<PlayerController>().SetPlayerTarget(Step.GetComponent<StepController>().StayStepMove(0));
                                    Step = null;
                                }
                               
                            }
                        }
                        else if (vertical < -0.5f) {
                            if (Step == null) {
                                if (!(PlayerMovecolliders[1].GetComponent<PlayerCollider>().MoveBlock == null)) {
                                    PlayerMove(PlayerMovecolliders[1].GetComponent<PlayerCollider>().MoveBlock);
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
                            Vector3 pos1 = Choice1.transform.position;
                            Vector3 pos2 = Choice2.transform.position;
                        if (pos1.y == pos2.y) {
                            float dis = Vector3.Distance(pos1, pos2);

                            Debug.Log(dis);
                            //選択された2つのキューブが隣り合っていた場合positionの交換をし選択状態の解除、変数の初期化
                            if (dis <= 2.5) {
                                Choice1.transform.position = pos2;
                                Choice2.transform.position = pos1;

                                Choice1.GetComponent<LineRenderer>().enabled = false;

                                Choice1 = null;
                                Choice2 = null;
                            }
                            else {
                                //選択されたキューブが遠い場合choice2のみ初期化
                                Choice2 = null;
                            }
                        }
                        else {
                            Choice2 = null;
                        }
                    }
                    


                }
            }
        }
        //playerの移動用の関数
        private void PlayerMove(GameObject target) {
            Debug.Log("a" + target);
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
            if(!(Choice1 == null)) {
                Choice1.GetComponent<LineRenderer>().enabled = false;
                Choice1 = null;
            }
            
        }
    }
}
