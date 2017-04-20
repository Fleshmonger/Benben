using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public void Move(float x, float y)
    {
        transform.Translate(x, y, 0);
    }

    public void Move(Vector2 delta)
    {
        Move(delta.x, delta.y);
    }
}