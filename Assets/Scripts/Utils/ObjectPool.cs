using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class ObjectPool : MonoBehaviour
    {
        public GameObject prefab;
        public int initialSize = 10;

        private Queue<GameObject> pool = new Queue<GameObject>();

        void Awake()
        {
            for (int i = 0; i < initialSize; i++)
            {
                AddObjectToPool();
            }
        }

        public GameObject Get()
        {
            if (pool.Count == 0)
                AddObjectToPool();

            var obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        public void Return(GameObject obj)
        {
            obj.SetActive(false);
            pool.Enqueue(obj);
        }

        private void AddObjectToPool()
        {
            var obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

}