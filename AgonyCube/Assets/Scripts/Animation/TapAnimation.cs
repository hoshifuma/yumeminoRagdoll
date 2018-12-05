using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapAnimation : MonoBehaviour {

    private Vector3 clickPosition;
    public GameObject clickObject;

    void Update()
    {
        clickPosition = Input.mousePosition;

        clickPosition.z = 10f;

        clickObject.transform.position = clickPosition;
    }
}
