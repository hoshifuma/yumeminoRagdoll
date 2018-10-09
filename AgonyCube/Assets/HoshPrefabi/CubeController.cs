using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

    public GameObject[] Wall;
    public GameObject Floor;
    public GameObject NowFloor;
    public GameObject CenterCollider;

    public int l;
    private int i;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > Floor.transform.position.y)
        {
            NowFloor = Floor;
        }
        else
        {
            for (i = 0; i <= 5; i++)
            {
                if (transform.position.y > Wall[i].transform.position.y)
                {
                    NowFloor = Wall[i];
                    
                }
               
            }


        }

        
    }
}
