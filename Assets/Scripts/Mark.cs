using UnityEngine;
using System.Collections;

public class Mark : MonoBehaviour
{
    public MeshRenderer renderer;
    public Material validMarkMaterial;
    public Material invalidMarkMaterial;

    public void SetValid(bool valid)
    {
        if (valid)
        {
            renderer.material = validMarkMaterial;
        }
        else
        {
            renderer.material = invalidMarkMaterial;
        }
    }
}
