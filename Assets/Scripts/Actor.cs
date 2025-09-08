using System.Collections.Generic;
using UnityEngine;

namespace IdleArcade
{
    public class Actor
    {
        public virtual ActorType ActorType { get; protected set; }
        public virtual ResourceType ResourceType { get; protected set; }
        public bool IsActive { get; set; } = false;

        protected Game _owner;
        public IView View { get; protected set; }

        public Actor() {}

        public Actor(Game owner, IView view, ResourceType resource)
        {
            _owner = owner;
            View = view;
            ResourceType = resource;

            _owner.AddActor(this);
        }
        public virtual void Destroy() 
        {
            _owner.RemoveActor(this);
            View.Destroy();
        }
        protected Actor GetNearest(List<Actor> actors, bool active = true)
        {
            Actor nearest = null;
            float minDistance = Mathf.Infinity;

            foreach (var actor in actors)
            {
                if (actor == null) continue;
                if (actor.IsActive != active) continue;

                if (!View.IsAlive || !actor.View.IsAlive) continue;

                float distance = Vector3.Distance(View.Position, actor.View.Position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = actor;
                }
            }

            return nearest;
        }
    }
}