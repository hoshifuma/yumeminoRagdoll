using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    //ブロックの状態
    public int BlockId = -1;
    //隣接ブロックの情報
    public Block[] adjacentBlock = new Block[4];
    //ブロック番号
    public int blockNumber;

    public GameObject block;

    private void Start() {
        block = transform.gameObject;
    }


}
