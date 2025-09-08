using Cysharp.Threading.Tasks;
using UnityEngine;

namespace IdleArcade
{
    public class Inventory
    {
        public IInventoryView View { get; private set; }
        public ResourceType Type { get; private set; }
        public int Capacity { get; private set; } = 0;

        public Inventory(IInventoryView view, ResourceType type)
        {
            View = view;
            Type = type;
        }

        public async UniTask Add(Vector3 from, Material material)
        {
            await View.Push(from, material);
            Capacity++;
        }

        public async UniTask TransferFrom(Inventory another)
        {
            while (another.Capacity > 0)
            {
                another.Capacity--;
                Capacity++;
                await View.TransferFrom(another.View);
            }            
        }
    }
}
