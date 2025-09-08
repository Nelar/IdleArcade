using Cysharp.Threading.Tasks;
using UnityEngine;

namespace IdleArcade
{    
    public interface IView
    {
        public Vector3 Position { get; }

        public bool IsActive { get; }
        public void Destroy();
    }

    public interface IWorkerView : IView
    {
        public UniTask GoTo(Vector3 position);

        public void Work();

        public void Unload();        
    }

    public interface IInventoryView : IView
    {
        public UniTask Push(Vector3 from, Material material);
        public IView Pop();
        public UniTask TransferFrom(IInventoryView another);
    }    

    public interface IHaveInventory
    {
        public Inventory GetInventory();
    }
}
