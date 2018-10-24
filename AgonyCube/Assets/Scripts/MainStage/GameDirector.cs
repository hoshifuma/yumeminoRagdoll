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
        private GameObject Step;
        private GameObject HitBlock;

        public bool CheckMode = false;
        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

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

                    if(CheckMode == true) {
                        PlayerMove();
                    }
                }
                else {
                    if (CheckMode == false) {
                        SwapCube();
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
                if (Vector3.Distance(Player.transform.position, hit.transform.gameObject.transform.position) >= 1.5) {
                    //choice1の変数に何もなかった場合rayに当たったものを格納し選択状態に変更
                  
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
                    


                }
            }
        }
        //playerの移動用の関数
        private void PlayerMove() {


            if (!(Choice1 == null)) {
                bool movecheck = false;

                if (Choice1.gameObject.tag == "Block") {
                    movecheck = Choice1.GetComponent<CubeController>().MoveCheck();
                }
                else if (Choice1.gameObject.tag == "Clear") {
                    movecheck = Choice1.GetComponent<ClearController>().ClearMoveCheck();
                }
                else if (Choice1.gameObject.tag == "Step") {
                    movecheck = Choice1.GetComponent<StepController>().StepMoveCheck();
                }

                if (Step == null) {
                    if (movecheck == true) {

                        Vector3 pos1 = Choice1.transform.position;
                        Vector3 pos2 = Player.transform.position;



                        float ybalance = Mathf.Abs(pos2.y - pos1.y);
                        if (ybalance <= 1) {
                            float dis = Vector3.Distance(pos1, pos2);



                            if (dis <= 2.5) {
                                Vector3 Newposi;
                                Newposi.x = pos1.x;
                                Newposi.z = pos1.z;
                                Newposi.y = pos2.y;

                                Player.transform.position = Newposi;


                                Choice1.GetComponent<LineRenderer>().enabled = false;
                                if (Choice1.gameObject.tag == "Step") {
                                    Step = Choice1;
                                }
                                Choice1 = null;
                            }

                        }
                    }
                }
                else {
                    GameObject[] Block;


                    Block = Step.GetComponent<StepController>().HitCheckCollider;
                    for (int i = 0; i < 2; i++) {
                        if (Block[i].GetComponent<HitCheckCollider>().HitBlock == Choice1) {
                            HitBlock = Block[0].GetComponent<HitCheckCollider>().HitBlock;
                        }
                    }

                    if (!(HitBlock == null)) {
                        if (movecheck == true) {
                            Vector3 Newposi = HitBlock.gameObject.transform.position;
                            Player.transform.position = Newposi;

                            if (Choice1.gameObject.tag == "Step") {
                                Step = Choice1;
                            }
                            else {
                                Step = null;
                            }

                            Choice1.GetComponent<LineRenderer>().enabled = false;
                            Choice1 = null;
                        }
                    }
                }



            }
        }

        public void ChangeMode() {
            CheckMode = !CheckMode;
        }
    }
}
