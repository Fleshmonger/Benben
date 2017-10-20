using UnityEngine;

/// <summary> Wraps a MonoBehavior class as a singleton. </summary>
namespace Datastructures
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        #region Properties

        /// <summary> The instance of the singleton-type. </summary>
        public static T Instance { get; private set; }

        #endregion

        #region Methods

        /// <summary> Initializes the singleton on awake. </summary>
        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }
}