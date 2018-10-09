using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up : MonoBehaviour {
    public int UpCheck = 0;
    public GameObject Wall;
	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "UpCollider")
         {
            Wall.GetComponent<MeshCollider>().enabled = false;
            Debug.Log("hit");
         }
        Debug.Log("else");
        
    }

}
