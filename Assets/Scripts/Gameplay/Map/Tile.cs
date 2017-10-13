using UnityEngine;

namespace Gameplay.Map
{
    /// <summary> Representation of a tile object. </summary>
    public struct Tile
    {
        #region Fields

        private int _height;

        #endregion

        #region Properties

        /// <summary> The layer which the tile is in. </summary>
        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = Mathf.Max(value, 0);
            }
        }

        /// <summary> The owner of the tile. </summary>
        public Team Team { get; set; }

        /// <summary> The world object representation of the tile. </summary>
        public GameObject Prop { get; set; }

        #endregion

        /// <summary> Creates a new tile with a given height, team and prop. </summary>
        public Tile(int height, Team team, GameObject prop)
        {
            _height = Mathf.Max(height, 0);
            Team = team;
            Prop = prop;
        }
    }
}