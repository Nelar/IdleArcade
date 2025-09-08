using Cysharp.Threading.Tasks;
using IdleArcade.Configs;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace IdleArcade.Views
{
    public class WorkerView : MonoBehaviour, IWorkerView
    {
        [SerializeField]
        private WorkerConfig _config;

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

        public Vector3 Position => transform.position;

        private void Awake() 
        {
            _navMeshAgent.speed = _config.Speed;
            new Worker(ServiceLocator.Instance.Get<Game>(), _config, this, _inventory);
        }

        public async UniTask GoTo(Vector3 position)
        {
            _animator.Play(_runAnimation);
            _navMeshAgent.SetDestination(position);
            
            await UniTask.WaitWhile(() => _navMeshAgent.isOnNavMesh && _navMeshAgent.remainingDistance > _config.StoppingDistance);
            
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

        public bool IsAlive => gameObject != null;
    }
}
