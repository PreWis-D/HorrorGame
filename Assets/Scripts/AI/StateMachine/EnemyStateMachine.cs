using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyState _firstState;

    private EnemyState _currentState;
    private Enemy _enemy;

    public EnemyState CurrentState => _currentState;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        Reset(_firstState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        var nextState = _currentState.GetNextState();
        if (nextState != null)
            Transit(nextState);
    }

    public void Activate()
    {
        Reset(_firstState);
    }

    public void Deactivate()
    {
        _currentState.enabled = false;
    }

    private void Reset(EnemyState startState)
    {
        _currentState = startState;

        if (_currentState != null)
            _currentState.Enter(_enemy);
    }

    private void Transit(EnemyState nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(_enemy);
    }
}
