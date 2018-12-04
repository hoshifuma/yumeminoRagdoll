using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace AgonyCubeMainStage {
    public class PlayerController : MonoBehaviour {

        CharacterController Controller;
        public float Speed;
        public float Gravity;
        Vector3 MoveDirection;
        private Animator animator;
        public Vector3 target;
        private const string key_walk = "Walking";

        public GameObject mainCamera;
        // StageControllerを指定します。
        public StageController stage;
        // プレイヤーの現在のグリッド座標を指定します。
        public Vector3Int gridPoint;
        // 移動速度を指定します。
        public float locomotionSpeed = 1.0f;
        // 移動を開始した際のワールド座標
        Vector3 startPosition;
        // 移動開始してからの経過時間
        float locomotionTime = 0;

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
            GameOver,
            // 「ステージクリアー演出」
            StageClear,
        }

        // 現在の状態
        public PlayerState playerState = PlayerState.None;
        // Use this for initialization
        void Start() {
            Controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
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
                        MoveDirection = new Vector3(0, 0, 1);
                        MoveDirection = transform.TransformDirection(MoveDirection);
                        MoveDirection *= Speed;
                    }
                    MoveDirection.y -= Gravity * Time.deltaTime;
                    Controller.Move(MoveDirection * Time.deltaTime);

                    float distance = Mathf.Abs(target.x - transform.position.x);
                    distance += Mathf.Abs(target.z - transform.position.z);

                    if (distance <= 0.01) {
                        Vector3 pos = target;
                        pos.y = transform.position.y;
                        transform.position = pos;
                        playerState = PlayerState.Idle;
                    }

                    break;
                case PlayerState.GameOver:
                    break;
                case PlayerState.StageClear:
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
            playerState = PlayerState.Locmotion;
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
                Debug.Log(mainCamera.transform.localEulerAngles.y);
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


                // 現在のグリッド位置のブロックを取得
                var currentBlock = stage.GetGrid(gridPoint);
                if (currentBlock.BlockId != 3) {
                    // ひとつ先のグリッドが通行可能かを判定
                    if (currentBlock.adjacentBlock[direction] != null) {
                        if (currentBlock.adjacentBlock[direction].BlockId != 3) {
                            if (currentBlock.adjacentBlock[direction].GetComponent<Block>().movableFlag) {
                                gridPoint.x += dx;
                                gridPoint.z += dz;
                                SetPlayerTarget(gridPoint);
                            }else {

                            }
                            
                        }
                        else {
                            var block = GetStepFrontAndBack(stage.GetGrid(gridPoint.x + dx, gridPoint.y, gridPoint.z + dz));
                            if (block[1].movableFlag) {
                                if (block[1] != null && currentBlock.blockNumber == block[1].blockNumber) {
                                    // 移動を確定する
                                    gridPoint.x += dx;
                                    gridPoint.z += dz;
                                    SetPlayerTarget(gridPoint);
                                }
                            }
                        }
                        
                    }
                }
                else {
                    if (currentBlock.adjacentBlock[direction] != null) {
                        var block = GetStepFrontAndBack(currentBlock);
                        if(block[0] != null && block[0].blockNumber == stage.GetGrid(gridPoint.x + dx, gridPoint.y + 1, gridPoint.z + dz).blockNumber) {
                            if (block[0].movableFlag) {
                                if(block[0].BlockId != 3) {
                                    // 移動を確定する
                                    gridPoint.x += dx;
                                    gridPoint.z += dz;
                                    gridPoint.y += 1;
                                    SetPlayerTarget(gridPoint);
                                }
                                else {

                                }
                                
                            }
                            
                        }else if(block[1] != null && block[1].blockNumber == stage.GetGrid(gridPoint.x + dx, gridPoint.y, gridPoint.z + dz).blockNumber) {
                            if (block[1].movableFlag) {
                                gridPoint.x += dx;
                                gridPoint.z += dz;
                                SetPlayerTarget(gridPoint);
                            }
                         
                        }
                        
                    }
                }
            }
        }

       

        public Block[] GetStepFrontAndBack(Block stepBlock) {
            var grid = stage.WorldPointToGrid(stepBlock.transform.position);
            var rad = stepBlock.transform.localEulerAngles.y;
            if (rad == 0) {
                Block[] block = new Block[2];
                block[0] = stage.GetGrid(grid.x, grid.y + 1, grid.z + 1);
                block[1] = stage.GetGrid(grid.x, grid.y, grid.z - 1);
                return block;
            }
            else if (rad == 90) {
                Block[] block = new Block[2];
                block[0] = stage.GetGrid(grid.x + 1, grid.y + 1, grid.z);
                block[1] = stage.GetGrid(grid.x - 1, grid.y, grid.z);
                return block;

            }
            else if (rad == 180) {
                Block[] block = new Block[2];
                block[0] = stage.GetGrid(grid.x, grid.y + 1, grid.z - 1);
                block[1] = stage.GetGrid(grid.x, grid.y, grid.z + 1);
                return block;
            }
            else {
                Block[] block = new Block[2];
                block[0] = stage.GetGrid(grid.x - 1, grid.y + 1, grid.z);
                block[1] = stage.GetGrid(grid.x + 1, grid.y, grid.z);
                return block;
            }

        }
    }
}
