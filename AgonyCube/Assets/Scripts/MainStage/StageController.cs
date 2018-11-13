using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgonyCubeMainStage {
    public class StageController : MonoBehaviour {
        //ブロックの種類
        public GameObject[] brockPrefabs;
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
            if(gridPoint.x < 0 || gridPoint.x > gridWidth ||
                gridPoint.y < 0 || gridPoint.y > gridHeight ||
                gridPoint.z < 0 || gridPoint.z > gridLength) {
                return null;
            }
            else {
                return gridData[(gridPoint.y * gridLength * gridWidth) + (gridPoint.z * gridWidth) + gridPoint.x];
            }
        }
        //指定したブロックの情報を取得
        public Block GetGrid(int gridX, int gridY, int gridZ) {
            //指定した場所がgridDataの範囲外の場合NULLを返却
            if (gridX < 0 || gridX > gridWidth ||
                gridY < 0 || gridY > gridHeight ||
                gridZ < 0 || gridZ > gridLength) {
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
            CalcurateAdgency();
        }

        private void CalcurateAdgency() {
            //gridDataのすべてのグリッドをループ
            for (var gridY = 0; gridY < gridHeight; gridY++) {
                for (var gridZ = 0; gridZ < gridLength; gridZ++) {
                    for (var gridX = 0; gridX < gridWidth; gridX++) {
                        var block = GetGrid(gridX, gridY, gridZ);

                        var northBock = GetGrid(gridX, gridY, gridZ + 1);//北
                        var eastBlock = GetGrid(gridX + 1, gridY, gridZ);//東
                        var southBlock = GetGrid(gridX, gridY, gridZ - 1);//南
                        var westBlock = GetGrid(gridX - 1, gridY, gridZ);//西

                        if (northBock != null || northBock.BlockId > 0) {
                            block.adjacentBlock[0] = northBock;
                        }
                        if (eastBlock != null || eastBlock.BlockId > 0) {
                            block.adjacentBlock[1] = eastBlock;
                        }
                        if (southBlock != null || southBlock.BlockId > 0) {
                            block.adjacentBlock[0] = southBlock;
                        }
                        if (westBlock != null || westBlock.BlockId > 0) {
                            block.adjacentBlock[0] = westBlock;
                        }
                    }
                }
            }
        }

        private void GenerateGridData() {
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

            foreach (Transform child in transform) {
                var gridPoint = WorldPointToGrid(child.position);

                SetGrid(gridPoint, child.GetComponent<Block>());
            }
        }

        // Update is called once per frame
        void Update() {

        }
    }
}