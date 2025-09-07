using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

namespace IdleArcade.Views
{
    public class InventoryView : MonoBehaviour, IInventoryView
    {
        [Serializable]
        public struct MaterialDescription
        {
            [SerializeField]
            private ResourceType _type;
            [SerializeField]
            private View _prefab;

            public ResourceType Type => _type;
            public View Prefab => _prefab;
        }

        public Vector3 Position => transform.position;
        public void Destroy() => GameObject.Destroy(gameObject);        

        [SerializeField]
        private List<MaterialDescription> _materialDescriptions = new List<MaterialDescription>();

        [SerializeField]
        private List<IView> _items = new List<IView>();

        [SerializeField]
        private InventorySize _inventorySize;

        public async UniTask Push(Vector3 from, Material material)
        {
            var prefab = GetPrefabByType(material.Type);
            var instance = Instantiate(prefab, transform);
            instance.transform.position = from;

            var targetPos = _inventorySize.GetPoint(_items.Count);
            await instance.transform.DOLocalJump(targetPos, 1, 1, 0.3f).SetRecyclable().SetAutoKill();
            _ = instance.transform.DOLocalRotate(Vector3.zero, 0.3f).SetRecyclable().SetAutoKill();

            _items.Add(instance);
        }

        public IView Pop()
        {
            var item = _items[_items.Count - 1];
            _items.Remove(item);
            return item;
        }

        public async UniTask TransferFrom(IInventoryView another)
        {            
            var instance = another.Pop() as View;
            instance.transform.SetParent(transform);

            var targetPos = _inventorySize.GetPoint(_items.Count);
            await instance.transform.DOLocalJump(targetPos, 1, 1, 0.3f).SetRecyclable().SetAutoKill();
            _ = instance.transform.DOLocalRotate(Vector3.zero, 0.3f).SetRecyclable().SetAutoKill();
            _items.Add(instance);
        }

        private View GetPrefabByType(ResourceType type) => _materialDescriptions.Find(x => x.Type == type).Prefab;
    }
}
