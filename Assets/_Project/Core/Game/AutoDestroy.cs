using UnityEngine;

namespace Core
{
    public class AutoDestroy : MonoBehaviour
    {
        [SerializeField] private float _waitTime;
        private void Start() => Destroy(gameObject, _waitTime);
    }
}
