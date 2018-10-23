using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

    public GameObject Ceiling;
    public GameObject Floor;

    


	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        
    }
    //アタッチされているroomにキャラの移動ができるかを返す関数
     public bool MoveCheck()
    {
        //下が床の場合trueを返す
        if (transform.position.y > Floor.transform.position.y)
        {
            return true;
        }
        else
        {
            //下が天井の場合、天井が別のキューブの床に触れているかを確認、触れていた場合trueを返す
            if (Ceiling.GetComponent<Wall>().FloorCheck == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
}
