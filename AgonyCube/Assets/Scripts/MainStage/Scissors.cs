using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AgonyCube.MainStage {
    public class Scissors : MonoBehaviour {
        public GameDirector director;
        public GameObject scissorsIcon;
        public GameObject invisibkeScissorsIcon;

        private void OnTriggerEnter(Collider other) {
            if(other.tag == "Player") {
                director.scissors = true;
                transform.gameObject.SetActive(false);
                scissorsIcon.SetActive(true);
                invisibkeScissorsIcon.SetActive(false);
            }
        }
    }
}