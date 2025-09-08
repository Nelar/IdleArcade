using Cysharp.Threading.Tasks;

namespace IdleArcade
{
    public class Resource : Actor
    {        
        public override ActorType ActorType => ActorType.Resource;

        private int _capacity = 3;
        public bool IsEmpty =>  _capacity <= 0;

        public Resource(Game owner, IView view, ResourceType resource) : base(owner, view, resource)
        {
        }

        public async UniTask<Material> Mine()
        {                                    
            _capacity--;
            await UniTask.Delay(1000);
            if (IsEmpty) Destroy();
            return new Material(ResourceType, 1);
        }
    }
}
