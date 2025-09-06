using IdleArcade.Views;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IdleArcade
{
    public class Actor
    {
        public virtual ActorType ActorType { get; protected set; }
        public virtual ResourceType ResourceType { get; protected set; }
        public bool IsActive { get; protected set; }

        protected Game _owner;
        public View View { get; protected set; }

        public Actor()
        {            
        }

        public Actor(Game owner, View view, ResourceType resource)
        {
            _owner = owner;
            View = view;
            ResourceType = resource;

            _owner.AddActor(this);
        }

        public virtual void Run() { }

        protected Actor GetNearest(List<Actor> actors)
        {
            return actors.Where(o => o != null)
                .Aggregate((a, b) => Vector3.Distance(View.Position, a.View.Position) <
                                    Vector3.Distance(View.Position, b.View.Position) ? a : b);
        }
    }
}