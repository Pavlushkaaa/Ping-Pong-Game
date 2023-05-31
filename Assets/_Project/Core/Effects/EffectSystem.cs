using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Core
{
    public class EffectSystem : MonoBehaviour
    {
        [SerializeField] private EffectApplier _effectApplier;

        [Space]
        [SerializeField] private Effect _effectPrefab;
        [SerializeField] private List<EffectSettings> _effects;
        [HideInInspector] [SerializeField] private List<int> _dropChanceIds;

        private PointSystem _pointSystem;

        private List<Effect> _existEffects = new List<Effect>();

        public void Reset(PointSystem pointSystem = null)
        {
            if (_existEffects.Count != 0)
                foreach (var effect in _existEffects)
                {
                    effect.Destroyed -= OnDestroyed;
                    effect.Caught -= OnCaught;

                    effect.Destroy();
                }

            _existEffects.Clear();

            _pointSystem = pointSystem;

            if(_pointSystem != null)
                _pointSystem.PointDestroyed += SpawnEffect;
        }

        private void SpawnEffect(Vector2 position)
        {
            if(!GameLoop.IsLooping) return;

            var id = _dropChanceIds[GenerateNumber()];
            if (id == -1) return;

            var effect = Instantiate(_effectPrefab, position, Quaternion.identity);
            effect.Initialize(_effects[id]);

            _existEffects.Add(effect);

            effect.Destroyed += OnDestroyed;
            effect.Caught += OnCaught;
        }

        private void OnCaught(Effect effect)
        {
            effect.Caught -= OnCaught;
            _effectApplier.ApplyEffect(effect.Settings);
        }
        private void OnDestroyed(Effect effect)
        {
            effect.Destroyed -= OnDestroyed;
            _existEffects.Remove(effect);
        }
         
        private int GenerateNumber(bool regenerate = false)
        {
            int customSeed = new System.Random().Next(0, 1000000);
            UnityEngine.Random.InitState(new System.Random().Next(0, 1000000));

            return UnityEngine.Random.Range(0, 101);
        }

        #if UNITY_EDITOR
        [Button]
        private void CheckEffects()
        {
            if (_effects.Count == 0) LoadEffectsToScipt();

            int sum = 0;

            foreach (var effect in _effects)
            {
                if (effect.Name.Trim().Length <= 1)
                {
                    print($"ERROR! {effect.name} hasn't name!");
                    return;
                }

                if (effect.DropChance < 1 || effect.DropChance > 100)
                {
                    print($"ERROR! {effect.name} is invalid drop chance: {effect.DropChance}");
                    return;
                }

                if (effect.Sprite == null)
                {
                    print($"ERROR! {effect.Name} hasn't sprite!");
                    return;
                }

                sum += effect.DropChance;
            }

            if (sum < 1 || sum > 100)
                print($"ERROR! Sum of effects drop chance is invalid value: {sum}");
            else
                print($"SUCCSESS! Sum of effects drop chance: {sum}");
        }
        [Button]
        private void GenerateEffectData()
        {
            LoadEffectsToScipt();
            CheckEffects();

            _dropChanceIds.Clear();
            for (int i = 0; i < _effects.Count; i++)
                for (int j = 0; j < _effects[i].DropChance; j++)
                    _dropChanceIds.Add(i);

            for (int i = _dropChanceIds.Count() - 1; i < 100; i++)
                _dropChanceIds.Add(-1);

            _effectApplier.CreateEffectNameEnum(_effects);
        }
        private void LoadEffectsToScipt()
        {
            if (_effects.Count > 0) _effects.Clear();

            string[] paths = AssetDatabase.FindAssets($"t:{nameof(EffectSettings)}");

            foreach (var path in paths)
                _effects.Add((EffectSettings)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(path), typeof(EffectSettings)));
        }
        #endif
    }
}
