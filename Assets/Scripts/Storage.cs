using IdleArcade.Views;

namespace IdleArcade
{
    public class Storage : Actor
    {        
        public override ActorType ActorType => ActorType.Storage;
        public Storage(Game owner, View view, ResourceType resource) : base(owner, view, resource)
        {
        }
    }
}