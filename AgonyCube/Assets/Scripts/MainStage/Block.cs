using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgonyCubeMainStage {
    public class Block : MonoBehaviour {
        //ブロックの状態
        public int BlockId = -1;
        //隣接ブロックの情報
        public Block[] adjacentBlock = new Block[4];
        ////階段用前のBlockを保存
        //public Block frontBlock;
        ////階段用上った先のBlockを保存
        //public Block upperFloor;
        //ブロック番号
        public int blockNumber;
       
        //自身の床
        public GameObject floor;
        //このブロックに移動できるか
        public bool movableFlag = false;
    }
}