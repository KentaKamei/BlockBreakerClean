using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // リスタート用

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // シングルトンでどこからでもアクセスできるように

    private bool isGameOver = false;

    void Awake()
    {
        // シングルトンの設定
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        // ブロックがなくなったらクリア判定
        if (!isGameOver && GameObject.FindObjectsOfType<BlockController>().Length == 0)
        {
            GameClear();
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        // ここにゲームオーバー演出やUI表示など追加できる
        SceneManager.LoadScene("GameOver"); 
    }

    public void GameClear()
    {
        isGameOver = true;
        // ボールを止める
        Ball ball = FindObjectOfType<Ball>();
        if (ball != null)
        {
            ball.Stop();
        }
        // ここにゲームクリア演出やUI表示など追加できる
        SceneManager.LoadScene("GameClear");
    }
}

