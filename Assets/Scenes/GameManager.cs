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
        Debug.Log("ゲームオーバー！");
        // ここにゲームオーバー演出やUI表示など追加できる
        Invoke("Restart", 2f); // 2秒後に再スタート
    }

    public void GameClear()
    {
        isGameOver = true;
        Debug.Log("ゲームクリア！");
        // ボールを止める
        Ball ball = FindObjectOfType<Ball>();
        if (ball != null)
        {
            ball.Stop();
        }
        // ここにゲームクリア演出やUI表示など追加できる
        Invoke("Restart", 2f);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

