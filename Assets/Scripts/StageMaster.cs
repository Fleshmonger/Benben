using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageMaster : MonoBehaviour
{
    private List<Object> props = new List<Object>();

    public GameObject fakeBlock;
    public Material validFakeMaterial, invalidFakeMaterial;
    public Block blockPrefab;

    private Vector3 GridToWorld(int x, int y, int z)
    {
        float worldX = x * blockPrefab.size / 2,
              worldY = y * blockPrefab.height,
              worldZ = z * blockPrefab.size / 2;
        return new Vector3(worldX, worldY, worldZ);
    }

    public Vector3 GridToWorld(Vector3 gridPos)
    {
        return GridToWorld((int)gridPos.x, (int)gridPos.y, (int)gridPos.z);
    }

    public Vector3 WorldToGrid(float x, float y, float z)
    {
        float gridX = x / (blockPrefab.size / 2),
              gridY = y / blockPrefab.height,
              gridZ = z / (blockPrefab.size / 2);
        return new Vector3(gridX, gridY, gridZ);
    }

    private GameObject AddProp(GameObject propPrefab, Vector3 worldPos)
    {
        GameObject prop = Instantiate(propPrefab, worldPos, Quaternion.identity) as GameObject;
        props.Add(prop);
        return prop;
    }

    public void SetFakeBlock(int gridX, int gridY, int gridZ, bool isValid)
    {
        fakeBlock.transform.position = GridToWorld(gridX, gridY, gridZ);
        if (isValid)
        {
            fakeBlock.GetComponent<MeshRenderer>().material = validFakeMaterial;
        }
        else
        {
            fakeBlock.GetComponent<MeshRenderer>().material = invalidFakeMaterial;
        }
    }

    public void ClearFakeBlock()
    {
        fakeBlock.transform.position = new Vector3(0, -10, 0);
    }

    public GameObject AddBlock(Vector3 gridPos, Team team)
    {
        GameObject block = AddProp(blockPrefab.gameObject, GridToWorld(gridPos));
        block.GetComponentInChildren<MeshRenderer>().material = team.material;
        return block;
    }
}