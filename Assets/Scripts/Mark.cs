using UnityEngine;
using System.Collections;

public class Mark : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Material validMarkMaterial;
    public Material invalidMarkMaterial;

    public void SetValid(bool valid)
    {
        if (valid)
        {
            meshRenderer.material = validMarkMaterial;
        }
        else
        {
            meshRenderer.material = invalidMarkMaterial;
        }
    }
}
