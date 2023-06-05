using UnityEngine;

namespace Core
{
    public class BallDeadZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Ball>(out var ball))
                ball.Die();
            else if(collision.TryGetComponent<Effect>(out var effect))
                effect.FailDestroy();
        }
    }
}
