using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    public void RetryGame()
    {
        SceneManager.LoadScene("GameScene"); // 本編のシーン名に合わせてね
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}