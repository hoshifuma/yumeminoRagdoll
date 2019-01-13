using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace AgonyCube.MainStage {
    public class PlayerController : MonoBehaviour {
        //playerについているCharacterControllerを保存
        CharacterController Controller;
        //playerの移動速度
        public float Speed;
        //重力
        public float Gravity;
        Vector3 moveDirection;
        public GameObject scissors;
        private Animator animator;
        public Animator scissorsAnimator;

        public Animator doorAnimator;

        public Vector3 target;
        private const string key_walk = "Walking";

        // アニメーションID
        static readonly int openId = Animator.StringToHash("OpenTrigger");

        static readonly int cutId = Animator.StringToHash("Cut");

        static readonly int scissorsId = Animator.StringToHash("CutTrigger");

        public GameObject mainCamera;
        // StageControllerを指定
        public StageController stage;
        //GameDirectorを指定
        public GameDirector director;
        // プレイヤーの現在のグリッド座標を指定
        public Vector3Int gridPoint;
        // 移動速度を指定
        public float locomotionSpeed = 1.0f;
        // 移動を開始した際のワールド座標
        Vector3 startPosition;

        // プレイヤーの状態を表します。
        public enum PlayerState {
            None,
            // 「ステージ開始演出」
            StartDemo,
            // 待機
            Idle,
            // 移動中
            Locmotion,
            // 「ゲームオーバー演出」
            Cut,
            // 「ステージクリアー演出」
            StageClear,
        }

        // 現在の状態
        public PlayerState playerState = PlayerState.None;
        // Use this for initialization
        void Start() {
            Controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            gridPoint = Data.instance.playerGrid;
            // グリッド座標からワールド座標に変換して設定
            var position = stage.GridToWorldPoint(gridPoint);
            transform.position = new Vector3(position.x, position.y - 1, position.z);
            // 状態をリセット
            playerState = PlayerState.Idle;
        }

        // Update is called once per frame
        void Update() {
            switch (playerState) {
                case PlayerState.None:
                    break;
                case PlayerState.StartDemo:
                    break;
                case PlayerState.Idle:
                    // 移動方向を決定
                    //int direction = -1;


                    var horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
                    var vertical = CrossPlatformInputManager.GetAxis("Vertical");

                    if (Mathf.Abs(horizontal) > Mathf.Abs(vertical)) {
                        if (horizontal > 0.5f) {
                            CameraRootPlayerMoveChanger(2);
                        }
                        else if (horizontal < -0.5f) {
                            CameraRootPlayerMoveChanger(3);
                        }
                    }
                    else {
                        if (vertical > 0.5f) {
                            CameraRootPlayerMoveChanger(0);
                        }
                        else if (vertical < -0.5f) {
                            CameraRootPlayerMoveChanger(1);
                        }
                    }
                    break;
                case PlayerState.Locmotion:
                    if (Controller.isGrounded) {
                        moveDirection = new Vector3(0, 0, 1);
                        moveDirection = transform.TransformDirection(moveDirection);
                        moveDirection *= Speed;
                    }
                    moveDirection.y -= Gravity;
                    Controller.Move(moveDirection * Time.deltaTime);

                    float distance = Mathf.Abs(target.x - transform.position.x);
                    distance += Mathf.Abs(target.z - transform.position.z);

                    if (distance <= 0.05) {
                        Vector3 pos = target;
                        pos.y = transform.position.y;
                        transform.position = pos;
                        director.ChangeIdleState();

                    }

                    break;
                case PlayerState.Cut:
                    break;
                case PlayerState.StageClear:
                    GoalPerformance();
                    break;
                default:
                    break;
            }

            if (playerState == PlayerState.Locmotion) {
                this.animator.SetBool(key_walk, true);
            }
            else if (playerState == PlayerState.Idle) {
                this.animator.SetBool(key_walk, false);
            }

        }

        public void SetPlayerTarget(Vector3Int gridPoint) {
            target = stage.GridToWorldPoint(gridPoint);

            Vector3 nexttarget;
            nexttarget = target;
            nexttarget.y = transform.position.y;
            transform.LookAt(nexttarget);
            director.ChangeMoveState();
        }
        //カメラの向きと入力された向きに応じて移動場所を指定
        public void CameraRootPlayerMoveChanger(int input) {
            int direction = -1;
            int dx = 0;
            int dz = 0;
            if (mainCamera.transform.localEulerAngles.y == 90) {
                Debug.Log(mainCamera.transform.localEulerAngles.y);
                if (input == 0) {
                    dx = 1;
                    direction = 1;
                }
                else if (input == 1) {
                    dx = -1;
                    direction = 3;
                }
                else if (input == 2) {
                    dz = -1;
                    direction = 2;
                }
                else if (input == 3) {
                    dz = 1;
                    direction = 0;
                }
            }
            else if (mainCamera.transform.localEulerAngles.y == 270) {
                Debug.Log("270");
                if (input == 0) {
                    dx = -1;
                    direction = 3;
                }
                else if (input == 1) {
                    dx = 1;
                    direction = 1;
                }
                else if (input == 2) {
                    dz = 1;
                    direction = 0;
                }
                else if (input == 3) {
                    dz = -1;
                    direction = 2;
                }
            }
            else if (mainCamera.transform.localEulerAngles.y == 180) {
                Debug.Log("180");
                if (input == 0) {
                    dz = -1;
                    direction = 2;
                }
                else if (input == 1) {
                    dz = 1;
                    direction = 0;
                }
                else if (input == 2) {
                    dx = -1;
                    direction = 3;
                }
                else if (input == 3) {
                    dx = 1;
                    direction = 1;
                }
            }
            else {
                if (input == 0) {
                    dz = 1;
                    direction = 0;
                }
                else if (input == 1) {
                    dz = -1;
                    direction = 2;
                }
                else if (input == 2) {
                    dx = 1;
                    direction = 1;
                }
                else if (input == 3) {
                    dx = -1;
                    direction = 3;
                }
            }

            if (direction > -1) {
                CheckMovableBlock(direction, dx, dz);
            }
        }

        private void CheckMovableBlock(int direction, int dx, int dz) {



            // 現在のグリッド位置のブロックを取得
            var currentBlock = stage.GetGrid(gridPoint);
            if (currentBlock.BlockId != 3) {
                //現在いる場所が階段以外の場合
                // ひとつ先のグリッドがあるか判断
                if (currentBlock.adjacentBlock[direction] != null) {
                    //移動先のBlockがあった場合
                    if (currentBlock.adjacentBlock[direction].movableFlag) {
                        //移動先に動ける場合
                        if (currentBlock.adjacentBlock[direction].BlockId != 3) {
                            //移動先が階段ではない場合
                            CheckCut(dx, 0,dz);
                        }
                        else {
                            //移動先が階段の場合
                            var block = GetStepFrontAndBack(stage.GetGrid(gridPoint.x + dx, gridPoint.y, gridPoint.z + dz));
                            if (block[1] != null && currentBlock.blockNumber == block[1].blockNumber && block[1].movableFlag) {
                                CheckCut(dx, 0, dz);
                            }
                        }

                    }
                    else {
                        //移動先のBlockが移動不可の場合
                        if (currentBlock.adjacentBlock[direction].BlockId != 3) {
                            // 移動先のBlockが階段ではない場合
                            var nextGrid = stage.WorldPointToGrid(currentBlock.adjacentBlock[direction].transform.position);
                            nextGrid.y -= 1;
                            var nextBlock = stage.GetGrid(nextGrid);
                            if (nextBlock != null && nextBlock.BlockId == 3) {
                                //移動先のしたのBlockが階段の場合

                                var block = GetStepFrontAndBack(nextBlock);
                                Debug.Log(currentBlock.blockNumber);
                               

                                if (currentBlock.blockNumber == block[0].blockNumber) {
                                    CheckCut(dx, -1, dz);
                                }
                            }
                        }
                        else {
                            //移動先のBlockが階段の場合
                            var block = GetStepFrontAndBack(currentBlock.adjacentBlock[direction]);
                            if (currentBlock.blockNumber == block[1].blockNumber) {
                                var nextGrid = stage.WorldPointToGrid(currentBlock.adjacentBlock[direction].transform.position);
                                nextGrid.y -= 1;
                                var nextBlock = stage.GetGrid(nextGrid);
                                if (nextBlock != null && nextBlock.BlockId == 3) {
                               
                                    //移動先のしたのBlockが階段の場合
                                    var block1 = GetStepFrontAndBack(nextBlock);
                                    if (currentBlock.blockNumber == block1[0].blockNumber) {
                                        CheckCut(dx, -1, dz);
                                    }
                                }
                            }
                        }
                    }

                }

            }

            else {
                //現在のBlockが階段の場合
                if (currentBlock.adjacentBlock[direction] != null) {
                    var block = GetStepFrontAndBack(currentBlock);
                    if (block[0] != null && block[0].blockNumber == stage.GetGrid(gridPoint.x + dx, gridPoint.y + 1, gridPoint.z + dz).blockNumber) {
                        //階段の後ろのBlockに移動する場合
                        if (block[0].movableFlag) {
                            //移動先に移動できる場合
                            if (block[0].BlockId != 3) {
                                //移動先が階段ではない場合
                                CheckCut(dx, 1, dz);
                            }
                            else {
                                //移動先が階段の場合
                                var block1 = GetStepFrontAndBack(stage.GetGrid(gridPoint.x + dx, gridPoint.y + 1, gridPoint.z + dz));
                                Debug.Log(block1[1]);
                                if (block1[1] != null) {
                                    var posi = block1[1].transform.position;
                                    var grid = stage.WorldPointToGrid(posi);
                                    grid.y -= 1;
                                    Debug.Log(stage.GetGrid(grid).blockNumber);
                                    if (currentBlock.blockNumber == stage.GetGrid(grid).blockNumber) {
                                        CheckCut(dx, 1, dz);
                                    }
                                }

                            }

                        }
                    }
                    else if (block[1] != null && block[1].blockNumber == stage.GetGrid(gridPoint.x + dx, gridPoint.y, gridPoint.z + dz).blockNumber) {
                        if (currentBlock.adjacentBlock[direction].movableFlag) {
                            //移動先に動ける場合
                            if (currentBlock.adjacentBlock[direction].BlockId != 3) {
                                //移動先が階段ではない場合
                                CheckCut(dx, 0, dz);
                            }
                            else {
                                //移動先が階段の場合
                                var block1 = GetStepFrontAndBack(stage.GetGrid(gridPoint.x + dx, gridPoint.y, gridPoint.z + dz));
                                if (block[1] != null && currentBlock.blockNumber == block1[1].blockNumber && block1[1].movableFlag) {
                                    //移動先の階段の前側にいる場合
                                    CheckCut(dx, 0, dz);
                                }
                            }

                        }
                        else {
                            //移動先のBlockが移動不可の場合
                            if (currentBlock.adjacentBlock[direction].BlockId != 3) {
                                // 移動先のBlockが階段ではない場合
                                var nextGrid = stage.WorldPointToGrid(currentBlock.adjacentBlock[direction].transform.position);
                                nextGrid.y -= 1;
                                var nextBlock = stage.GetGrid(nextGrid);
                                if (nextBlock != null && nextBlock.BlockId == 3) {
                                    Debug.Log(nextBlock);
                                    //移動先のしたのBlockが階段の場合
                                    var block1 = GetStepFrontAndBack(nextBlock);
                                    if (currentBlock.blockNumber == block1[0].blockNumber) {
                                        CheckCut(dx, -1, dz);
                                    }
                                }
                            }
                            else {
                                //移動先のBlockが階段の場合
                                var block1 = GetStepFrontAndBack(currentBlock.adjacentBlock[direction]);
                                if (currentBlock.blockNumber == block1[1].blockNumber) {
                                    var nextGrid = stage.WorldPointToGrid(currentBlock.adjacentBlock[direction].transform.position);
                                    nextGrid.y -= 1;
                                    var nextBlock = stage.GetGrid(nextGrid);
                                    if (nextBlock != null && nextBlock.BlockId == 3) {
                                        Debug.Log(nextBlock);
                                        //移動先のしたのBlockが階段の場合
                                        var block2 = GetStepFrontAndBack(nextBlock);
                                        if (currentBlock.blockNumber == block2[0].blockNumber) {
                                            CheckCut(dx, -1, dz);
                                        }
                                    }
                                }
                            }
                        }

                    }

                }
            }
        }

        private void CheckCut(int dx,int dy ,int dz) {
            if (stage.doorGrid != new Vector3Int(gridPoint.x + dx, gridPoint.y + dy, gridPoint.z + dz)) {
                //移動を確定する
                gridPoint.x += dx;
                gridPoint.z += dz;
                gridPoint.y += dy;
                SetPlayerTarget(gridPoint);

            }
            else if (director.scissors) {
                gridPoint.x += dx;
                gridPoint.z += dz;
                gridPoint.y += dy;
                target = stage.GridToWorldPoint(gridPoint);

                Vector3 nexttarget;
                nexttarget = target;
                nexttarget.y = transform.position.y;
                transform.LookAt(nexttarget);
             
                StartCoroutine(OnDoorEnter());
            }
        }

        private IEnumerator OnDoorEnter() {
            var mesh = scissors.GetComponentsInChildren<MeshRenderer>();
           
            foreach (var child in mesh) {
                child.enabled = true;
            }
            playerState = PlayerState.Cut;
            scissorsAnimator.SetTrigger(scissorsId);
            animator.SetTrigger(cutId);

            doorAnimator.SetTrigger(openId);
            yield return new WaitForSeconds(1);

            while (true) {
                var info = doorAnimator.GetCurrentAnimatorStateInfo(0);
                var info1 = animator.GetCurrentAnimatorStateInfo(0);
                var info2 = scissorsAnimator.GetCurrentAnimatorStateInfo(0);
                if (info.normalizedTime > 1.0f && info1.normalizedTime > 1.0f && info2.normalizedTime > 1.0f) {
                    
                    break;
                }
                yield return null;
            }
            stage.doorGrid = new Vector3Int(10, 10, 10);
            
            SetPlayerTarget(gridPoint);
            foreach (var child in mesh) {
                child.enabled = false;
            }
        }

        //指定した階段の前と上った先を返す　0が上った先1が前
        public Block[] GetStepFrontAndBack(Block stepBlock) {
            Block[] block = new Block[2];
            //階段のあるgridを保存
            var grid = stage.WorldPointToGrid(stepBlock.transform.position);
            //階段の角度を保存
            var rad = stepBlock.transform.localEulerAngles;
            rad = new Vector3(Mathf.Round(rad.x), Mathf.Round(rad.y), rad.z);
            Debug.Log(rad);
            if (rad.x == 0) {
                //階段が上向きの時
                if (rad.y == 0) {
                   

                    block[0] = stage.GetGrid(grid.x, grid.y + 1, grid.z + 1);
                    block[1] = stage.GetGrid(grid.x, grid.y, grid.z - 1);
                   
                }
                else if (rad.y   == 90) {
                   

                    block[0] = stage.GetGrid(grid.x + 1, grid.y + 1, grid.z);
                    block[1] = stage.GetGrid(grid.x - 1, grid.y, grid.z);

                }
                else if (rad.y == 180) {
                   

                    block[0] = stage.GetGrid(grid.x, grid.y + 1, grid.z - 1);
                    block[1] = stage.GetGrid(grid.x, grid.y, grid.z + 1);
                }
                else {
                    block[0] = stage.GetGrid(grid.x - 1, grid.y + 1, grid.z);
                    block[1] = stage.GetGrid(grid.x + 1, grid.y, grid.z);
                }
            }
            else {
                //階段がひっくり返っているとき
                if (rad.y == 0) {
                    block[0] = stage.GetGrid(grid.x, grid.y + 1, grid.z - 1);
                    block[1] = stage.GetGrid(grid.x, grid.y, grid.z + 1);
                }
                else if (rad.y == 90) {
                    block[0] = stage.GetGrid(grid.x - 1, grid.y + 1, grid.z);
                    block[1] = stage.GetGrid(grid.x + 1, grid.y, grid.z);
                }
                else if (rad.y == 180) {
                    block[0] = stage.GetGrid(grid.x, grid.y + 1, grid.z + 1);
                    block[1] = stage.GetGrid(grid.x, grid.y, grid.z - 1);
                }
                else {
                    block[0] = stage.GetGrid(grid.x + 1, grid.y + 1, grid.z);
                    block[1] = stage.GetGrid(grid.x - 1, grid.y, grid.z);
                }
            }
          
            return block;
        }
        
        
        //クリア演出内容
        private void GoalPerformance() {
            mainCamera.GetComponent<CameraController>().cameraMove = false;
            animator.SetTrigger("Goal");
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        //[Nextscreen.cs]で呼び出し
        public void GetHeart() {
            //ハートを取ったらStateChange
            playerState = PlayerState.StageClear;
        }
    }
}