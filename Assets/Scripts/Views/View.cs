using UnityEngine;

namespace IdleArcade.Views
{
    public class View : MonoBehaviour
    {
        public virtual Vector3 Position => transform.position;
    }
}

