using UnityEngine;

namespace Core.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private MainMenuView _view;

        private GameLoop _gameLoop;

        public void Show() => _view.ShowPanel();
        private void Hide() => _view.HidePanel();

        private void Start()
        {
            _gameLoop= GetComponentInParent<GameLoop>();

            _view.OnStartGame += () => { Hide(); _gameLoop.StartLoop(); };
        }
    }
}
