using UnityEngine;
using Utils;

namespace IdleArcade.Views
{
    public class ResourceView : View
    {
        [SerializeField]
        private ResourceType _resource;

        private void Awake()
        {
            new Resource(ServiceLocator.Instance.Get<Game>(), this, _resource);
        }
    }
}

