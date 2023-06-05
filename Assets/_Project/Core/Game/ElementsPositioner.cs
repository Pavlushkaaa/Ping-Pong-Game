using UnityEngine;

namespace Core
{
    public class ElementsPositioner : MonoBehaviour
    {
        [SerializeField] private Transform _platform;
        [SerializeField] private Transform _ballDeadZone;

        [Space]
        [SerializeField] private Transform _menuSoundSwitchButton;
        [SerializeField] private Transform _pauseSoundSwitchButton;
        [SerializeField] private Transform _pauseButton;
        [SerializeField] private Transform _retryButton;
        [SerializeField] private Transform _backToMainMenuButton;

        [Space]
        [SerializeField] private Transform _borderUp;
        [SerializeField] private Transform _borderDown;
        [SerializeField] private Transform _borderRight;
        [SerializeField] private Transform _borderLeft;

        [Space]
        [SerializeField] private float _platformHeight = 1.5f;
        [SerializeField] private float _ballDeadZoneHeight = 0.7f;
        [SerializeField] private float _buttonOffset = 0.5f;

        private void Start()
        {
            PositionMenuElements();
            PositionGameElements();
            PositionBorders();
        }
        private void PositionMenuElements()
        {
            Vector2 buttonPosition = InputModule.WorlsScreenSize - _buttonOffset * Vector2.one;
            
            _pauseButton.position = buttonPosition;
            _menuSoundSwitchButton.position = buttonPosition;
            _retryButton.position = buttonPosition;
            _backToMainMenuButton.position = buttonPosition;

            buttonPosition.x *= -1;
            _pauseSoundSwitchButton.position = buttonPosition;
        }
        private void PositionGameElements()
        {
            Vector2 screen = InputModule.WorlsScreenSize;

            _platform.position = new(0, -screen.y + _platformHeight);
            _ballDeadZone.position = new(0, -screen.y - _ballDeadZoneHeight);
        }
        private void PositionBorders()
        {
            Vector2 screen = InputModule.WorlsScreenSize;
            float offset = _borderUp.localScale.y / 2;

            _borderUp.position = new(0, screen.y + offset);
            _borderDown.position = new(0, -screen.y - offset);

            _borderLeft.position = new(screen.x + offset, 0);
            _borderRight.position = new(-screen.x - offset, 0);
        }
    }
}
