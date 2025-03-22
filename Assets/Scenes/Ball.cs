using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speedIncreaseFactor = 2f; // 加速倍率
    public float loseY = -5f; // ゲームオーバーライン
    public float minSpeed = 2.0f;//最小速度のしきい値

    private Rigidbody2D rb;

    void Start()
    {
        float angle = Random.Range(30f, 150f); // ランダム角度
        float radians = angle * Mathf.Deg2Rad;

        rb = GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        rb.velocity = direction.normalized * 5f; // 初速
    }

    void Update()
    {
        if (transform.position.y < loseY)
        {
            GameManager.instance.GameOver();
            Destroy(gameObject);
        }
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
