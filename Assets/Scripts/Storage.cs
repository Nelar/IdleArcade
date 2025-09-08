using Cysharp.Threading.Tasks;
using IdleArcade.Configs;

namespace IdleArcade
{
    public class Storage : Actor, IHaveInventory
    {        
        public override ActorType ActorType => ActorType.Storage;

        private Inventory _inventory;

        public Storage(Game owner, IView view, IInventoryView inventoryView, StorageConfig _config) : base(owner, view, _config.ResourceType)
        {
            _inventory = new Inventory(inventoryView, inventoryView.Config);
            IsActive = true;
        }

        public async UniTask LoadFrom(Worker worker) => await _inventory.TransferFrom(worker.GetInventory());
        public Inventory GetInventory() => _inventory;
    }
}