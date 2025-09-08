using UnityEngine;

namespace IdleArcade.Views
{
    public class View : MonoBehaviour, IView
    {
        public virtual Vector3 Position => transform.position;
        public virtual bool IsAlive => gameObject != null;
        public virtual void Destroy() => GameObject.Destroy(gameObject);
    }
}

