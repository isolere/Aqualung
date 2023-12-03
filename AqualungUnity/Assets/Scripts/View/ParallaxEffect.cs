using UnityEngine;

namespace Platformer.View
{
    /// <summary>
    /// Used to move a transform relative to the main camera position with a scale factor applied.
    /// This is used to implement parallax scrolling effects on different branches of gameobjects.
    /// </summary>
    public class ParallaxEffect : MonoBehaviour
    {
        private Vector3 _previousCamPosition;
        private Transform _cameraTransform;

        [SerializeField] private float _parallaxMultiplier;

        void Awake()
        {
            _cameraTransform = Camera.main.transform;
            _previousCamPosition= _cameraTransform.position;
        }

        void LateUpdate()
        {
            float deltaX = (_cameraTransform.position.x - _previousCamPosition.x) * _parallaxMultiplier;
            float deltaY = (_cameraTransform.position.y - _previousCamPosition.y) * _parallaxMultiplier;
            transform.Translate(new Vector3(deltaX, deltaY, 0));
            _previousCamPosition = _cameraTransform.position;
        }

    }
}