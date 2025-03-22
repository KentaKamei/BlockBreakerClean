using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
public float speed = 10f;
public float minX = -5.22f;  // 左端
public float maxX = 5.165f;   // 右端

void Update()
{
    float move = Input.GetAxis("Horizontal");
    Vector3 newPos = transform.position + Vector3.right * move * speed * Time.deltaTime;

    // X位置を制限
    newPos.x = Mathf.Clamp(newPos.x, minX, maxX);

    transform.position = newPos;
}}
