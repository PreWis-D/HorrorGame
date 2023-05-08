using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float _radius;
    [Range(0, 360)][SerializeField] private float _angle;
    [SerializeField] private float _delay = 0.2f;

    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private LayerMask _obstructionMask;

    private Player _player;

    public bool CanSeePlayer { get; private set; }

    public LayerMask PlayerMask => _playerMask;
    public Player Player => _player;
    public float Radius => _radius;
    public float Angle => _angle;

    public event UnityAction<Player> PlayerSpotted;
    public event UnityAction PlayerMissed;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }

    private void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, _radius, _playerMask);

        if (rangeCheck.Length > 0 )
        {
            Transform target = rangeCheck[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < _angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obstructionMask))
                {
                    CanSeePlayer = true;
                    PlayerSpotted?.Invoke(_player);
                }
                else
                {
                    PlayerMissed?.Invoke();
                    CanSeePlayer = false;
                }
            }
            else if (CanSeePlayer)
            {
                PlayerMissed?.Invoke();
                CanSeePlayer = false;
            }
        }
    }
}
