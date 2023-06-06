using UnityEngine;

namespace Core
{
    public abstract class ScorePoint : SimplePoint
    {
        [SerializeField] [Range(1, 100)] private int _effectDropChance;

        public bool CheckEffectDrop() => Random.Range(0, 101) <= _effectDropChance;
    }
}
