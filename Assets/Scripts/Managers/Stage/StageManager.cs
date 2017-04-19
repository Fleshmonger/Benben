using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Gameplay
{
    //TODO This should be compiled with MapManager into a super-map class.
    public sealed class StageManager : Singleton<StageManager>
    {
        public Mark mark;
        public Block blockPrefab;

        private List<Object> props = new List<Object>();

        public Vector3 GridToWorld(int x, int y, int z)
        {
            float worldX = x * blockPrefab.size / 2,
                  worldY = y * blockPrefab.size / 2,
                  worldZ = z * blockPrefab.depth;
            return new Vector3(worldX, worldY, worldZ);
        }

        public Vector3 GridToWorld(Point3 gridPos)
        {
            return GridToWorld(gridPos.x, gridPos.y, gridPos.z);
        }

        public Point2 WorldToGrid(float x, float y)
        {
            var gridX = Mathf.RoundToInt(x / (blockPrefab.size / 2));
            var gridY = Mathf.RoundToInt(y / (blockPrefab.size / 2));
            return new Point2(gridX, gridY);
        }

        public Point2 WorldToGrid(Vector2 worldPos)
        {
            return WorldToGrid(worldPos.x, worldPos.y);
        }

        public Point3 WorldToGrid(float x, float y, float z)
        {
            var point2 = WorldToGrid(x, y);
            var gridZ = Mathf.RoundToInt(z / blockPrefab.depth);
            return new Point3(point2.x, point2.y, gridZ);
        }

        public Point3 WorldToGrid(Vector3 worldPos)
        {
            return WorldToGrid(worldPos.x, worldPos.y, worldPos.z);
        }

        public void SetMarkPos(int gridX, int gridY, int gridZ, bool isValid)
        {
            var meshRenderer = mark.GetComponent<MeshRenderer>();
            mark.gameObject.SetActive(true);
            mark.transform.position = GridToWorld(gridX, gridY, gridZ);
            mark.SetValid(isValid);
        }

        public void ClearMark()
        {
            mark.gameObject.SetActive(false);
        }

        public GameObject AddBlock(int gridX, int gridY, int gridZ, Team team)
        {
            GameObject block = AddProp(blockPrefab.gameObject, GridToWorld(gridX, gridY, gridZ));
            block.GetComponentInChildren<MeshRenderer>().material = team.material;
            return block;
        }

        private GameObject AddProp(GameObject propPrefab, Vector3 worldPos)
        {
            GameObject prop = Instantiate(propPrefab, worldPos, Quaternion.identity) as GameObject;
            props.Add(prop);
            return prop;
        }
    }
}