using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoSelect : MonoBehaviour
{

    //　スタートボタンを押したら実行する
    public void SceneLoad()
    {
        SceneManager.LoadScene("Select_mitani");
    }
}