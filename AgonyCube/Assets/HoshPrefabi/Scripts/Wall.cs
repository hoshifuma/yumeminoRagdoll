using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    //下が床か確認するための変数CubeControllerで引用
    public bool FloorCheck = false;

    
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
      
        
    }

    public void OnTriggerEnter(Collider other)
    {
       
        //床に当たった際にtrueに変更
        if (other.gameObject.tag == "Floor")
        {
            FloorCheck = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        //player以外から離れた際にfalseに変更
        if(!(other.gameObject.tag == "Player"))
        {
            FloorCheck = false;
        }
       
    }
}
