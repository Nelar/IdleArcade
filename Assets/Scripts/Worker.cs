using Cysharp.Threading.Tasks;
using IdleArcade.Views;

namespace IdleArcade
{
    public class Worker : Actor
    {
        public override ResourceType ResourceType { get; protected set; }
        public override ActorType ActorType => ActorType.Worker;

        private WorkerView _workerView;

        private Actor _target;

        public Worker(Game owner, View view, ResourceType resource) : base()
        {
            _owner = owner;
            View = view;
            ResourceType = resource;
            _workerView = view as WorkerView;

            _owner.AddActor(this);            
        }

        public override async void Run()
        {
            IsActive = true;
            while (true)
            {
                if (!await GoToResource()) break;
                if (!await Work()) break;
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

        private async UniTask<bool> Work()
        {
            await _workerView.Work();
            var resource = _target as Resource;
            return true;
        }

        private async UniTask<bool> Unload()
        {
            await _workerView.Unload();
            var storage = _target as Storage;
            return true;
        }
    }
}
