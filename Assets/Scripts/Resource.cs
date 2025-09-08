using Cysharp.Threading.Tasks;
using IdleArcade.Configs;
using System;

namespace IdleArcade
{
    public class Resource : Actor
    {        
        public override ActorType ActorType => ActorType.Resource;

        private ResourceConfig _config;

        private int _capacity = 0;
        public bool IsEmpty =>  _capacity <= 0;

        public Resource(Game owner, IView view, ResourceConfig config) : base(owner, view, config.ResourceType)
        {
            _config = config;
            _capacity = config.Capacity;
        }

        public async UniTask<ResourceItem> Mine()
        {                                    
            _capacity--;
            await UniTask.Delay(TimeSpan.FromSeconds(_config.MineTime));
            if (IsEmpty) Destroy();
            return new ResourceItem(ResourceType, 1);
        }
    }
}
