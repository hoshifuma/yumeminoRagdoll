
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgonyCube.MainStage {
    public class TestAni : MonoBehaviour {

        public GameObject Scissers;
        Animator animator;
        PlayerController playerController;
        private const string key_walk = "Walking";

        // Use this for initialization
        void Start() {
            playerController = this.GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update() {
            if (playerController.playerState == PlayerController.PlayerState.Locmotion) {
                this.animator.SetBool(key_walk, true);
            }
            else if (playerController.playerState == PlayerController.PlayerState.Idle) {
                this.animator.SetBool(key_walk, false);
            }
        }
    }
}