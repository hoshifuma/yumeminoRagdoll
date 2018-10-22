using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSC : MonoBehaviour {

    GameObject CameraPosition;

	// Use this for initialization
	void Start () {
        CameraPosition = GameObject.Find("CameraPosition");
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position = CameraPosition.transform.position;
        this.transform.rotation = CameraPosition.transform.rotation;
    }
}

