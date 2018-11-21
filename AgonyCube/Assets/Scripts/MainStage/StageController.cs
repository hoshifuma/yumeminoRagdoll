using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgonyCubeMainStage {
    public class StageController : MonoBehaviour {
        //グリッドのX軸のサイズ
        int gridWidth = 0;
        //グリッドのZ軸のサイズ   
        int gridLength = 0;
        //グリッドのY軸のサイズ
        int gridHeight = 0;
        //グリッドの状態を保存
        public Block[] gridData;
        //一つのグリッドの大きさ
        public int gridSize = 2;
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
                gridPoint.y < 0 || gridPoint.y >= gridHeight ||
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
                gridY < 0 || gridY >= gridHeight ||
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
            GenerateGridData();
            CreateBlockNumber();
            CalcurateAdgency();
        }

        //起動時に外側の壁にtagを付与
        private void GrantTagWall(int gridX, int gridY, int gridZ, Block block) {
            //Y軸で端の壁にtag,layer,colliderを付与
            if (gridY == gridHeight) {
                
                foreach (Transform child in block.block.transform) {
                    if (child.transform.position.y > block.transform.position.y) {
                        child.tag = "UpAndDown";
                        child.GetComponent<MeshCollider>().enabled = true;
                        child.gameObject.layer = LayerMask.NameToLayer("Wall");
                    }
                }
            }
            else if (gridY == 0) {
               
                foreach (Transform child in block.block.transform) {
                    if (child.transform.position.y < block.transform.position.y) {
                        child.tag = "UpAndDown";
                        child.gameObject.layer = LayerMask.NameToLayer("Wall");
                    }
                }
            }
            //X軸で端の壁にtag,layer,colliderを付与
            if(gridX == gridWidth) {
               
                foreach (Transform child in block.block.transform) {
                    if (child.transform.position.x > block.transform.position.x) {
                        child.tag = "RightAndLeft";
                        child.gameObject.layer = LayerMask.NameToLayer("Wall");
                    }
                }
            }
            else if(gridX == 0) {
                foreach (Transform child in block.block.transform) {
                    if (child.transform.position.x < block.transform.position.x) {
                        child.tag = "RightAndLeft";
                        child.gameObject.layer = LayerMask.NameToLayer("Wall");
                    }
                }
            }
            //Z軸で端の壁にtag,layer,colliderを付与
            if (gridZ == gridLength) {
                foreach (Transform child in block.block.transform) {
                    if (child.transform.position.z > block.transform.position.z) {
                        child.tag = "FrontAndBehind";
                        child.gameObject.layer = LayerMask.NameToLayer("Wall");
                    }
                }
            }
            else if(gridZ == 0) {
                foreach (Transform child in block.block.transform) {
                    if (child.transform.position.z < block.transform.position.z) {
                        child.tag = "FrontAndBehind";
                        child.gameObject.layer = LayerMask.NameToLayer("Wall");
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
        //Blockに隣接情報を付与
        public void CalcurateAdgency() {
            //gridDataのすべてのグリッドをループ
            for (var gridY = 0; gridY < gridHeight; gridY++) {
                for (var gridZ = 0; gridZ < gridLength; gridZ++) {
                    for (var gridX = 0; gridX < gridWidth; gridX++) {
                        var block = GetGrid(gridX, gridY, gridZ);

                        GrantTagWall(gridX, gridY, gridZ, block);

                        var northBock = GetGrid(gridX, gridY, gridZ + 1);//北
                        var eastBlock = GetGrid(gridX + 1, gridY, gridZ);//東
                        var southBlock = GetGrid(gridX, gridY, gridZ - 1);//南
                        var westBlock = GetGrid(gridX - 1, gridY, gridZ);//西

                        if (northBock != null && northBock.BlockId > 0) {
                            block.adjacentBlock[0] = northBock;
                        }
                        if (eastBlock != null && eastBlock.BlockId > 0) {
                            block.adjacentBlock[1] = eastBlock;
                        }
                        if (southBlock != null && southBlock.BlockId > 0) {
                            block.adjacentBlock[2] = southBlock;
                        }
                        if (westBlock != null && westBlock.BlockId > 0) {
                            block.adjacentBlock[3] = westBlock;
                        }
                    }
                }
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
            gridHeight = maxGridY + 1;

            gridData = new Block[gridWidth * gridLength * gridHeight];

            UpdateGridData();
        }
        //gridDataの更新
        public void UpdateGridData() {
            foreach (Transform child in transform) {
                var gridPoint = WorldPointToGrid(child.position);

                SetGrid(gridPoint, child.GetComponent<Block>());
            }
            CalcurateAdgency();
        }

        public void InvisibleBlock(GameObject Block) {
            var posi = WorldPointToGrid(Block.transform.position);

            for (var gridY = 0; gridY < gridHeight; gridY++) {
                for (var gridZ = 0; gridZ < gridLength; gridZ++) {
                    for (var gridX = 0; gridX < gridWidth; gridX++) {
                        if (gridY != posi.y) {
                            GetGrid(gridX, gridY, gridZ).block.SetActive(false);
                        }
                    }
                }
            }
        }

        public void UnInvisibleBlock(GameObject block) {
            var posi = WorldPointToGrid(block.transform.position);

            for (var gridY = 0; gridY < gridHeight; gridY++) {
                for (var gridZ = 0; gridZ < gridLength; gridZ++) {
                    for (var gridX = 0; gridX < gridWidth; gridX++) {
                        if (gridY != posi.y) {
                            GetGrid(gridX, gridY, gridZ).block.SetActive(true);
                        }
                    }
                }
            }
        }
        //X軸の回転
        public void XSpinBlock(GameObject block) {
            var grid = WorldPointToGrid(block.transform.position);

            

            for (var gridY = 0; gridY < gridHeight; gridY++) {
                for (var gridZ = 0; gridZ < gridLength; gridZ++) {
                    GetGrid(grid.x, gridY, gridZ).block.transform.RotateAround
                        (new Vector3(block.transform.position.x, (gridHeight * gridSize / 2) - 1, (gridLength * gridSize / 2) - 1),
                        Vector3.right, 180);
                }
            }
        }
        //Y軸の回転
        public void YSpinBlock(GameObject block) {
            var grid = WorldPointToGrid(block.transform.position);

            for (var gridZ = 0; gridZ < gridLength; gridZ++) {
                for (var gridX = 0; gridX < gridWidth; gridX++) {
                    GetGrid(gridX, grid.y, gridZ).block.transform.RotateAround
                        (new Vector3((gridWidth * gridSize / 2) - 1, block.transform.position.y, (gridLength * gridSize / 2) - 1), 
                        Vector3.up, 180);
                }
            }
        }
        //Z軸の回転
        public void ZSpinBlock(GameObject block) {
            var grid = WorldPointToGrid(block.transform.position);

            for (var gridZ = 0; gridZ < gridLength; gridZ++) {
                for (var gridX = 0; gridX < gridWidth; gridX++) {
                    GetGrid(gridX, grid.y, gridZ).block.transform.RotateAround
                        (new Vector3((gridWidth * gridSize / 2) - 1, block.transform.position.y, (gridLength * gridSize / 2) - 1),
                        Vector3.up, 180);
                }
            }
        }
    }
}