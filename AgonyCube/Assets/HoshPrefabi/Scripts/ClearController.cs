using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearController : MonoBehaviour {

    public GameObject[] Wall;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool ClearMoveCheck()
    {
        Debug.Log("Clear");
        for(int i = 0; i < 2; i++)
        {
            if (Wall[i].transform.position.y <= transform.position.y)
            {
                if (Wall[i].GetComponent<Wall>().FloorCheck == true)
                {
                    return true;
                }
                else
                {
                    Debug.Log("q");
                    return false;
                }
            }
        }

        return false;
    }
}
