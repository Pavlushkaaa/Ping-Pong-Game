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

        private Dictionary<EffectName, Action> _effect;

        public void ApplyEffect(EffectSettings effect)
        {
            _effect.TryGetValue((EffectName)effect.Id, out var result);
            result.Invoke();
        }

        private void MultiplyBallsBy2() => _ballSystem.MultiplyBalls(2);
        private void MultiplyBallsBy3() => _ballSystem.MultiplyBalls(3);

        private void ChangePlatformLenghtBy2() => _platformScaler.Increase();
        private void ChangePlatformLenghtByD2() => _platformScaler.Decrease();

        private void Start()
        {
            _effect = new Dictionary<EffectName, Action>
            {
                { EffectName.BallMultiplierX2, MultiplyBallsBy2 },
                { EffectName.BallMultiplierX3, MultiplyBallsBy3 },
                { EffectName.PlatformLenghtX2, ChangePlatformLenghtBy2 },
                { EffectName.PlatformLenghtDX2, ChangePlatformLenghtByD2 }
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
                effects[i].Id = i;

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
