using Cysharp.Threading.Tasks;
using IdleArcade.Configs;
using UnityEngine;

namespace IdleArcade
{    
    public interface IView
    {
        public Vector3 Position { get; }

        public bool IsAlive { get; }
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
        public InventoryConfig Config { get; }
        public UniTask Push(Vector3 from, ResourceItem material);
        public IView Pop();
        public UniTask TransferFrom(IInventoryView another);
    }    

    public interface IHaveInventory
    {
        public Inventory GetInventory();
    }
}
