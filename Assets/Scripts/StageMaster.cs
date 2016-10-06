using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageMaster : MonoBehaviour
{
    private List<Object> props = new List<Object>();

    public GameObject blockPrefab;

    private Vector3 GridToWorld(Vector3 gridPos)
    {
        Vector3 scale = blockPrefab.transform.localScale;
        float worldX = gridPos.x * scale.x / 2,
              worldY = gridPos.y * scale.y,
              worldZ = gridPos.z * scale.z / 2;
        return new Vector3(worldX, worldY, worldZ);
    }

    private GameObject AddProp(GameObject propPrefab, Vector3 worldPos)
    {
        GameObject prop = Instantiate(propPrefab, worldPos, Quaternion.identity) as GameObject;
        props.Add(prop);
        return prop;
    }

    public GameObject AddBlock(Vector3 gridPos, Team team)
    {
        GameObject block = AddProp(blockPrefab, GridToWorld(gridPos));
        block.GetComponent<MeshRenderer>().material = team.material;
        return block;
    }
}