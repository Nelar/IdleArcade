using UnityEngine;

namespace IdleArcade.Configs
{
    [CreateAssetMenu(fileName = "New Storage Config", menuName = "Storage Config", order = 1)]
    public class StorageConfig : ScriptableObject
    {
        [SerializeField]
        private ResourceType _resourceType;

        public ResourceType ResourceType => _resourceType;
    }
}
