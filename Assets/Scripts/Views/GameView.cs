using UnityEngine;
using Utils;

namespace IdleArcade.Views
{
    [DefaultExecutionOrder(-999)]
    public class GameView : MonoBehaviour
    {
        void Awake()
        {
            ServiceLocator.Instance.Register(new Game());
        }
    }
}
