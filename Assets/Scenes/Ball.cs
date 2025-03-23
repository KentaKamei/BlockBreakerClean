using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // ← TextMeshPro 用

public class Ball : MonoBehaviour
{
    public float speedIncreaseFactor = 2f; // 加速倍率
    public float loseY = -5f; // ゲームオーバーライン
    public float minSpeed = 2.0f;//最小速度のしきい値
    public TextMeshProUGUI countdownText; // 
    private Rigidbody2D rb;
    public float countdownTime = 3f;
    public float startSpeed = 5f; // 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero; // 最初は動かさない
        
        StartCoroutine(CountdownAndStart());
    }

    void Update()
    {
        if (transform.position.y < loseY)
        {
            GameManager.instance.GameOver();
            Destroy(gameObject);
        }
    }

    IEnumerator CountdownAndStart()
    {
        float count = countdownTime;

        while (count > 0)
        {
            countdownText.text = Mathf.Ceil(count).ToString();
            yield return new WaitForSeconds(1f);
            count--;
        }

        countdownText.text = ""; // カウント終わったら非表示

        // ボール発射
        float angle = Random.Range(30f, 150f);
        float radians = angle * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        rb.velocity = direction.normalized * startSpeed;
    }

    void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;

        // X方向が小さすぎるなら補正
        if (Mathf.Abs(velocity.x) < minSpeed)
        {
            velocity.x = Mathf.Sign(velocity.x) * minSpeed;
        }

        // Y方向が小さすぎるなら補正
        if (Mathf.Abs(velocity.y) < minSpeed)
        {
            velocity.y = Mathf.Sign(velocity.y) * minSpeed;
        }

        // 向きを変えずにスピードを保つ
        rb.velocity = velocity.normalized * rb.velocity.magnitude;
    }

    public void IncreaseSpeed()
    {
        rb.velocity = rb.velocity.normalized * (rb.velocity.magnitude * speedIncreaseFactor);
    }

    public void Stop()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero; // スピードを0にする
        rb.isKinematic = true;      // 必要に応じて物理を止める（オプション）
    }

}
