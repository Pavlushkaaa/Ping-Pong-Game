using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Core
{
    public class EffectApplier : MonoBehaviour
    {
        [SerializeField] private BallSystem _ballSystem;
        [SerializeField] private PlatformScaler _platformScaler;

        private Dictionary<EffectName, Action> _effects;

        public void ApplyEffect(EffectSettings effect)
        {
            _effects.TryGetValue(effect.Id, out var result);
            result.Invoke();
        }

        private void MultiplyBallsBy2() => _ballSystem.MultiplyBalls(2);
        private void MultiplyBallsBy3() => _ballSystem.MultiplyBalls(3);

        private void IncreasePlatformLenght() => _platformScaler.Increase();
        private void DecreasePlatformLenght() => _platformScaler.Decrease();

        private void IncreaseBallSpeed() => _ballSystem.IncreaseBallsSpeed();
        private void DecreaseBallSpeed() => _ballSystem.DecreaseBallsSpeed();

        private void Start()
        {
            _effects = new Dictionary<EffectName, Action>
            {
                { EffectName.BallMultiplierX2, MultiplyBallsBy2 },
                { EffectName.BallMultiplierX3, MultiplyBallsBy3 },
                { EffectName.BallSpeedDX2, DecreaseBallSpeed },
                { EffectName.BallSpeedX2, IncreaseBallSpeed },
                { EffectName.PlatformLenghtDX2, DecreasePlatformLenght },
                { EffectName.PlatformLenghtX2, IncreasePlatformLenght }
            };
        }

        #if UNITY_EDITOR
        public void CreateEffectNameEnum(List<EffectSettings> effects)
        {
            string path = $"{Application.dataPath + "/_Project/Core/Effects/EffectName.cs"}";

            var file = File.Open(path, FileMode.Create);
            StreamWriter wr = new StreamWriter(file);

            string firstParte = "namespace Core\r\n{\r\n    public enum EffectName\r\n    {";
            string lastParte = "    }\r\n}";
            wr.WriteLine(firstParte);

            for (int i = 0; i < effects.Count; i++)
            {
                effects[i].Id = (EffectName)i;

                string result = "        " + effects[i].Name.Replace(" ", "").Trim();
                if (i + 1 < effects.Count) result += ",";

                wr.WriteLine(result);
            }

            wr.WriteLine(lastParte);
            wr.Close();
        }
        #endif
    }
}
