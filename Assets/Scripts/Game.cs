using System.Collections.Generic;
using Utils;

namespace IdleArcade
{
    public class Game : IService
    {
        private List<Actor> _actors = new List<Actor>();

        public void AddActor(Actor actor)
        {
            _actors.Add(actor);
                        
            var slackers = _actors.FindAll(x=>x.IsActive == false && x.ActorType == ActorType.Worker 
                                            && x.ResourceType == actor.ResourceType).ConvertAll(x=>x as Worker);
            slackers.ForEach(x => x.Action());
        }

        public void RemoveActor(Actor actor)
        {            
            _actors.Remove(actor);
        }

        public List<Actor> GetActorsByTypeAndResource(ActorType actor, ResourceType resource)
            => _actors.FindAll(x => x.ResourceType == resource && x.ActorType == actor);
    }
}