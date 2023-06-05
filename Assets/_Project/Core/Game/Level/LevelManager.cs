using NaughtyAttributes;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Core
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameZone _gameZone;

        [Space]
        [SerializeField] private List<LevelSO> _levels = new();

        private List<LevelSave> _savesLevel = new();

        private Dictionary<LevelSO, LevelSave> _levelData = new Dictionary<LevelSO, LevelSave>();

        private int _currentLevelId = 0;
        private int _lastLevelId = 0;
        private int _numberAvailableLevels;
        private string _savePath;

        #if UNITY_EDITOR
        [SerializeField] private bool _isDebug;
        [SerializeField] private LevelSO _debugLevel;
        #endif

        public void Restart()
        {
            _gameZone.DestroyZone();
            StartLastLevel();
        }

        public void StopLevel() => _gameZone.DestroyZone();

        public void StartLastLevel()
        {
            #if UNITY_EDITOR
            if (_isDebug)
            {
                _gameZone.CreateZone(_debugLevel.LevelPrefab);
                return;
            }
            #endif

            _gameZone.CreateZone(_levels[_currentLevelId].LevelPrefab);
        }

        public void SetLevelComplete()
        {
            #if UNITY_EDITOR
            if (_isDebug) return;
            #endif

            _numberAvailableLevels--;
            _levelData.TryGetValue(_levels[_currentLevelId], out var save);
            save.IsComplete = true;
            SaveLevels();
        }

        public void StartNewLevel()
        {
            #if UNITY_EDITOR
            if (_isDebug)
            {
                _gameZone.CreateZone(_debugLevel.LevelPrefab);
                return;
            }
            #endif

            _gameZone.CreateZone(ChooseRandomLevel().LevelPrefab);
        }

        private void Start()
        {
            _savePath = Application.persistentDataPath + @"\data.json";

            #if UNITY_EDITOR
            _savePath = Application.dataPath + @"\data.json";
            #endif

            LoadLevels();
        }

        private LevelSO ChooseRandomLevel()
        {
            if(_numberAvailableLevels <= 1)
                ResetSaveLevels();

            bool result = true;

            do
            {
                _currentLevelId = Random.Range(0, _levels.Count);

                if (_currentLevelId == _lastLevelId) continue;

                _levelData.TryGetValue(_levels[_currentLevelId], out var save);
                result = save.IsComplete;

            } while (result);

            _lastLevelId = _currentLevelId;
            return _levels[_currentLevelId];
        }

        private void SaveLevels()
        {
            JsonSaver<SerializableList<LevelSave>>.Save(_savesLevel.ToSerializable(), _savePath);
        }

        private void LoadLevels()
        {
            if (!JsonSaver<SerializableList<LevelSave>>.Load(_savePath, out var list))
                ResetSaveLevels();
            else
            {
                _savesLevel = list.List;

                for (int i = _savesLevel.Count; i < _levels.Count; i++)
                    _savesLevel.Add(new() { Name = _levels[i].Name, IsComplete = false });

                for (int i = 0; i < _levels.Count; i++)
                    _levelData.Add(_levels[i], _savesLevel.Find(x => x.Name == _levels[i].Name));

                SaveLevels();
            }

            for (int i = 0; i < _savesLevel.Count; i++)
                if (!_savesLevel[i].IsComplete)
                    _numberAvailableLevels++;
        }
        private void ResetSaveLevels()
        {
            _savesLevel.Clear();

            for (int i = 0; i < _levels.Count; i++)
                _savesLevel.Add(new() { Name = _levels[i].Name, IsComplete = false });

            SaveLevels();

            _savesLevel.Clear();
            _levelData.Clear();

            LoadLevels();
        }

        #if UNITY_EDITOR
        [Button]
        private void AutoLoadLevels()
        {
            if (_levels.Count > 0) _levels.Clear();

            string[] paths = AssetDatabase.FindAssets($"t:{nameof(LevelSO)}");

            foreach (var path in paths)
                _levels.Add((LevelSO)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(path), typeof(LevelSO)));

        }
        #endif
    }
}
