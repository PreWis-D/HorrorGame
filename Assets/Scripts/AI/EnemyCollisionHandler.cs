using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class EnemyCollisionHandler : MonoBehaviour
{
    private Collider _collider;

    public event UnityAction<Player> PlayerCollisionEnter;
    public event UnityAction<Player> PlayerCollisionStay;
    public event UnityAction PlayerCollisionExit;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            PlayerCollisionEnter?.Invoke(player);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            PlayerCollisionStay?.Invoke(player);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            PlayerCollisionExit?.Invoke();
    }

    public void Activate()
    {
        _collider.enabled = true;
    }

    public void Deactivate()
    {
        _collider.enabled = false;
    }
}
