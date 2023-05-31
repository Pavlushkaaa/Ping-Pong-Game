using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Ball Speed Settings", menuName = "Game Settings/Ball Speed Settings", order = 0)]
    public class BallSpeedSO : ScriptableObject
    {
        [field: SerializeField] public float NormalSpeed;
        [field: SerializeField] public float MinSpeed;
        [field: SerializeField] public float MaxSpeed;
        [field: SerializeField] public float SpeedChangeStep;
    }
}
