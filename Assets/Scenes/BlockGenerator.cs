using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    public GameObject blockPrefab;
    public int rows = 6;
    public int columns = 5;
    public float spacingX = 0.8f;
    public float spacingY = 1.3f;
    public float PosX = -7.0f;
    public float PosY = 6.0f;

    void Start()
    {
        Vector2 startPos = new Vector2(PosX, PosY); // XとYを好きな値に調整
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                Vector2 pos = startPos + new Vector2(x * spacingX, y * spacingY);
                Instantiate(blockPrefab, pos, Quaternion.identity);
            }
        }
    }
}
