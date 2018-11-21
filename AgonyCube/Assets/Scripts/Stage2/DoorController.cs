using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    //ドアエリアに入っているかどうか
    private bool isNear;

    //ドアのアニメーター
    private Animator animator;

	// Use this for initialization
	void Start () {
        isNear = false;
        animator = transform.parent.GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space") && isNear)
        {
            animator.SetBool("Open", !animator.GetBool("open"));

        }
	}

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            isNear = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        isNear = false;
    }
}
