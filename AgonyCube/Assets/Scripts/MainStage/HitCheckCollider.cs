using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgonyCubeMainStage {
    public class HitCheckCollider : MonoBehaviour {

        public GameObject HitBlock;
        public bool PlayerHit = false;
        public bool MoveCheck;
        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.tag == "Player") {
                PlayerHit = true;
            }
            else {
                HitBlock = other.gameObject;
                if (HitBlock.gameObject.tag == "Block") {
                    MoveCheck = HitBlock.GetComponent<CubeController>().MoveCheck();
                }
                else if (HitBlock.gameObject.tag == "Clear") {
                    MoveCheck = HitBlock.GetComponent<ClearController>().ClearMoveCheck();

                }

                if(MoveCheck == false) {
                    HitBlock = null;
                }
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.gameObject.tag == "Player") {
                PlayerHit = false;
            }
        }
    }
}