using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField]
    float startFov;
    [SerializeField]
    float endFov;

    Transform goal;

    private Camera mainCamera;

    private void Awake()
    {
        goal = GameObject.FindGameObjectWithTag("Finish").transform;
    }

    private void Start()
    {
        StartGoalMotion();
    }

    /// <summary>
    /// ゴール時のカメラモーションを開始します。
    /// </summary>
    public void StartGoalMotion()
    {
        StartCoroutine(OnGoal());
    }

    IEnumerator OnGoal()
    {
        
        float startTime = 0;

        Camera.main.transform.LookAt(goal);

        while (startTime <= 1.0f)
        {
            startTime += Time.deltaTime;
            Camera.main.fieldOfView = Mathf.Lerp(startFov, endFov, startTime);

            yield return null;
        }

    }
}
