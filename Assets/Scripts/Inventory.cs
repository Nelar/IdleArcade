using Cysharp.Threading.Tasks;
using IdleArcade.Configs;
using UnityEngine;

namespace IdleArcade
{
    public class Inventory
    {
        public IInventoryView View { get; private set; }
        public InventoryConfig Config { get; private set; }
        public int Capacity { get; private set; } = 0;

        public Inventory(IInventoryView view, InventoryConfig config)
        {
            View = view;
            Config = config;
        }

        public async UniTask Add(Vector3 from, ResourceItem material)
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
