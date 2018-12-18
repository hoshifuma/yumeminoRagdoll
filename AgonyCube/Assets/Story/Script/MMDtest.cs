using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgonyCube.Test
{
    public class MMDtest : MonoBehaviour
    {

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            if (Input.GetKeyUp(KeyCode.Return)) {
                ScreenCapture.CaptureScreenshot("ScreenShot.png");
            }
        }
    }
}