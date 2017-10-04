using UnityEngine;
using Gameplay;
using Gameplay.Map;
using Geometry;

namespace Interface
{
    public sealed class InputController : Singleton<InputController>
    {
        private readonly Plane contactPlane = new Plane(Vector3.back, Vector3.zero);

        public float viewSpeed = 5;
        public CameraManager cameraManager;

        private bool marked;
        private bool moving;
        private Vector2I markPos;
        private Vector3 movePos;
        private Vector3 cameraMovePos;

        private bool InputAction()
        {
            return Input.GetMouseButtonDown(0);
        }

        private bool InputMove()
        {
            return Input.GetMouseButton(0) && Input.GetMouseButton(1);
        }

        private void UpdateAction()
        {
            if (!InputAction() || InputMove())
            {
                return;
            }
            var gameMaster = GameMaster.Instance;
            if (!gameMaster.isGameOver)
            {
                PlaceBlock();
            }
            else
            {
                gameMaster.Restart();
            }
            return;
        }

        private Vector3 GetContactPoint(Vector3 pixelCoords)
        {
            var ray = Camera.main.ScreenPointToRay(pixelCoords);
            float enter;
            if (contactPlane.Raycast(ray, out enter))
            {
                return ray.GetPoint(enter);
            }
            return Vector3.zero;
        }

        private void UpdateMove()
        {
            if (!InputMove())
            {
                moving = false;
                return;
            }
            var inputPos = Input.mousePosition;
            if (moving)
            {
                var delta = inputPos - movePos;
                var horiz = Camera.main.aspect * Camera.main.orthographicSize / (Camera.main.pixelWidth / 2f);
                var verti = Camera.main.orthographicSize / (Camera.main.pixelHeight / 2f);
                cameraManager.transform.localPosition -= new Vector3(-delta.x * horiz, (delta.y * Mathf.Sqrt(2)) * verti);
            }
            movePos = inputPos;
            moving = true;
            return;
        }

        private void PlaceBlock()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var stageManager = StageManager.Instance;
                var gameMaster = GameMaster.Instance;
                var mapManager = Map.Instance;
                var gridPos = stageManager.WorldToGrid(hit.point);
                var valid = MapUtil.IsValid(gridPos, gameMaster.blockSize, TeamManager.Instance.activeTeam, mapManager);
                if (marked && valid && markPos == gridPos)
                {
                    gameMaster.PlayerMove(gridPos);
                    stageManager.ClearMark();
                    marked = false;
                }
                else
                {
                    stageManager.SetMarkPos(gridPos.x, gridPos.y, mapManager.GetDepth(gridPos), valid);
                    marked = true;
                    markPos = gridPos;
                }
            }
        }

        // Unity update callback.
        private void Update()
        {
            UpdateAction();
            UpdateMove();
        }
    }
}