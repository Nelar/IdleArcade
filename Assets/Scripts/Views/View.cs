using Cysharp.Threading.Tasks;
using UnityEngine;

namespace IdleArcade.Views
{
    public class View : MonoBehaviour, IView
    {
        public virtual Vector3 Position => transform.position;
        public virtual void Destroy() => GameObject.Destroy(gameObject);
    }
}

