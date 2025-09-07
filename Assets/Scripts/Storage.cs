using Cysharp.Threading.Tasks;
using IdleArcade.Views;

namespace IdleArcade
{
    public class Storage : Actor, IHaveInventory
    {        
        public override ActorType ActorType => ActorType.Storage;

        private Inventory _inventory;

        public Storage(Game owner, IView view, IInventoryView inventoryView, ResourceType resource) : base(owner, view, resource)
        {
            _inventory = new Inventory(inventoryView, resource);
        }

        public async UniTask LoadFrom(Worker worker)
        {
            await _inventory.TransferFrom(worker.GetInventory());
        }

        public Inventory GetInventory() => _inventory;
    }
}