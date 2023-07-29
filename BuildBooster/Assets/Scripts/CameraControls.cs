using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace FS_AvatarCreator
{
    // All the camera controls like zoom, panning, rotation..
    public class CameraControls : MonoBehaviour
    {
        public enum mouseBtnOpts { Left = 0, Right = 1, Middle = 2 }
        public mouseBtnOpts mouseButtonToOrbit = mouseBtnOpts.Right;
        public mouseBtnOpts mouseButtonToPan = mouseBtnOpts.Right;

        [SerializeField]
        private Camera _mainCamera;
        private Vector2 _lightAngle = new Vector2(0f, -45f);
        public Vector2 CameraAngle;
        public const float MaxPitch = 80f;
        public float InputMultiplier = 1f;
        public Vector3 CameraPivot;
        public float CameraDistance = 1f;
        public float _lastMultiTouchDistance;
        public const float InputMultiplierRatio = 0.1f;
        public const float MaxCameraDistanceRatio = 3f;
        public const float MinCameraDistance = 0.01f;
        [SerializeField]
        private Light _light;

        public Transform target;

        public float yMinLimit = -20f;
        public float yMaxLimit = 80f;

        public float distanceMin = .5f;
        public float distanceMax = 15f;


        Vector3 defaultPos;
        Quaternion defaultRot;
        Vector3 defaultCameraPivot;
        float defaultCameraDistance;
        Vector2 defaultCameraAngle;
        public FixedTouchField TF;

        private void Start()
        {
            _mainCamera = Camera.main;
            defaultCameraAngle = CameraAngle;
            defaultCameraDistance = CameraDistance;
            defaultCameraPivot = CameraPivot;

        }
        //Resets camera position 
        public void ResetCamera()
        {
            CameraAngle = defaultCameraAngle;
            CameraDistance = defaultCameraDistance;
            CameraPivot = defaultCameraPivot;
        }
        public void FixedUpdate()
        {
            if (target)
            {
                ProcessInput();
            }
        }

        protected virtual void ProcessInput()
        {
            if (!_mainCamera.enabled)
            {
                return;
            }
            ProcessInputInternal(_mainCamera.transform);
        }

        /// <param name="cameraTransform">The Camera to process input movements.</param>
        private void ProcessInputInternal(Transform cameraTransform)
        {

            if (Input.GetMouseButton((int)mouseButtonToOrbit))
            {
                if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
                {
                    _lightAngle.x = Mathf.Repeat(_lightAngle.x + TF.touchDist.x, 360f);
                    _lightAngle.y = Mathf.Clamp(_lightAngle.y + TF.touchDist.y, -MaxPitch, MaxPitch);
                }
                else
                {
                    UpdateCamera();
                }
            }
            if (Input.GetMouseButton((int)mouseButtonToPan))
            {
                CameraPivot -= cameraTransform.up * (TF.touchDist.y / 100) * (InputMultiplier) + cameraTransform.right * (TF.touchDist.x / 100) * (InputMultiplier);
            }

            CameraDistance = Mathf.Min(CameraDistance - GetMouseScrollDelta().y * InputMultiplier, InputMultiplier * (1f / InputMultiplierRatio) * MaxCameraDistanceRatio);

            if (CameraDistance < 0f)
            {
                CameraPivot += cameraTransform.forward * -CameraDistance;
                CameraDistance = 0f;
            }

            cameraTransform.position = CameraPivot + Quaternion.AngleAxis(CameraAngle.x, Vector3.up) * Quaternion.AngleAxis(CameraAngle.y, Vector3.right) * new Vector3(0f, 0f, Mathf.Max(MinCameraDistance, CameraDistance));
            cameraTransform.LookAt(CameraPivot);

        }

        public void UpdateCamera()
        {
            CameraAngle.x = Mathf.Repeat(CameraAngle.x + (TF.touchDist.x / 10), 360f);
            CameraAngle.y = Mathf.Clamp(CameraAngle.y + (TF.touchDist.y / 10), -MaxPitch, MaxPitch);
            CameraAngle.y = ClampAngle(CameraAngle.y, yMinLimit, yMaxLimit);
        }
        // gives Mouse scroll value
        public Vector2 GetMouseScrollDelta()
        {
#if ENABLE_INPUT_SYSTEM
            return Mouse.current != null ? Mouse.current.scroll.ReadValue() * 0.01f: default;
#else
            if (SystemInfo.deviceType == DeviceType.Handheld && Input.touchCount == 2)
            {
                var firstTouch = Input.touches[0];
                var secondTouch = Input.touches[1];
                if (firstTouch.phase == TouchPhase.Began || secondTouch.phase == TouchPhase.Began)
                {
                    _lastMultiTouchDistance = Vector2.Distance(firstTouch.position, secondTouch.position);
                }
                if (firstTouch.phase != TouchPhase.Moved || secondTouch.phase != TouchPhase.Moved)
                {
                    return Vector2.zero;
                }
                var newMultiTouchDistance = Vector2.Distance(firstTouch.position, secondTouch.position);
                var deltaMultiTouchDistance = newMultiTouchDistance - _lastMultiTouchDistance;
                _lastMultiTouchDistance = newMultiTouchDistance;
                return new Vector2(0f, deltaMultiTouchDistance * 0.05f);
            }
            return Input.mouseScrollDelta;
#endif
        }
        // Clamp Angle with in given values..
        public float ClampAngle(float angle, float min, float max)
        {

            while (angle < -360F)
                angle += 360F;
            while (angle > 360F)
                angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }
    }
}