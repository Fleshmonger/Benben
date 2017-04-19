using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public GameObject eye;
    public GameObject board;

    public void Move(float x, float y)
    {
        eye.transform.Translate(x, y, 0);
        board.transform.Translate(x, y, 0);
    }
}