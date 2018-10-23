using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepController : MonoBehaviour {

    public GameObject Wall;
    public GameObject Floor;

    public LayerMask PlayerLayer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       

        
    }

    public bool StepMoveCheck()
    {

        if(Floor.transform.position.y <=( transform.position.y + 0.5))
        {
            if(Wall.GetComponent<Wall>().FloorCheck == false)
            {
                Ray stepray = new Ray(transform.position, -transform.forward);
                RaycastHit hit;
             
               
                if (Physics.Raycast(stepray, out hit, 3, PlayerLayer))
                {
                    return true;
                }
               else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }
        else
        {
            return false;
        }
    }
}
