using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DifficultySettings
{
    [SerializeField] private float _light;    
    [SerializeField] private int _inventoryMaxCount;    
    [SerializeField] private float _enemyWalkSpeed;    
    [SerializeField] private float _enemyRunSpeed;    
    [SerializeField] private float _enemyDelay;    
    [SerializeField] private float _enemyWaitingTime;    
    [SerializeField] private float _enemyFollowTime;    

    public float Light => _light;
    public int InventoryMaxCount => _inventoryMaxCount;
    public float EnemyWalkSpeed => _enemyWalkSpeed;
    public float EnemyRunSpeed => _enemyRunSpeed;
    public float EnemyDelay => _enemyDelay;
    public float EnemyWaitTime => _enemyWaitingTime;
    public float EnemyFollowTime => _enemyFollowTime;

}