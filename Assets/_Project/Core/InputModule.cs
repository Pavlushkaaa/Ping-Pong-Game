using Core.UI;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace Core
{
    public class InputModule : MonoBehaviour
    {
        public static Vector2 WorlsScreenSize { get => Camera.main.ScreenToWorldPoint(new(Camera.main.pixelWidth, Camera.main.pixelHeight)); }

        public bool IsTouchDown { get; private set; }
        public bool IsTouchMove { get; private set; }
        public bool IsTouchUp { get; private set; }

        [field: SerializeField] public Vector2 TouchDirection { get; private set; }
        [field: SerializeField] public float PointerXAxisPosition { get; private set; }

        [SerializeField] private float _sensivity;
        [SerializeField] private LayerMask _ignoreLayers;

        private float _defoultPointerXAxisPosition;
        private Vector2 _touchDownPoint;
        private Camera _camera;

        public void Reset() => PointerXAxisPosition = _defoultPointerXAxisPosition;

        private void OnEnable()
        {
            EnhancedTouchSupport.Enable();
        }
        private void OnDisable()
        {
            EnhancedTouchSupport.Disable();
        }
        private void Awake()
        {
            _defoultPointerXAxisPosition = Camera.main.WorldToScreenPoint(Vector2.zero).x;
            PointerXAxisPosition = _defoultPointerXAxisPosition;
        }
        private void Start()
        {
            Touch.onFingerDown += UpdateTouchDown;
            Touch.onFingerMove += UpdateTouchMove;
            Touch.onFingerUp += UpdateTouchUp;

            _camera = Camera.main;
        }
        private void Update()
        {
            if (Touch.activeFingers.Count == 1)
            {
                IsTouchDown = Touch.activeTouches[0].phase == TouchPhase.Began;
                IsTouchMove = Touch.activeTouches[0].phase == TouchPhase.Moved || Touch.activeTouches[0].phase == TouchPhase.Stationary;
                IsTouchUp = Touch.activeTouches[0].phase == TouchPhase.Ended;
            }

            #if UNITY_EDITOR
            if (Touch.activeFingers.Count > 0) return;

            IsTouchDown = Mouse.current.leftButton.wasPressedThisFrame;
            IsTouchMove = Mouse.current.leftButton.isPressed;
            IsTouchUp = Mouse.current.leftButton.wasReleasedThisFrame;

            if (Mouse.current.leftButton.isPressed)
            {
                var t = Mouse.current.delta.ReadValue().x * _sensivity;
                PointerXAxisPosition = Mathf.Clamp(PointerXAxisPosition + t, 0, Camera.main.pixelWidth);

                TouchDirection = _touchDownPoint - Mouse.current.position.ReadValue();
            }
            else if (Mouse.current.leftButton.wasReleasedThisFrame)
                TouchDirection = _touchDownPoint - Mouse.current.position.ReadValue();
            #endif
        }

        private bool CheckTouch(Vector2 touchPosition)
        {
            Ray ray = _camera.ScreenPointToRay(touchPosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, _ignoreLayers);

            if (hit.collider != null && hit.collider.TryGetComponent<IClickHandler>(out IClickHandler handler))
            {
                handler.OnClicked();
                return true;
            }

            return false;
        }
        private void UpdateTouchMove(Finger f)
        {
            TouchDirection = _touchDownPoint - f.screenPosition;

            if (Touch.activeTouches.Count == 1)
                    PointerXAxisPosition = Mathf.Clamp(PointerXAxisPosition + Touch.activeTouches[0].delta.x * _sensivity, 0, Camera.main.pixelWidth);
        }
        private void UpdateTouchDown(Finger f)
        {
            _touchDownPoint = f.screenPosition;

            CheckTouch(f.screenPosition);
            PointerXAxisPosition = f.screenPosition.x;
        }
        private void UpdateTouchUp(Finger f)
        {
            TouchDirection = _touchDownPoint - f.screenPosition;
        }

        #if UNITY_EDITOR
        private void OnClickDown()
        {
            if (Touch.activeFingers.Count > 0) return;

            if (!CheckTouch(Mouse.current.position.ReadValue()))
                PointerXAxisPosition = Mouse.current.position.ReadValue().x;

            _touchDownPoint = Mouse.current.position.ReadValue();
        }
        #endif
    }
}
