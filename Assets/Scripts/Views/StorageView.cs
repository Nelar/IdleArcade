using UnityEngine;
using Utils;

namespace IdleArcade.Views
{
    public class StorageView : View
    {
        [SerializeField]
        private ResourceType _resource;

        private void Awake()
        {
            new Storage(ServiceLocator.Instance.Get<Game>(), this, _resource);
        }
    }
}
