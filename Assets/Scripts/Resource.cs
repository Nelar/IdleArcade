using IdleArcade.Views;

namespace IdleArcade
{
    public class Resource : Actor
    {
        public override ActorType ActorType => ActorType.Resource;

        public Resource(Game owner, View view, ResourceType resource) : base(owner, view, resource)
        {
        }
    }
}
