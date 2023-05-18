using Core.UI;
using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Core
{
    public class InputModule : MonoBehaviour
    {
        public static Vector2 WorlsScreenSize { get => Camera.main.ScreenToWorldPoint(new(Camera.main.pixelWidth, Camera.main.pixelHeight)); }
        [field: SerializeField] public float PointerXAxisPosition { get; private set; }
        [SerializeField] private float _sensivity;
        [SerializeField] private LayerMask _ignoreLayers;

        private float _defoultPointerXAxisPosition;

        public void Reset() => PointerXAxisPosition = _defoultPointerXAxisPosition;
        public static Vector2 CreateWorldScreenSize(float offset)
        {
            Vector2 result = WorlsScreenSize;
            result.x -= offset;
            result.y -= offset;

            return result;
        }

        private void OnEnable()
        {
            EnhancedTouchSupport.Enable();
        }
        private void OnDisable()
        {
            EnhancedTouchSupport.Disable();
        }

        private void UpdateTouchMove(Finger f)
        {
            if (Touch.activeTouches.Count == 1)
                if (!CheckTouch(f.screenPosition))
                    PointerXAxisPosition = Mathf.Clamp(PointerXAxisPosition + Touch.activeTouches[0].delta.x * _sensivity, 0, Camera.main.pixelWidth);
        }
        private void UpdateTouchDown(Finger f)
        {
            UpdateTouchMove(f); 
            PointerXAxisPosition = f.screenPosition.x;
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
        }

        private bool CheckTouch(Vector2 touchPosition)
        {
            Vector2 clickPosition = touchPosition;

            Ray ray = Camera.main.ScreenPointToRay(clickPosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 15, _ignoreLayers);

            if (hit.collider != null && hit.collider.TryGetComponent<IClickHandler>(out IClickHandler handler))
            {
                handler.OnClick();
                return true;
            }

            return false;
        }

        #if UNITY_EDITOR
        private void Update()
        {
            if (Mouse.current.press.isPressed)
            {
                var t = Mouse.current.delta.ReadValue().x * _sensivity;
                PointerXAxisPosition = Mathf.Clamp(PointerXAxisPosition + t, 0, Camera.main.pixelWidth);
            }

        }

        private void OnClick()
        {
            if (Touch.activeFingers.Count > 0) return;

            if(!CheckTouch(Mouse.current.position.ReadValue()))
                PointerXAxisPosition = Mouse.current.position.ReadValue().x;
        }
        #endif
    }
}
