using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AgonyCube.MainStage {
    public class ItemScript : MonoBehaviour {

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            this.transform.Rotate(0, 1, 0);
        }
    }
}