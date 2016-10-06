using UnityEngine;
using System.Collections;

public class CameraMaster : MonoBehaviour
{
    public float minHeight = 10;
    public GameObject eye, canvas;

    public void Move(float x, float z)
    {
        eye.transform.Translate(x, 0f, z);
        canvas.transform.Translate(x, 0f, z);
    }
}