using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour
{
    private float spawnTime = 0f;
    private Vector3 baseScale;

    public float spawnAnimTime = 0.15f, size = 4f, height = 1f;

    private void Awake()
    {
        spawnTime = Time.time;
        baseScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        float scale;
        if (Time.time < spawnTime + spawnAnimTime)
        {
            float fraction = (Time.time - spawnTime) / spawnAnimTime;
            scale = fraction * fraction;
        }
        else
        {
            scale = 1f;
        }
        transform.localScale = scale * baseScale;
    }
}
