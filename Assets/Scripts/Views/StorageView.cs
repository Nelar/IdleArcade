using IdleArcade.Configs;
using UnityEngine;
using Utils;

namespace IdleArcade.Views
{
    public class StorageView : View
    {
        [SerializeField]
        private StorageConfig _config;

        [SerializeField]
        private InventoryView _inventory;

        private void Awake()
        {
            new Storage(ServiceLocator.Instance.Get<Game>(), this, _inventory, _config);
        }
    }
}
