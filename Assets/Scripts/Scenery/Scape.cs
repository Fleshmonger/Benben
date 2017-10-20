using UnityEngine;
using Geometry;
using Gameplay.Map;

/// <summary> Creates a terrain mesh from a map. </summary>
[RequireComponent(typeof(MeshFilter))]
public sealed class Scape : MonoBehaviour
{
    #region Methods

    /// <summary> Recreates the mesh to match the new map. </summary>
    public void ConstructMesh(Map map)
    {
        var meshFilter = GetComponent<MeshFilter>();
        var mesh = new Mesh();

        var mapDimensions = map.GetDimensions();
        var dimensions = new Size2I(mapDimensions.Width + 1, mapDimensions.Height + 1);

        // Vertices
        var offset = map.GetOrigin();
        var vertices = new Vector3[dimensions.Area];
        for (var x = 0; x < dimensions.Width; x++)
        {
            for (var y = 0; y < dimensions.Height; y++)
            {
                vertices[x + dimensions.Width * y] = new Vector3(x, y, Random.value) + offset;
            }
        }

        // Triangles
        var triangles = new int[6 * mapDimensions.Area];
        var triangleIndex = 0;
        for (var x = 0; x < mapDimensions.Width; x++)
        {
            for (var y = 0; y < mapDimensions.Height; y++)
            {
                var v0 = x + y * dimensions.Width;
                var v1 = v0 + 1;
                var v2 = v0 + dimensions.Width;
                var v3 = v2 + 1;
                triangles[triangleIndex] = v0;
                triangles[triangleIndex + 1] = v1;
                triangles[triangleIndex + 2] = v2;

                triangles[triangleIndex + 3] = v3;
                triangles[triangleIndex + 4] = v2;
                triangles[triangleIndex + 5] = v1;
                triangleIndex += 6;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        meshFilter.mesh = mesh;
    }

    #endregion
}