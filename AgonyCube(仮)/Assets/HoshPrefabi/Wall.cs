using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public GameObject UpCollider;
    public int Floorcheck;

    
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
        if (UpCollider.GetComponent<Up>().UpCheck == 1)
        {
            GetComponent<MeshCollider>().enabled = false;
            Debug.Log(UpCollider.GetComponent<Up>().UpCheck);
        }
        else
        {
            GetComponent<MeshCollider>().enabled = true;
            Debug.Log(UpCollider.GetComponent<Up>().UpCheck);
        }
        
    }
}
