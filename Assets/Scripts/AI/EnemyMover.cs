using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _runSpeed = 4.0f;
    [SerializeField] private float _walkSpeed = 3.0f;

    public NavMeshAgent NavMeshAgent { get; private set; }

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void Init(float walkSpeed, float runSpeed)
    {
        _walkSpeed = walkSpeed;
        _runSpeed = runSpeed;
    }

    public void Activate()
    {
        NavMeshAgent.enabled = true;
    }

    public void Deactivate()
    {
        NavMeshAgent.enabled = false;
    }

    public bool CheckNavMesh()
    {
        return NavMeshAgent.enabled;
    }

    public void SetRunSpeed()
    {
        NavMeshAgent.speed = _runSpeed;
    }

    public void SetWalkSpeed()
    {
        NavMeshAgent.speed = _walkSpeed;
    }

    public void Stop()
    {
        if (NavMeshAgent.isStopped == false)
            NavMeshAgent.isStopped = true;
    }

    public void Move(Transform target)
    {
        if (NavMeshAgent.isStopped == true)
            NavMeshAgent.isStopped = false;
        NavMeshAgent.SetDestination(target.position);
    }
}
