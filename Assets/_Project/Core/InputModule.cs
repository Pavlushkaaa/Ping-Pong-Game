using Core.UI;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
using Random = UnityEngine.Random;

namespace Core
{
    public class InputModule : MonoBehaviour
    {
        public static Vector2 WorlsScreenSize { get => CalculationWorlsScreenSize(); }

        public event Action Touched;

        public bool IsTouchDown { get; private set; }
        public bool IsTouchMove { get; private set; }
        public bool IsTouchUp { get; private set; }

        [field: SerializeField] public Vector2 TouchDirection { get; private set; }
        [field: SerializeField] public float PointerXAxisPosition { get; private set; }

        public float DefoultPointerXAxisPosition { get => _defoultPointerXAxisPosition; }

        [SerializeField] private float _sensivity;
        [SerializeField] private LayerMask _ignoreLayers;
        [SerializeField] private AudioClip _touchClip;

        private float _defoultPointerXAxisPosition;
        private Vector2 _touchDownPoint;
        private Camera _camera;
        private SoundPlayer _soundPlayer;

        public static Vector2 CreateRandomPosition(float offset)
        {
            var border = WorlsScreenSize;
            border.x -= offset;
            border.y -= offset;

            var x = Random.Range(-border.x, border.x);
            var y = Random.Range(-border.y, border.y);
            return new Vector2(x, y);
        }
        public void Reset() => PointerXAxisPosition = _defoultPointerXAxisPosition;

        private void OnEnable() => EnhancedTouchSupport.Enable();
        private void OnDisable() => EnhancedTouchSupport.Disable();

        private void Awake()
        {
            _soundPlayer = GetComponent<SoundPlayer>();

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

                if(IsTouchDown || IsTouchMove) Touched?.Invoke();
            }

            #if UNITY_EDITOR
            if (Touch.activeFingers.Count > 0) return;

            IsTouchDown = Mouse.current.leftButton.wasPressedThisFrame;
            IsTouchMove = Mouse.current.leftButton.isPressed;
            IsTouchUp = Mouse.current.leftButton.wasReleasedThisFrame;

            if (IsTouchDown)
            {
                Touched?.Invoke();
                _soundPlayer.Play(_touchClip);
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
                Touched?.Invoke();

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
            _soundPlayer.Play(_touchClip);

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

        private static Vector2 CalculationWorlsScreenSize()
        {
            var camera = Camera.main;
            var width = camera.pixelWidth;
            var height = camera.pixelHeight;

            if (height / width == 2) 
                camera.orthographicSize = 5.5f;
            else
                camera.orthographicSize = 5.2f;

            return camera.ScreenToWorldPoint(new(width, height));
        }
    }
}
