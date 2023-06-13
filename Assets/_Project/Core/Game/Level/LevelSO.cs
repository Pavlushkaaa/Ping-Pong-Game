using NaughtyAttributes;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Level", menuName = "Game Settings/Level")]
    public class LevelSO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public GameObject LevelPrefab { get; private set; }

        [Button]
        private void SetAutoName()
        {
            if (LevelPrefab == null) return;

            Name = LevelPrefab.name.Replace(" ", "_");
        }

    }
}
