using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgonyCubeMainStage {
    public class HitCheckCollider : MonoBehaviour {

        public GameObject HitBlock;
        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        private void OnTriggerEnter(Collider other) {
            if (!(other.gameObject.tag == "player")) {
                HitBlock = other.gameObject;
            }
        }
    }
}