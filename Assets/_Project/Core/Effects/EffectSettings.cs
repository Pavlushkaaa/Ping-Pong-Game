using NaughtyAttributes;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Effects Settings", menuName = "Game Settings/Effects Settings", order = 0)]
    public class EffectSettings : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public int DropChance { get; private set; }

        [Space]
        [ReadOnly] public EffectName Id;
    }
}
