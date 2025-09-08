using IdleArcade.Configs;
using UnityEngine;
using Utils;

namespace IdleArcade.Views
{
    public class ResourceView : View
    {
        [SerializeField]
        private ResourceConfig _config;

        private void Awake()
        {
            new Resource(ServiceLocator.Instance.Get<Game>(), this, _config);
        }
    }
}

