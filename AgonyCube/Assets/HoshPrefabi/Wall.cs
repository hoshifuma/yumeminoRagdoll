using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {


    public int FloorCheck = 0;

    
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
      
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Floor")
        {
            FloorCheck = 1;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(!(other.gameObject.tag == "Player"))
        {
            FloorCheck = 0;
        }
       
    }
}
