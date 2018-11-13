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
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
