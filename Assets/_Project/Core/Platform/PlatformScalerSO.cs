using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Platform Scaler Settings", menuName = "Game Settings/Platform Scaler Settings", order = 0)]
    public class PlatformScalerSO : ScriptableObject
    {
        [field: SerializeField] public float NormalLength { get; private set; }
        [field: SerializeField] public float Height { get; private set; }
        [field: SerializeField] public float Step { get; private set; }
        [field: SerializeField] public float MinLength { get; private set; }
        [field: SerializeField] public float MaxLength { get; private set; }
    }
}
