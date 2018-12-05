using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{

    private Camera cam;
    private float zoom;
    private float view;
    float startTime;
    void Start()
    {
        cam = GetComponent<Camera>();
        view = cam.fieldOfView;
    }


    void Update()
    {
        cam.fieldOfView = view + zoom;

        if (Time.time - startTime > 5f)
        {


            //上を押すと、zoomの数値が減少
            if (Input.GetKey(KeyCode.UpArrow))
            {

                zoom -= 0.5f;
                //下を押すと、zoomの数値が増加
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                zoom += 0.5f;
            }
        }
    }
}