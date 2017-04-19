using UnityEngine;

public sealed class Block : MonoBehaviour
{
    public float spawnAnimTime = 0.15f;
    public float size = 4f;
    public float depth = 1f;

    private float spawnTime = 0f;
    private Vector3 baseScale;

    private void Awake()
    {
        spawnTime = Time.time;
        baseScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        var scale = 1f;
        if (Time.time < spawnTime + spawnAnimTime)
        {
            var t = (Time.time - spawnTime) / spawnAnimTime;
            scale = t * t;
        }
        transform.localScale = scale * baseScale;
    }
}
