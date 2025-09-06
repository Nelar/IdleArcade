using System.Collections.Generic;
using System;
using UnityEngine;
using Utils;

namespace Utils
{
    [DefaultExecutionOrder(-1000)]
    public class ServiceLocator : MonoBehaviour
    {
        private static ServiceLocator _instance;
        public static ServiceLocator Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("ServiceLocator");
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<ServiceLocator>();
                }
                return _instance;
            }
        }

        private readonly Dictionary<Type, IService> _services = new();

        public void Register<T>(T service) where T : IService
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
                throw new Exception($"Service {type.Name} alredy registered");
            _services[type] = service;
        }

        public T Get<T>() where T : IService
        {
            var type = typeof(T);
            foreach (var service in _services)
            {
                if (service.Value.GetType() == type || service.Value.GetType().IsSubclassOf(type))
                    return (T)service.Value;
            }

            throw new Exception($"Service {type.Name} not registered");
        }

        public bool IsRegistered<T>() where T : IService
        {
            return _services.ContainsKey(typeof(T));
        }

        public void Unregister<T>() where T : IService
        {
            _services.Remove(typeof(T));
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}