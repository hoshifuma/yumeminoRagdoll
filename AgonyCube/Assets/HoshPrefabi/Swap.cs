using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swap : MonoBehaviour {

    public LayerMask Cube;
    public GameObject Choice1;
    public GameObject Choice2;
    public GameObject Player;
    
    private int i = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(mouseray, out hit, 10.0f, Cube))
            {

                if (Vector3.Distance(Player.transform.position, hit.transform.gameObject.transform.position) >= 1.5)
                {
                    if (Choice1 == null)
                    {
                        Choice1 = hit.transform.gameObject;

                        Choice1.GetComponent<LineRenderer>().enabled = true;
                    }
                    else
                    {
                        Choice2 = hit.transform.gameObject;

                        if (Choice1 == Choice2)
                        {
                            Choice1.GetComponent<LineRenderer>().enabled = false;
                            Choice1 = null;
                            Choice2 = null;
                        }
                        else
                        {
                            Vector3 pos1 = Choice1.transform.position;
                            Vector3 pos2 = Choice2.transform.position;


                            float dis = Vector3.Distance(pos1, pos2);

                            Debug.Log(dis);
                            if (dis <= 2)
                            {
                                Choice1.transform.position = pos2;
                                Choice2.transform.position = pos1;

                                Choice1.GetComponent<LineRenderer>().enabled = false;

                                Choice1 = null;
                                Choice2 = null;
                            }
                            else
                            {
                                Choice2 = null;
                            }
                        }
                    }


                }
            }
        }
	}
}
