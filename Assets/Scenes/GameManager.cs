using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // リスタート用
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager instance; // シングルトンでどこからでもアクセスできるように
    private bool isGameOver = false;
    private bool isPaused = false;
    public GameObject pauseText;
    private Coroutine blinkCoroutine; // ← 点滅用のコルーチン
    private TextMeshProUGUI pauseTextUI; // ← Text本体（.enabledを使うため）

    void Awake()
    {
        // シングルトンの設定
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        // pauseText が TextMeshPro の場合に .enabled 切り替えが使える
        if (pauseText != null)
        {
            pauseTextUI = pauseText.GetComponent<TextMeshProUGUI>();
            pauseText.SetActive(false); // 最初は非表示
        }
    }

    void Update()
    {
        // ブロックがなくなったらクリア判定
        if (!isGameOver && GameObject.FindObjectsOfType<BlockController>().Length == 0)
        {
            GameClear();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f; 
        if (pauseText != null)
        {
            if (isPaused)
            {
                pauseText.SetActive(true);
                if (pauseTextUI != null)
                {
                    blinkCoroutine = StartCoroutine(BlinkPauseText());
                }
            }
            else
            {
                if (blinkCoroutine != null) StopCoroutine(blinkCoroutine);
                if (pauseTextUI != null) pauseTextUI.enabled = true; // 強制ON
                pauseText.SetActive(false); // 非表示に戻す
            }
        }
    }

    IEnumerator BlinkPauseText()
    {
        while (true)
        {
            pauseTextUI.enabled = !pauseTextUI.enabled;
            yield return new WaitForSecondsRealtime(0.5f); // ← ポーズ中でも動く！
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

    public bool IsPaused()
    {
        return isPaused;
    }
}

