using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgonyCube.MainStage {
    public class StageController : MonoBehaviour {
        //グリッドのX軸のサイズ
        int gridWidth = 0;
        //グリッドのZ軸のサイズ   
        int gridLength = 0;
        //キューブのY軸のサイズ
        int gridHeight = 0;
        
        //グリッドのY軸のサイズ
        int blockHeight = 0;
        //グリッドの状態を保存
        public Block[] gridData;
        //一つのグリッドの大きさ
        public int gridSize = 2;
        //扉を保存
        public GameObject door;
        //ドアのある場所を保存
        public Vector3Int doorGrid;
        public float doorRad;
        //ハートを保存
        public GameObject hert;
        //ハートのある場所を保存
        public Vector3Int hertGrid;
        //鋏を保存
        public GameObject scissors;
        public GameObject cameraRoot;
        //鋏のある場所を保存
        public Vector3Int scissorsGrid;
        //playerControllerを指定
        public PlayerController player;

        public GameDirector gameDirector;
        //ワールド座標をグリッドポイントに変換
        public Vector3Int WorldPointToGrid(Vector3 position) {
            return new Vector3Int(
               Mathf.RoundToInt(position.x / gridSize),
               Mathf.RoundToInt(position.y / gridSize),
               Mathf.RoundToInt(position.z / gridSize));
        }
        //グリッドポイントをワールド座標に変換
        public Vector3 GridToWorldPoint(Vector3Int gridPoint) {
            return new Vector3(
                (gridPoint.x * gridSize),
                (gridPoint.y * gridSize),
                (gridPoint.z * gridSize));
        }
        //gridDataをset
        public void SetGrid(Vector3Int gridPoint, Block value) {
            gridData[(gridPoint.y * gridLength * gridWidth) + (gridPoint.z * gridWidth) + gridPoint.x] = value;
        }
        //指定したブロックの情報を取得
        public Block GetGrid(Vector3Int gridPoint) {
            //指定した場所がgridDataの範囲外の場合NULLを返却
            if (gridPoint.x < 0 || gridPoint.x >= gridWidth ||
                gridPoint.y < 0 || gridPoint.y >= blockHeight ||
                gridPoint.z < 0 || gridPoint.z >= gridLength) {
                return null;
            }
            else {
                return gridData[(gridPoint.y * gridLength * gridWidth) + (gridPoint.z * gridWidth) + gridPoint.x];
            }
        }
        //指定したブロックの情報を取得
        public Block GetGrid(int gridX, int gridY, int gridZ) {
            //指定した場所がgridDataの範囲外の場合NULLを返却
            if (gridX < 0 || gridX >= gridWidth ||
                gridY < 0 || gridY >= blockHeight ||
                gridZ < 0 || gridZ >= gridLength) {
                return null;
            }
            else {
                return gridData[(gridY * gridLength * gridWidth) + (gridZ * gridWidth) + gridX];
            }
        }
        //キャラクターが移動可能か確認
        public bool CheckMovableGrid(Vector3Int gridPoint) {
            var block = GetGrid(gridPoint);
            return (block != null && block.BlockId > 0);
        }

        // Use this for initialization
        void Start() {

            
            CreateBlockNumber();
            GenerateGridData();
            RoopBlock();
            for (var gridZ = 0; gridZ < gridLength; gridZ++) {
                for (var gridX = 0; gridX < gridWidth; gridX++) {
                    //Blockに隣接情報を付与
                    CalcurateAdgency(gridX, blockHeight - 1, gridZ);
                }
            }
            cameraRoot.transform.position = new Vector3(gridWidth / 2 + 1, gridHeight / 2 + 1, gridLength / 2 + 1);
            scissors.transform.position = GridToWorldPoint(scissorsGrid);
            door.transform.position = GridToWorldPoint(doorGrid);
            hert.transform.position = GridToWorldPoint(hertGrid);
            door.transform.rotation = new Quaternion(0, doorRad,0,0);
        }

        //外側の壁にtagを付与
        private void GrantTagWall(int gridX, int gridY, int gridZ) {
            var block = GetGrid(gridX, gridY, gridZ);
            //Y軸で端の壁にtag,layer,colliderを付与
            if (gridY == gridHeight - 1) {

                foreach (Transform child in block.transform) {
                    if (child.transform.position.y - block.transform.position.y > 0.5) {
                        // child.gameObject.SetActive(true);
                        if (child.transform.tag == "Untagged") {
                            child.tag = "UpAndDown";
                        
                            child.GetComponent<BoxCollider>().enabled = true;
                        }
                      
                        //child.GetComponent<MeshRenderer>().enabled = true;
                    }
                }
            }
            else if (gridY == 0) {

                foreach (Transform child in block.transform) {
                    if (child.transform.position.y - block.transform.position.y < -0.5) {
                        //  child.gameObject.SetActive(true);
                        if (child.transform.tag == "Untagged") {
                            child.tag = "UpAndDown";
                        
                            child.GetComponent<BoxCollider>().enabled = true;
                        }

                        //child.GetComponent<MeshRenderer>().enabled = true;
                    }
                }
            }
            //X軸で端の壁にtag,layer,colliderを付与
            if (gridX == gridWidth - 1) {

                foreach (Transform child in block.transform) {
                    if (child.transform.position.x - block.transform.position.x > 0.5) {

                        if (child.transform.tag == "Untagged") {//child.gameObject.SetActive(true);
                            child.tag = "RightAndLeft";
                            child.GetComponent<BoxCollider>().enabled = true;
                            // child.GetComponent<MeshRenderer>().enabled = true;
                        }
                    }
                }
            }
            else if (gridX == 0) {
                foreach (Transform child in block.transform) {
                    if (child.transform.position.x - block.transform.position.x < -0.5) {
                        //child.gameObject.SetActive(true);
                        if (child.transform.tag == "Untagged") {
                            child.tag = "RightAndLeft";
                            child.GetComponent<BoxCollider>().enabled = true;
                            //child.GetComponent<MeshRenderer>().enabled = true;
                        }
                    }
                }
            }
            //Z軸で端の壁にtag,layer,colliderを付与
            if (gridZ == gridLength - 1) {
                foreach (Transform child in block.transform) {
                    if (child.transform.position.z - block.transform.position.z > 0.5) {
                        // child.gameObject.SetActive(true);
                        if (child.transform.tag == "Untagged") {
                            child.tag = "FrontAndBehind";
                            child.GetComponent<BoxCollider>().enabled = true;
                            // child.GetComponent<MeshRenderer>().enabled = true;
                        }
                    }
                }
            }
            else if (gridZ == 0) {
                foreach (Transform child in block.transform) {
                    if (child.transform.position.z - block.transform.position.z < -0.5) {
                        // child.gameObject.SetActive(true);
                        if (child.transform.tag == "Untagged") {
                            child.tag = "FrontAndBehind";
                            child.GetComponent<BoxCollider>().enabled = true;
                            //child.GetComponent<MeshRenderer>().enabled = true;
                        }
                    }
                }
            }
        }

        //起動時にBlockにナンバーを付与
        private void CreateBlockNumber() {
            var index = 0;
            foreach (Transform child in transform) {
                child.GetComponent<Block>().blockNumber = index;
                index++;
            }
        }
        //Blockの個数分ループgriddataのupdate時に使用
        public void RoopBlock() {
            //gridDataのすべてのグリッドをループ
            for (var gridY = 0; gridY < gridHeight; gridY++) {
                for (var gridZ = 0; gridZ < gridLength; gridZ++) {
                    for (var gridX = 0; gridX < gridWidth; gridX++) {
                        //Blockの外側の壁にtagを付与
                        GrantTagWall(gridX, gridY, gridZ);
                        //Blockに通れるか確認するflagを付与
                        GrantMovableFlag(gridX, gridY, gridZ);
                        //Blockに隣接情報を付与
                        CalcurateAdgency(gridX, gridY, gridZ);
                    }
                }
            }
        }
        //Blockに通れるか確認するflagを付与
        public void GrantMovableFlag(int gridX, int gridY, int gridZ) {
            var block = GetGrid(gridX, gridY, gridZ);

            if (block.BlockId == 1 || block.BlockId == 3) {
                var higherPlaceBlock = GetGrid(gridX, gridY + 1, gridZ);
                //床のあるBlockまたはstepの場合
                if (block.transform.position.y > block.floor.transform.position.y) {
                    //床が下にある場合自身のmovableFlagをtrueに変更
                    block.movableFlag = true;
                    var dawnPlaceBlock = GetGrid(gridX, gridY - 1, gridZ);
                    if (dawnPlaceBlock != null && dawnPlaceBlock.BlockId == 3) {
                        //下にあるBlockがstepの場合下にあるstepのmovableFlagをfalseに変更
                        dawnPlaceBlock.movableFlag = false;
                    }
                }
                else {
                    if (higherPlaceBlock != null && higherPlaceBlock.BlockId != 3) {
                        //床が上にある場合上のstep以外のBlockのmovableFlagをtrueに変更
                        higherPlaceBlock.movableFlag = true;
                    }

                }
            }
        }
        //Blockに隣接情報を付与
        private void CalcurateAdgency(int gridX, int gridY, int gridZ) {
            var block = GetGrid(gridX, gridY, gridZ);

            // if (block.BlockId != 3) {
            var northBlock = GetGrid(gridX, gridY, gridZ + 1);//北
            var eastBlock = GetGrid(gridX + 1, gridY, gridZ);//東
            var southBlock = GetGrid(gridX, gridY, gridZ - 1);//南
            var westBlock = GetGrid(gridX - 1, gridY, gridZ);//西

            if (northBlock != null) {
                block.adjacentBlock[0] = northBlock;
            }
            else {
                block.adjacentBlock[0] = null;
            }
            if (eastBlock != null) {
                block.adjacentBlock[1] = eastBlock;
            }
            else {
                block.adjacentBlock[1] = null;
            }
            if (southBlock != null) {
                block.adjacentBlock[2] = southBlock;
            }
            else {
                block.adjacentBlock[2] = null;
            }
            if (westBlock != null) {
                block.adjacentBlock[3] = westBlock;
            }
            else {
                block.adjacentBlock[3] = null;
            }
           
        }
        //gridDataの作成
        public void GenerateGridData() {
            int maxGridX = 0;
            int maxGridZ = 0;
            int maxGridY = 0;

            foreach (Transform child in transform) {
                var gridPoint = WorldPointToGrid(child.position);

                if (gridPoint.x > maxGridX) {
                    maxGridX = gridPoint.x;
                }
                if (gridPoint.z > maxGridZ) {
                    maxGridZ = gridPoint.z;
                }
                if (gridPoint.y > maxGridY) {
                    maxGridY = gridPoint.y;
                }
            }

            gridWidth = maxGridX + 1;
            gridLength = maxGridZ + 1;
            gridHeight = maxGridY;

            blockHeight = maxGridY + 1;
            gridData = new Block[gridWidth * gridLength * blockHeight];

            UpdateGridData();
        }
        //gridDataの更新
        public void UpdateGridData() {
            foreach (Transform child in transform) {
                var posi = child.transform.position;
                posi.y = Mathf.RoundToInt(posi.y);
                posi.x = Mathf.RoundToInt(posi.x);
                posi.z = Mathf.RoundToInt(posi.z);
                child.transform.position = posi;
                var gridPoint = WorldPointToGrid(child.position);
                

                SetGrid(gridPoint, child.GetComponent<Block>());
                var grid = GetGrid(gridPoint);
                grid.movableFlag = false;
                foreach (Transform wall in child.transform) {
                    if(wall.tag != "Step") {
                        wall.GetComponent<BoxCollider>().enabled = false;
                        wall.tag = "Untagged";
                    }
                    
                  
                    //wall.GetComponent<MeshRenderer>().enabled = false;
                    //wall.gameObject.SetActive(false);
                }
                if (grid.floor != null) {
                    grid.floor.GetComponent<BoxCollider>().enabled = true;
                   // grid.floor.GetComponent<MeshRenderer>().enabled = true;
                    //grid.floor.SetActive(true);
                }
            }

            RoopBlock();
        }
        
        //選択された段以外のBlockを透明化
        public void InvisibleBlock(GameObject Block) {
            var posi = WorldPointToGrid(Block.transform.position);

            for (var gridY = 0; gridY < gridHeight; gridY++) {
                for (var gridZ = 0; gridZ < gridLength; gridZ++) {
                    for (var gridX = 0; gridX < gridWidth; gridX++) {
                        if (gridY != posi.y) {
                            GetGrid(gridX, gridY, gridZ).transform.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
        //すべてのBlockの透明化を解除
        public void UnInvisibleBlock() {

            for (var gridY = 0; gridY < gridHeight; gridY++) {
                for (var gridZ = 0; gridZ < gridLength; gridZ++) {
                    for (var gridX = 0; gridX < gridWidth; gridX++) {
                        GetGrid(gridX, gridY, gridZ).transform.gameObject.SetActive(true);
                    }
                }
            }
        }
        
        //X軸の回転
        public void XSpinBlock(GameObject block) {
            var grid = WorldPointToGrid(block.transform.position);
            
            if (player.gridPoint.x != grid.x) {
                gameDirector.spinBlock = new Block[gridHeight * gridLength];
                for (var gridY = 0; gridY < gridHeight; gridY++) {
                    for (var gridZ = 0; gridZ < gridLength; gridZ++) {
                       gameDirector.spinBlock[gridY * gridLength + gridZ] =  GetGrid(grid.x, gridY, gridZ);
                    }
                }
                gameDirector.spinCenter = new Vector3(block.transform.position.x, (gridHeight * gridSize / 2) - 1, (gridLength * gridSize / 2) - 1);
                gameDirector.spinRote = Vector3.right;

            }
            else {
                gameDirector.spinBlock = new Block[0];
            }

        }
        //Y軸の回転
        public void YSpinBlock(GameObject block) {
            var grid = WorldPointToGrid(block.transform.position);
            if (player.gridPoint.y != grid.y) {
                gameDirector.spinBlock = new Block[gridLength * gridWidth];
                for (var gridZ = 0; gridZ < gridLength; gridZ++) {
                    for (var gridX = 0; gridX < gridWidth; gridX++) {
                        gameDirector.spinBlock[gridZ * gridWidth + gridX] = GetGrid(gridX, grid.y, gridZ);
                    }
                }
                gameDirector.spinCenter = new Vector3((gridWidth * gridSize / 2) - 1, block.transform.position.y, (gridLength * gridSize / 2) - 1);
                gameDirector.spinRote = Vector3.up;
            }
            else {
                gameDirector.spinBlock = new Block[0];
            }
        }
        //Z軸の回転
        public void ZSpinBlock(GameObject block) {
            var grid = WorldPointToGrid(block.transform.position);
            if (player.gridPoint.z != grid.z) {
                gameDirector.spinBlock = new Block[gridHeight * gridWidth];
                for (var gridY = 0; gridY < gridHeight; gridY++) {
                    for (var gridX = 0; gridX < gridWidth; gridX++) {
                        gameDirector.spinBlock[gridY * gridWidth + gridX] = GetGrid(gridX, gridY, grid.z);
                    }
                }
                gameDirector.spinCenter = new Vector3((gridWidth * gridSize / 2) - 1, (gridHeight * gridSize / 2) - 1, block.transform.position.z);
                gameDirector.spinRote = Vector3.forward;
            }
            else {
                gameDirector.spinBlock = new Block[0];
            }
        }
    }
}