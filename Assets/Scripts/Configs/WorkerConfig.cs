using UnityEngine;

namespace IdleArcade.Configs
{
    [CreateAssetMenu(fileName = "New Worker Config", menuName = "Worker Config", order = 1)]
    public class WorkerConfig : ScriptableObject
    {
        [SerializeField]
        private ResourceType _resourceType;
        [SerializeField]
        private float _stoppingDistance = 3.0f;
        [SerializeField]
        private float _speed = 5.0f;

        public ResourceType ResourceType => _resourceType;
        public float StoppingDistance => _stoppingDistance;
        public float Speed => _speed;
    }
}
