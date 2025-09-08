using UnityEngine;

namespace IdleArcade.Configs
{
    [CreateAssetMenu(fileName = "New Inventory Config", menuName = "Inventory Config", order = 1)]
    public class InventoryConfig : ScriptableObject
    {
        [SerializeField]
        private InventorySize _inventorySize;
        public InventorySize InventorySize => _inventorySize;
    }
}
