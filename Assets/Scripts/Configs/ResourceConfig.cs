using UnityEngine;

namespace IdleArcade.Configs
{
    [CreateAssetMenu(fileName = "New Resource Config", menuName = "Resource Config", order = 1)]
    public class ResourceConfig : ScriptableObject
    {
        [SerializeField]
        private ResourceType _resourceType;
        [SerializeField]
        private int _capacity;
        [SerializeField]
        private float _mineTime;

        public int Capacity => _capacity;
        public ResourceType ResourceType => _resourceType;
        public float MineTime => _mineTime;
    }
}
