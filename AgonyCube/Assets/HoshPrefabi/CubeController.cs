using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

    public GameObject[] Wall;
    public GameObject Floor;

    private int i;


	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        
    }

     public bool MoveCheck()
    {

        if (transform.position.y > Floor.transform.position.y)
        {
            return true;
        }
        else
        {
            for (i = 0; i < 5; i++)
            {
                if (transform.position.y > Wall[i].transform.position.y)
                {
                    if(Wall[i].GetComponent<Wall>().FloorCheck == 1)
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

        return false;
        
    }
}
