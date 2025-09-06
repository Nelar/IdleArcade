using System.Collections.Generic;
using Utils;

namespace IdleArcade
{
    public class Game : IService
    {
        private Dictionary<ActorType, List<Actor>> _actorByType = new Dictionary<ActorType, List<Actor>>();
        private Dictionary<ResourceType, List<Actor>> _actorByResource = new Dictionary<ResourceType, List<Actor>>();

        private List<Actor> _actors = new List<Actor>();

        public void AddActor(Actor actor)
        {
            var resourceType = actor.ResourceType;
            var actorType = actor.ActorType;

            if (_actorByResource.ContainsKey(resourceType))
                _actorByResource[resourceType].Add(actor);
            else
                _actorByResource.Add(resourceType, new List<Actor>() { actor });

            if (_actorByType.ContainsKey(actorType))
                _actorByType[actorType].Add(actor);
            else
                _actorByType.Add(actorType, new List<Actor>() { actor });

            _actors.Add(actor);
            actor.Run();

            foreach (var anotherActor in _actors)
            {
                if (!anotherActor.IsActive) anotherActor.Run();
            }
        }

        public List<Actor> GetActorsByType(ActorType type) => _actorByType[type];
        public List<Actor> GetActorsByResource(ResourceType type) => _actorByResource[type];

        public List<Actor> GetActorsByTypeAndResource(ActorType actor, ResourceType resource)
            => _actors.FindAll(x => x.ResourceType == resource && x.ActorType == actor);
    }
}