using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotate : MonoBehaviour
{
    public GameObject[,,] Cube = new GameObject[3,3,3];
    public GameObject Centar;
    public GameObject Around1;
    public GameObject Around2;
    public GameObject Around3;
    public GameObject Around4;
    public GameObject Around5;
    public GameObject Around6;
    public GameObject Around7;
    public GameObject Around8;
    public string CubeTag = "Cube";
    float minAngle = 0.0F;
    float maxAngle = 180.0F;
    // Update is called once per frame  
    private void OnCollisionEnter(Collision collision)
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray();
            RaycastHit hit = new RaycastHit();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //マウスクリックした場所からRayを飛ばし、オブジェクトがあればtrue 
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.CompareTag(CubeTag))
                {
                    Around1.transform.parent = Centar.transform;
                    Around2.transform.parent = Centar.transform;
                    Around3.transform.parent = Centar.transform;
                    Around4.transform.parent = Centar.transform;
                    Around5.transform.parent = Centar.transform;
                    Around6.transform.parent = Centar.transform;
                    Around7.transform.parent = Centar.transform;
                    Around8.transform.parent = Centar.transform;
                    float angle = Mathf.LerpAngle(minAngle, maxAngle, Time.time);
                    Centar.transform.eulerAngles = new Vector3(0, angle, 0);
                }
            }
        }
    }
}
