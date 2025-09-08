using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace IdleArcade.Views
{
    public class WorkerView : MonoBehaviour, IWorkerView
    {
        public Vector3 Position => transform.position;        

        [SerializeField]
        private ResourceType _resource;

        [SerializeField]
        public float _stoppingDistance = 3.0f;

        [SerializeField]
        public float _workTime = 3.0f;

        [SerializeField]
        public float _unloadTime = 1.0f;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private InventoryView _inventory;

        [SerializeField]
        private NavMeshAgent _navMeshAgent;

        [SerializeField]
        [AnimationClipPicker]
        private string _idleAnimation;

        [SerializeField]
        [AnimationClipPicker]
        private string _workAnimation;

        [SerializeField]
        [AnimationClipPicker]
        private string _runAnimation;
        

        private void Awake() 
        {
            new Worker(ServiceLocator.Instance.Get<Game>(), _resource, this, _inventory);
        }

        public async UniTask GoTo(Vector3 position)
        {
            _animator.Play(_runAnimation);
            _navMeshAgent.SetDestination(position);
            
            await UniTask.WaitWhile(() => _navMeshAgent.isOnNavMesh && _navMeshAgent.remainingDistance > _stoppingDistance);
            
            if (_navMeshAgent.isOnNavMesh)
                _navMeshAgent.ResetPath();
        }

        public void Work()
        {
            _animator.Play(_workAnimation);            
        }        

        public void Unload()
        {
            _animator.Play(_idleAnimation);            
        }

        public void Destroy() => GameObject.Destroy(gameObject);

        public bool IsActive => gameObject != null;
    }
}
