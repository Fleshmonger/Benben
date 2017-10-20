using UnityEngine;
using Geometry;
using Datastructures;
using Gameplay;
using Gameplay.Map;

/// <summary> Input controller for the in-game hud. </summary>
public sealed class GameplayInput : Singleton<GameplayInput>
{
    #region Constants

    private readonly Plane contactPlane = new Plane(Vector3.back, Vector3.zero);

    #endregion

    #region Fields

    private bool marked;
    private Vector2I markPos;

    #endregion

    #region Methods

    /// <summary> Places a block at the center of the screen. </summary>
    public void PlaceBlock()
    {
        var ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f));
        float enter;
        if (contactPlane.Raycast(ray, out enter))
        {
            var stageManager = StageManager.Instance;
            var gameMaster = GameMaster.Instance;
            var mapManager = Map.Instance;
            var gridPos = stageManager.WorldToGrid(ray.GetPoint(enter));
            var valid = MapUtil.IsValid(gridPos, gameMaster.blockSize, TeamManager.Instance.activeTeam, mapManager);
            if (marked && valid && markPos == gridPos)
            {
                gameMaster.PlayerMove(gridPos);
                stageManager.ClearMark();
                marked = false;
            }
            else
            {
                stageManager.SetMarkPos(gridPos.x, gridPos.y, mapManager.GetTile(gridPos).Height, valid);
                marked = true;
                markPos = gridPos;
            }
        }
    }

    #endregion
}