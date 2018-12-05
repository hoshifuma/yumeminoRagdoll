using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AgonyCube.MainStage {
    public class Scissor : MonoBehaviour {
        public GameDirector director;
        private void OnTriggerEnter(Collider other) {
            if(other.tag == "Player") {
                director.scissor = true;
                transform.gameObject.SetActive(false);
            }
        }
    }
}