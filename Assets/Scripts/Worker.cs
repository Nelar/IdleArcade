using Cysharp.Threading.Tasks;
using IdleArcade.Configs;

namespace IdleArcade
{
    public class Worker : Actor, IHaveInventory
    {        
        public override ResourceType ResourceType { get; protected set; }
        public override ActorType ActorType => ActorType.Worker;      
        private IWorkerView _workerView;

        private Actor _target;
        private Inventory _inventory;

        public Worker(Game owner, WorkerConfig config, IWorkerView view, IInventoryView inventoryView) : base()
        {
            _inventory = new Inventory(inventoryView, inventoryView.Config);
            _owner = owner;
            ResourceType = config.ResourceType;
            View = view;
            _workerView = view;

            _owner.AddActor(this);            
        }        
        public async void Action()
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
        private async UniTask<bool> GoTo(ActorType actor, ResourceType resource, bool active)
        {
            var actors = _owner.GetActorsByTypeAndResource(actor, resource);
            _target = GetNearest(actors, active);

            if (_target == null) return false;

            _target.IsActive = true;
            await _workerView.GoTo(_target.View.Position);
            return true;
        }
        private async UniTask<bool> GoToStorage() => await GoTo(ActorType.Storage, ResourceType, true);
        private async UniTask<bool> GoToResource() => await GoTo(ActorType.Resource, ResourceType, false);
        private async UniTask Work()
        {
            _workerView.Work();
            var resource = _target as Resource;
            while (!resource.IsEmpty)
            {
                var material = await resource.Mine();
                
                if (!_target.View.IsAlive) break;

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
        public Inventory GetInventory() => _inventory;
    }
}
