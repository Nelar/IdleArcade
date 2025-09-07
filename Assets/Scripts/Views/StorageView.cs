using UnityEngine;
using Utils;

namespace IdleArcade.Views
{
    public class StorageView : View
    {
        [SerializeField]
        private ResourceType _resource;

        [SerializeField]
        private InventoryView _inventory;

        private void Awake()
        {
            new Storage(ServiceLocator.Instance.Get<Game>(), this, _inventory, _resource);
        }
    }
}
