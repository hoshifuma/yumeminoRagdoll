using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour {
    public GameObject MoveBlock;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Step") {            
            MoveBlock = other.gameObject;
        }else if(other.gameObject.tag == "Block") {
            MoveBlock = other.gameObject;
        }else if(other.gameObject.tag == "Clear") {
            MoveBlock = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (!(other.gameObject.tag == "Collider")) {
            MoveBlock = null;

        }
    }
}
