using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AgonyCube.MainStage {
    public class ZoomCamera : MonoBehaviour {
        [SerializeField]
        float startFov;
        [SerializeField]
        float endFov;

        Transform goal;

        private Camera mainCamera;

        private void Awake() {
            goal = GameObject.FindGameObjectWithTag("Finish").transform;
        }
        //テスト用
        private void Start() {
            //StartGoalMotion();
        }

        /// <summary>
        /// ゴール時のカメラモーションを開始します。
        /// </summary>
        public void StartGoalMotion() {
            transform.localEulerAngles = new Vector3(30, 0, 0);
            StartCoroutine(OnGoal());
        }

        IEnumerator OnGoal() {

            float startTime = 0;

            Camera.main.transform.LookAt(goal);

            while (startTime <= 1.0f) {
                startTime += Time.deltaTime;
                Camera.main.fieldOfView = Mathf.Lerp(startFov, endFov, startTime);

                yield return null;
            }

        }

    }
}