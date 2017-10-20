using UnityEngine;
using Datastructures;

namespace Interface
{
    public sealed class ViewInput : Singleton<ViewInput>
    {
        #region Fields

        public float viewSpeed = 5;
        public CameraManager cameraManager;
        
        private bool moving;
        private Vector3 movePos;
        private Vector3 cameraMovePos;

        #endregion

        #region Methods

        // Unity update callback.
        private void Update()
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

        private bool InputMove()
        {
            return Input.GetMouseButton(0);
        }

        #endregion
    }
}