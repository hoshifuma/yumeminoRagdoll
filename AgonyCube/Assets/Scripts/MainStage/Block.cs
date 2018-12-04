using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgonyCube.MainStage {
    public class Block : MonoBehaviour {
        //ブロックの状態
        public int BlockId = -1;
        //隣接ブロックの情報
        public Block[] adjacentBlock = new Block[4];
       
        //ブロック番号
        public int blockNumber;
       
        //自身の床
        public GameObject floor;
        //このブロックに移動できるか
        public bool movableFlag = false;
    }
}