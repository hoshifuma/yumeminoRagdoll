using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class T2onS : MonoBehaviour
{

    //　スタートボタンを押したら実行する
    public void SELECTb() {
        SceneManager.LoadScene("S");
    }
}