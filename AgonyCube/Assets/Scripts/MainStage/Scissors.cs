using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AgonyCube.MainStage {
    public class Scissors : MonoBehaviour {
        public GameDirector director;
        public GameObject scissorsIcon;
        public GameObject invisibkeScissorsIcon;
        public GameObject scissors2D;
       
        private void OnTriggerEnter(Collider other) {
            if(other.tag == "Player") {
                scissors2D.transform.LookAt(director.mainCamera.transform);
                var rotate = scissors2D.transform.rotation;
                rotate.x = 0;
                rotate.z = 0;
                scissors2D.transform.rotation = rotate;
                scissors2D.SetActive(true);
                StartCoroutine(Scissors2DInvisible());
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

       private IEnumerator Scissors2DInvisible() {
            yield return new WaitForSeconds(1.3f);
            scissors2D.SetActive(false);
        }
    }
}