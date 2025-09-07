using Cysharp.Threading.Tasks;
using IdleArcade.Views;

namespace IdleArcade
{
    public class Worker : Actor, IHaveInventory
    {        
        public override ResourceType ResourceType { get; protected set; }
        public override ActorType ActorType => ActorType.Worker;      
        private IWorkerView _workerView;

        private Actor _target;
        private Inventory _inventory;

        public Worker(Game owner, ResourceType resource, IWorkerView view, InventoryView inventoryView) : base()
        {
            _inventory = new Inventory(inventoryView, resource);
            _owner = owner;
            ResourceType = resource;
            View = view;
            _workerView = view;

            _owner.AddActor(this);            
        }

        public Inventory GetInventory() => _inventory;

        public async UniTask Action()
        {
            IsActive = true;
            while (true)
            {
                if (!await GoToResource()) break;
                await Work();
                if (!await GoToStorage()) break;
                if (!await Unload()) break;
            }            
            IsActive = false;
        }

        private async UniTask<bool> GoTo(ActorType actor, ResourceType resource)
        {
            var actors = _owner.GetActorsByTypeAndResource(actor, resource);
            _target = GetNearest(actors);
            if (_target == null) return false;

            await _workerView.GoTo(_target.View.Position);
            return true;
        }

        private async UniTask<bool> GoToStorage() => await GoTo(ActorType.Storage, ResourceType);
        private async UniTask<bool> GoToResource() => await GoTo(ActorType.Resource, ResourceType);

        private async UniTask Work()
        {
            _workerView.Work();
            var resource = _target as Resource;
            while (!resource.IsEmpty)
            {
                var material = await resource.Mine();
                await _inventory.Add(_target.View.Position, material);
            }
        }

        private async UniTask<bool> Unload()
        {
            _workerView.Unload();
            var storage = _target as Storage;
            await storage.LoadFrom(this);
            return true;
        }
    }
}
