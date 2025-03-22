using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public int hp; // Inspectorで確認したいならpublic
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        hp = GetRandomHP();  // ランダムで1〜3のHPを設定
        UpdateColor();       // HPに応じて色を変える例（任意）
    }

    int GetRandomHP()
    {
        // 出現確率を調整（割合の合計は100）
        int r = Random.Range(0, 100); // 0〜99の乱数

        if (r < 60) return 1;         // 0〜49 → 50%
        else if (r < 90) return 2;    // 50〜79 → 30%
        else return 3;                // 80〜99 → 20%
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            hp--;

            if (hp <= 0)
            {
                // ボールを加速させる
                Ball ball = collision.gameObject.GetComponent<Ball>();
                if (ball != null)
                {
                    ball.IncreaseSpeed();
                    Debug.Log("加速");
                }
                Destroy(gameObject);
            }
            else
            {
                UpdateColor(); // HPが残ってたら色を変える（任意）
            }
        }
    }

    void UpdateColor()
    {
        if (sr == null) return;

        // HPに応じて色を変える（自由にカスタマイズOK）
        switch (hp)
        {
            case 3: sr.color = Color.red; break;
            case 2: sr.color = Color.yellow; break;
            case 1: sr.color = Color.white; break;
        }
    }
}


