using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoTittle : MonoBehaviour
{

    //　スタートボタンを押したら実行する
    public void SceneLoad()
    {
        SceneManager.LoadScene("Tittle_mitani");
    }
}