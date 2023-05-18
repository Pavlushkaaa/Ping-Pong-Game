using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class GameZone : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _zones;

        private int _currentZoneId = 0;
        private GameObject _currentZone;
        private PointSystem _currentPointSystem;

        private EndGame _endGame;

        public void CreateZone()
        {
            _currentZone = Instantiate(_zones[_currentZoneId], Vector2.zero, Quaternion.identity);
            _currentPointSystem = _currentZone.GetComponent<PointSystem>();
            _currentPointSystem.OnPointsEnd += _endGame.EndSuccess;
        }

        public void DestroyZone()
        {
            _currentPointSystem.OnPointsEnd -= _endGame.EndSuccess;
            Destroy(_currentZone);
        }

        private void Start()
        {
            _endGame = GetComponent<EndGame>();
        }
    }
}
