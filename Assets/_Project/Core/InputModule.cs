using Core.UI;
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

        public float DefoultPointerXAxisPosition { get => _defoultPointerXAxisPosition; }

        [SerializeField] private float _sensivity;
        [SerializeField] private LayerMask _ignoreLayers;

        private float _defoultPointerXAxisPosition;
        private Vector2 _touchDownPoint;
        private Camera _camera;

        public void Reset() => PointerXAxisPosition = _defoultPointerXAxisPosition;

        private void OnEnable() => EnhancedTouchSupport.Enable();
        private void OnDisable() => EnhancedTouchSupport.Disable();

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
                IsTouchDown = Touch.activeTouches[0].phase == TouchPhase.Began && !CheckClick(Touch.activeTouches[0].screenPosition);
                IsTouchMove = Touch.activeTouches[0].phase == TouchPhase.Moved || Touch.activeTouches[0].phase == TouchPhase.Stationary;
                IsTouchUp = Touch.activeTouches[0].phase == TouchPhase.Ended;
            }

            #if UNITY_EDITOR
            if (Touch.activeFingers.Count > 0) return;

            IsTouchDown = Mouse.current.leftButton.wasPressedThisFrame;
            IsTouchMove = Mouse.current.leftButton.isPressed;
            IsTouchUp = Mouse.current.leftButton.wasReleasedThisFrame;

            if (IsTouchDown)
            {
                if (TryClick(Mouse.current.position.ReadValue(), out var t))
                {
                    IsTouchDown = false;
                    t.OnClicked();
                    return;
                }

                PointerXAxisPosition = Mouse.current.position.ReadValue().x;
                _touchDownPoint = Mouse.current.position.ReadValue();
            }
            else if (IsTouchUp && !CheckClick(Mouse.current.position.ReadValue()))
            {
                TouchDirection = _touchDownPoint - Mouse.current.position.ReadValue();
                return;
            }

            if (IsTouchMove && !CheckClick(Mouse.current.position.ReadValue()))
            {
                var t = Mouse.current.delta.ReadValue().x * _sensivity;
                PointerXAxisPosition = Mathf.Clamp(PointerXAxisPosition + t, 0, Camera.main.pixelWidth);

                TouchDirection = _touchDownPoint - Mouse.current.position.ReadValue();
            }
            #endif
        }

        private bool TryClick(Vector2 touchPosition, out IClickHandler clickHandler)
        {
            clickHandler = null;
            Ray ray = _camera.ScreenPointToRay(touchPosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, _ignoreLayers);

            return hit.collider != null && hit.collider.TryGetComponent<IClickHandler>(out clickHandler);
        }
        private bool CheckClick(Vector2 touchPosition) => TryClick(touchPosition, out var temp);
        
        private void UpdateTouchMove(Finger f)
        {
            if (CheckClick(f.screenPosition) || Touch.activeTouches.Count != 1) return;

            TouchDirection = _touchDownPoint - f.screenPosition;
            PointerXAxisPosition = Mathf.Clamp(PointerXAxisPosition + Touch.activeTouches[0].delta.x * _sensivity, 0, Camera.main.pixelWidth);
        }
        private void UpdateTouchDown(Finger f)
        {
            if (TryClick(f.screenPosition, out var button))
            {
                button.OnClicked();
                return;
            }

            _touchDownPoint = f.screenPosition;
            PointerXAxisPosition = f.screenPosition.x;
        }
        private void UpdateTouchUp(Finger f)
        {
            if (CheckClick(f.screenPosition)) return;

            TouchDirection = _touchDownPoint - f.screenPosition;
        }
    }
}
