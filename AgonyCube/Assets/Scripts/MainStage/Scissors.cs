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
                transform.parent = other.transform;
                other.transform.GetComponent<PlayerController>().scissors = gameObject;
                var mesh = GetComponentsInChildren<MeshRenderer>();
                transform.localPosition = new Vector3(0, 0, 0);
                transform.localRotation = new Quaternion(0, 0, 0, 0);
                foreach(var child in mesh) {
                    child.enabled = false;
                }
                GetComponent<ItemScript>().enabled = false;
                scissorsIcon.SetActive(true);
                invisibkeScissorsIcon.SetActive(false);
            }
        }
    }
}