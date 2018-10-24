using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class camera : MonoBehaviour
{


    public float RotationSpeed;
    public float RotationRadian;
    private Vector2 LastMousePosition;
    private Vector2 InputMousePosition;
    private Vector2 NewAngle = new Vector2(10, 200);
    private float rotation = 0;

    private int HitCheck = 0;
    public LayerMask Cube;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // UIのオブジェクトに交差している場合はHitCheck = 0;
        var horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        var vertical = CrossPlatformInputManager.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) < 0.1f && Mathf.Abs(vertical) < 0.1f) {
            if (Input.GetMouseButtonDown(0)) {

                LastMousePosition = Input.mousePosition;
                InputMousePosition = Input.mousePosition;
                // Cubeと交差している場合
                Ray Mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(Mouseray, 10.0f, Cube) == true) {
                    HitCheck = 0;
                }
                else {
                    HitCheck = 1;
                }

                Debug.Log("hitcheck" + HitCheck);

            }
            if (HitCheck == 1) {
                if (Input.GetMouseButton(0)) {
                    rotation = LastMousePosition.y - Input.mousePosition.y;

                    if (rotation > 4 || rotation < -4) {
                        NewAngle.x += (rotation) * RotationSpeed;
                    }

                    LastMousePosition = Input.mousePosition;



                    if (InputMousePosition.x - LastMousePosition.x >= 150) {
                        NewAngle.y -= RotationRadian;

                        InputMousePosition.x = Input.mousePosition.x;
                    }
                    else if (InputMousePosition.x - LastMousePosition.x <= -150) {
                        NewAngle.y += RotationRadian;

                        InputMousePosition.x = Input.mousePosition.x;
                    }

                    transform.localEulerAngles = NewAngle;
                }

            }
        }
        else {
            // UIに入力がある場合
            // horizontal -1.0f～1.0f
            HitCheck = 0;
        }
    }
}
