﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace AgonyCube.MainStage
{
    public class GameDirector : MonoBehaviour
    {
        //Blockのレイヤーマスク
        public LayerMask cube;
        //キューブの周りにある壁に付与するレイヤーマスク
        public LayerMask wall;
        //一つ目絵に選択されたBlockを保存する変数
        public GameObject choice1;
        //二つ目に選択されたBlockを保存する変数 swapに使用
        public GameObject choice2;
        //カメラを保存
        public GameObject mainCamera;
        //stageのプレファブを保存
        [SerializeField]
        GameObject[] stagePrefs;
        //StageControllerを指定
        public StageController stage;
        //ハサミを持っているかを判定
        public bool scissors = false;
        //SpinSelectStateの矢印
        public GameObject arrows;
        public RectTransform arrowsPosi;
        //spin回数を一時的に保存
        public int spin = 0;
        //swap回数を一時的に保存
        public int swap = 0;
        //spinするBlockを保存
        public Block[] spinBlock;
        //spinするときの中心点
        public Vector3 spinCenter = new Vector3(0, 0, 0);
        //spinする軸
        public Vector3 spinRote = new Vector3(0, 0, 0);
        //spinする向き
        [SerializeField]
        int spinDirection = 0;
        //1フレームでspinする角度
        [SerializeField]
        int spinRad = 0;
        //マテリアル
        [SerializeField]
        Material normal;
        [SerializeField]
        Material select1;
        [SerializeField]
        Material select2;
        //扉を保存
        public GameObject door;
        //ハートを保存
        public GameObject hert;
        //鋏を保存
        public GameObject scissorsObject;
        //animator
        private Animator choice1Animator;
        private Animator choice2Animator;

        // アニメーションID
        static readonly int BigId = Animator.StringToHash("Big");

        static readonly int SmallId = Animator.StringToHash("Small");
        GameState currentState = null;
        public PlayerController player;

        private class MainScene : GameState
        {
            protected GameDirector gameDirector;

            public MainScene(GameDirector gameDirector)
            {
                this.gameDirector = gameDirector;
            }
        }

        //通常状態
        private class IdleState : MainScene
        {
            //クリックされている時間を保存する変数
            float tapTime = 0;
            public IdleState(GameDirector gameDirector) : base(gameDirector)
            {

            }
            public override void Start()
            {
                gameDirector.player.GetComponent<PlayerController>().playerState = PlayerController.PlayerState.Idle;
            }

            public override void Update()
            {
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
                            gameDirector.ChangeState(new SpinSelectState(gameDirector));
                        }
                    }
                }

                if (Input.GetMouseButtonUp(0)) {
                    //tapTimeが一定値を超える前にマウスのボタンが離された場合
                    if (gameDirector.choice1 != null) {
                        gameDirector.ChangeState(new SwapSelectState(gameDirector));
                    }
                }
            }
        }

        //Swapモード
        private class SwapSelectState : MainScene
        {
            //クリック開始時のマウス位置を保存する変数
            Vector3 startMousePosi;
            //マウスを離す直前のマウス位置を保存する変数
            Vector3 lastMousePosi;
            public SwapSelectState(GameDirector gameDirector) : base(gameDirector)
            {

            }
            public override void Start()
            {
                gameDirector.player.GetComponent<PlayerController>().playerState = PlayerController.PlayerState.None;
                //Swapする段以外のBlockを非表示
                gameDirector.stage.InvisibleBlock(gameDirector.choice1);
                gameDirector.choice1 = null;

            }

            public override void Update()
            {
                if (Input.GetMouseButtonDown(0)) {
                    ////クリック開始時のマウスポジションを保存
                    startMousePosi = Input.mousePosition;
                    startMousePosi = Camera.main.ScreenToViewportPoint(startMousePosi);
                    if (gameDirector.choice1 == null) {
                        gameDirector.choice1 = gameDirector.SwapCheckBlockClick();
                        if (gameDirector.choice1 != null) {
                            //Blockがクリックされていた場合選択状態に変更

                            foreach (Transform child in gameDirector.choice1.transform) {
                                //指定されたBlockのマテリアルを選択状態に変更
                                if (gameDirector.choice1.GetComponent<Block>().floor != child.gameObject && child.tag != "Step") {

                                    var mats = child.GetComponent<MeshRenderer>().materials;
                                    mats[0] = gameDirector.select1;
                                    child.GetComponent<MeshRenderer>().materials = mats;
                                }
                            }

                        }
                        //else {
                        //    gameDirector.choice1 = null;
                        //    gameDirector.ChangeState(new IdleState(gameDirector));
                        //}
                    }
                    else if (gameDirector.SwapCube()) {
                        //２つ目のBlockが選択された場合
                        gameDirector.ChangeState(new SwapState(gameDirector));
                    }//else {
                    //    foreach (Transform child in gameDirector.choice1.transform) {
                    //        if (gameDirector.choice1.GetComponent<Block>().floor != child.gameObject && child.tag != "Step") {

                    //            var mats = child.GetComponent<MeshRenderer>().materials;
                    //            mats[0] = gameDirector.normal;
                    //            child.GetComponent<MeshRenderer>().materials = mats;
                    //        }
                    //    }
                    //    gameDirector.choice1 = null;
                    //    gameDirector.ChangeState(new IdleState(gameDirector));
                    //}

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
                    if (gameDirector.choice1 == null) {
                        if (mousePosi.x < 0.01 && mousePosi.y < 0.01) {
                            //マウスの入力がクリック時から変更がない場合

                            gameDirector.ChangeState(new IdleState(gameDirector));
                        }
                    }
                }
            }

            public override void Exsit()
            {
                gameDirector.stage.UnInvisibleBlock();

            }
        }

        //Swap中のState
        private class SwapState : MainScene
        {
            public SwapState(GameDirector gameDirector) : base(gameDirector)
            {

            }

            public override void Start()
            {
                gameDirector.choice1Animator = gameDirector.choice1.GetComponent<Animator>();
                gameDirector.choice2Animator = gameDirector.choice2.GetComponent<Animator>();

                gameDirector.StartCoroutine(gameDirector.SmallBlock());

            }

            public override void Update()
            {

            }

            public override void Exsit()
            {
                foreach (Transform child in gameDirector.choice1.transform) {
                    if (gameDirector.choice1.GetComponent<Block>().floor != child.gameObject && child.tag != "Step") {

                        var mats = child.GetComponent<MeshRenderer>().materials;
                        mats[0] = gameDirector.normal;
                        child.GetComponent<MeshRenderer>().materials = mats;
                    }
                }
                foreach (Transform child in gameDirector.choice2.transform) {
                    if (gameDirector.choice2.GetComponent<Block>().floor != child.gameObject && child.tag != "Step") {

                        var mats = child.GetComponent<MeshRenderer>().materials;
                        mats[0] = gameDirector.normal;
                        child.GetComponent<MeshRenderer>().materials = mats;
                    }
                }

                gameDirector.stage.UpdateGridData();
            }
        }


        //Spinモード
        private class SpinSelectState : MainScene
        {

            public SpinSelectState(GameDirector gameDirector) : base(gameDirector)
            {

            }

            //SpinSelectState開始時のマウス位置を保存する変数
            Vector3 startMousePosi;
            //マウスを離す直前のマウス位置を保存する変数
            Vector3 lastMousePosi;
            //クリックされているBlockの面を保存
            GameObject wall;

            public override void Start()
            {
                gameDirector.player.GetComponent<PlayerController>().playerState = PlayerController.PlayerState.None;
                //初期のマウスポジションを保存
                startMousePosi = Input.mousePosition;
                startMousePosi = Camera.main.ScreenToViewportPoint(startMousePosi);
                wall = gameDirector.CheckWallClick();

                //arrowsの表示
                gameDirector.arrows.SetActive(true);
                gameDirector.arrowsPosi.position = RectTransformUtility.WorldToScreenPoint(Camera.main, wall.transform.position);



            }

            public override void Update()
            {
                if (Input.GetMouseButton(0)) {

                    //最新のマウスポジションを保存
                    lastMousePosi = Input.mousePosition;
                    lastMousePosi = Camera.main.ScreenToViewportPoint(lastMousePosi);
                }

                if (Input.GetMouseButtonUp(0)) {
                    //マウス入力の差を求める
                    var mousePosi = lastMousePosi - startMousePosi;

                    var mousePosiAbsoluteValue = new Vector2(Mathf.Abs(mousePosi.x), Mathf.Abs(mousePosi.y));
                    if (mousePosiAbsoluteValue.x > 0.1 || mousePosiAbsoluteValue.y > 0.1) {
                        //マウス入力があった場合
                        if (mousePosiAbsoluteValue.x < mousePosiAbsoluteValue.y) {
                            //マウス入力の差が上下の場合

                            if (wall.tag == "RightAndLeft") {
                                gameDirector.stage.ZSpinBlock(gameDirector.choice1);
                                if (Mathf.Floor(gameDirector.mainCamera.transform.localEulerAngles.y) == 270) {
                                    if (mousePosi.y > 0) {

                                        gameDirector.spinDirection = 1;
                                    }
                                    else {
                                        gameDirector.spinDirection = -1;
                                    }
                                }
                                else {
                                    if (mousePosi.y > 0) {

                                        gameDirector.spinDirection = -1;
                                    }
                                    else {
                                        gameDirector.spinDirection = 1;
                                    }
                                }
                            }
                            else if (wall.tag == "FrontAndBehind") {
                                gameDirector.stage.XSpinBlock(gameDirector.choice1);

                                if (Mathf.Floor(gameDirector.mainCamera.transform.localEulerAngles.y) == 0) {
                                    if (mousePosi.y > 0) {

                                        gameDirector.spinDirection = 1;
                                    }
                                    else {
                                        gameDirector.spinDirection = -1;
                                    }
                                }
                                else {
                                    if (mousePosi.y > 0) {

                                        gameDirector.spinDirection = -1;
                                    }
                                    else {
                                        gameDirector.spinDirection = 1;
                                    }
                                }
                            }
                            else if (wall.tag == "UpAndDown") {
                                if (Mathf.Floor(gameDirector.mainCamera.transform.localEulerAngles.y) % 180 == 0) {
                                    gameDirector.stage.XSpinBlock(gameDirector.choice1);
                                    if (mousePosi.y > 0) {
                                        gameDirector.spinDirection = 1;
                                    }
                                    else {
                                        gameDirector.spinDirection = -1;
                                    }
                                }
                                else {
                                    gameDirector.stage.ZSpinBlock(gameDirector.choice1);
                                    if (mousePosi.y > 0) {
                                        gameDirector.spinDirection = 1;
                                    }
                                    else {
                                        gameDirector.spinDirection = -1;
                                    }
                                }
                            }

                        }
                        else if (mousePosiAbsoluteValue.x > mousePosiAbsoluteValue.y) {
                            //マウス入力の差が左右の場合




                            if (wall.tag == "RightAndLeft") {
                                gameDirector.stage.YSpinBlock(gameDirector.choice1);
                                if (mousePosi.x > 0) {
                                    gameDirector.spinDirection = -1;
                                }
                                else {
                                    gameDirector.spinDirection = 1;
                                }
                            }
                            else if (wall.tag == "FrontAndBehind") {
                                gameDirector.stage.YSpinBlock(gameDirector.choice1);
                                if (mousePosi.x > 0) {
                                    gameDirector.spinDirection = -1;
                                }
                                else {
                                    gameDirector.spinDirection = 1;
                                }
                            }
                            else if (wall.tag == "UpAndDown") {
                                if (gameDirector.mainCamera.transform.localEulerAngles.y % 180 == 0) {
                                    gameDirector.stage.ZSpinBlock(gameDirector.choice1);
                                    if (Mathf.Floor(gameDirector.mainCamera.transform.localEulerAngles.y) == 0) {
                                        if (mousePosi.x > 0) {

                                            gameDirector.spinDirection = -1;
                                        }
                                        else {
                                            gameDirector.spinDirection = 1;
                                        }
                                    }
                                    else {
                                        if (mousePosi.x > 0) {

                                            gameDirector.spinDirection = 1;
                                        }
                                        else {
                                            gameDirector.spinDirection = -1;
                                        }
                                    }
                                }
                                else {
                                    gameDirector.stage.XSpinBlock(gameDirector.choice1);

                                    if (mousePosi.x > 0) {
                                        gameDirector.spinDirection = 1;
                                    }
                                    else {
                                        gameDirector.spinDirection = -1;
                                    }
                                }
                            }
                        }
                        gameDirector.ChangeState(new SpinState(gameDirector));
                    }
                    else {
                        gameDirector.ChangeState(new IdleState(gameDirector));

                    }
                }
            }

            public override void Exsit()
            {
                gameDirector.arrows.SetActive(false);
                gameDirector.choice1 = null;

            }
        }
        //Spin中のステート
        private class SpinState : MainScene
        {
            public SpinState(GameDirector gameDirector) : base(gameDirector)
            {

            }

            int roteFrame = 0;

            public override void Start()
            {
                roteFrame = 180 / gameDirector.spinRad;

            }

            public override void Update()
            {
                if (roteFrame > 0) {

                    foreach (Block child in gameDirector.spinBlock) {
                        child.transform.RotateAround
                        (gameDirector.spinCenter, gameDirector.spinRote, gameDirector.spinDirection * gameDirector.spinRad);
                    }
                    roteFrame -= 1;
                }
                else {
                    gameDirector.ChangeState(new IdleState(gameDirector));

                }
            }

            public override void Exsit()
            {
                gameDirector.spinBlock = null;
                gameDirector.spin += 1;
                gameDirector.stage.UpdateGridData();
            }
        }

        //キャラクターが動いている状態
        private class PlayerMoveState : MainScene
        {
            public PlayerMoveState(GameDirector gameDirector) : base(gameDirector)
            {

            }


        }

        private class PauseState : MainScene
        {
            public PauseState(GameDirector gameDirector) : base(gameDirector)
            {

            }

            public override void Start()
            {
                gameDirector.player.playerState = PlayerController.PlayerState.None;
            }
        }

        void ChangeState(GameState newState)
        {
            if (currentState != null) {
                currentState.Exsit();
            }
            currentState = newState;
            currentState.Start();
        }
        private void Awake()
        {
            var stageClone = Instantiate(stagePrefs[Data.instance.stageNum]);
            stage = stageClone.GetComponent<StageController>();
            player.stage = stage;
            stage.player = player;
            stage.gameDirector = this;
            stage.door = door;
            stage.scissors = scissorsObject;
            stage.hert = hert;
            stage.cameraRoot = mainCamera;
            //foreach (GameObject child in stageClone.transform) {
            //    if (child.tag == "Stage") {
            //        stage = child.GetComponent<StageController>();
            //    }
            //    if (child.tag == "Player") {
            //        player = child.GetComponent<PlayerController>();
            //    }
            //}
        }

        // Use this for initialization
        void Start()
        {
            ChangeState(new IdleState(this));
        }



        // Update is called once per frame
        void Update()
        {
            if (currentState != null) {
                currentState.Update();
            }
        }

        private IEnumerator BigBlock()
        {
            choice1Animator.SetBool(BigId, true);
            choice2Animator.SetBool(BigId, true);

            yield return new WaitForSeconds(0.5f);

            while (true) {
                var info = choice1Animator.GetCurrentAnimatorStateInfo(0);
                var info1 = choice2Animator.GetCurrentAnimatorStateInfo(0);
                if (info.normalizedTime > 1.0f && info1.normalizedTime > 1.0f) {

                    break;
                }
                yield return null;
            }

            choice1Animator.SetBool(BigId, false);
            choice2Animator.SetBool(BigId, false);

            ChangeState(new IdleState(this));
        }

        private IEnumerator SmallBlock()
        {
            choice1Animator.SetBool(SmallId, true);
            choice2Animator.SetBool(SmallId, true);

            yield return new WaitForSeconds(0.5f);

            while (true) {
                var info = choice1Animator.GetCurrentAnimatorStateInfo(0);
                var info1 = choice2Animator.GetCurrentAnimatorStateInfo(0);

                Debug.Log(choice2Animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
                if (info.normalizedTime > 1.0f && info1.normalizedTime > 1.0f) {
                    break;
                }
                yield return null;
            }
            choice1Animator.SetBool(SmallId, false);
            choice2Animator.SetBool(SmallId, false);

            Vector3 pos1 = choice1.transform.position;
            Vector3 pos2 = choice2.transform.position;
            choice1.transform.position = pos2;
            choice2.transform.position = pos1;
            stage.UpdateGridData();
            swap += 1;

            yield return StartCoroutine(BigBlock());

        }

        //クリックされたものがBlockの場合クリックされたBlockを返す
        private GameObject CheckBlockClick()
        {
            Ray mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(mouseray, out hit, 30.0f, cube)) {
                return hit.transform.gameObject;
            }
            return null;
        }

        private GameObject SwapCheckBlockClick()
        {
            Ray mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(mouseray, out hit, 30.0f, cube)) {
                if (player.gridPoint != stage.WorldPointToGrid(hit.transform.gameObject.transform.position)) {
                    //PlayerがいないblockのBlockを返す
                    return hit.transform.gameObject;
                }
            }
            return null;
        }

        private GameObject CheckWallClick()
        {
            Ray mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(mouseray, out hit, 30.0f, wall)) {
                return hit.transform.gameObject;
            }
            return null;
        }
        //2つ目にせんたくされたBlockがchoice1とswapできた場合選択されたBlockをchoice2に保存しtrueを返す
        private bool SwapCube()
        {
            Ray mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;


            //rayがキューブに当たった際に起動
            if (Physics.Raycast(mouseray, out hit, 30.0f, cube)) {
                //rayが当たったキューブがplayerのいるキューブじゃないことを確認
                if (player.gridPoint != stage.WorldPointToGrid(hit.transform.gameObject.transform.position)) {
                    choice2 = hit.transform.gameObject;
                    //選択されたものが同じものだった場合選択状態を解除し変数を初期状態に変更
                    if (choice1 == choice2) {
                        foreach (Transform child in choice1.transform) {
                            Debug.Log(choice1.GetComponent<Block>().floor);
                            if (choice1.GetComponent<Block>().floor != child.gameObject && child.tag != "Step") {

                                var mats = child.GetComponent<MeshRenderer>().materials;
                                mats[0] = normal;
                                child.GetComponent<MeshRenderer>().materials = mats;
                            }
                        }
                        choice1 = null;
                        choice2 = null;
                    }
                    else {
                        for (int index = 0; index < 4; index++) {
                            if (choice1.GetComponent<Block>().adjacentBlock[index] != null) {
                                if (choice1.GetComponent<Block>().adjacentBlock[index].blockNumber == choice2.GetComponent<Block>().blockNumber) {
                                    foreach (Transform child in choice2.transform) {
                                        //指定されたBlockのマテリアルを選択状態に変更
                                        if (choice2.GetComponent<Block>().floor != child.gameObject && child.tag != "Step") {

                                            var mats = child.GetComponent<MeshRenderer>().materials;
                                            mats[0] = select2;
                                            child.GetComponent<MeshRenderer>().materials = mats;
                                        }
                                        return true;
                                    }
                                }
                            }
                        }


                        //選択された2つのキューブが隣り合っていた場合positionの交換をし選択状態の解除、変数の初期化









                    }
                }
            }
            choice2 = null;
            return false;
        }

        public void ChangeMoveState()
        {
            player.playerState = PlayerController.PlayerState.Locmotion;
            ChangeState(new PlayerMoveState(this));
        }

        public void ChangeIdleState()
        {
            player.playerState = PlayerController.PlayerState.Idle;
            ChangeState(new IdleState(this));
        }


    }
}
