using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgonyCubeMainStage {
    public class PlayerCollider : MonoBehaviour {
        public GameObject MoveBlock;
        public GameObject GameDirector;
        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.tag == "Step") {
                MoveBlock = other.gameObject;
                GameDirector.GetComponent<GameDirector>().SetMove(MoveBlock);
            }
            else if (other.gameObject.tag == "Block") {
                MoveBlock = other.gameObject;
                GameDirector.GetComponent<GameDirector>().SetMove(MoveBlock);
            }
            else if (other.gameObject.tag == "Clear") {
                MoveBlock = other.gameObject;
                GameDirector.GetComponent<GameDirector>().SetMove(MoveBlock);
            }
        }
        private void OnTriggerExit(Collider other) {
            if (!(other.gameObject.tag == "Collider")) {
                MoveBlock = null;

            }
        }
    }
}
