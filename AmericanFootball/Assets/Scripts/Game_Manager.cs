using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    [Header("Game Settings")]
    public bool GameStop = false;

    [Header("Enemy Settings")]
    public float Enemy_Speed;
    public int EnemySpawnCount;
    public float EnemySpawnTime1;
    public float EnemySpawnTime2;
    public float EnemySpawnTime3;
    public float EnemySpawnTime4;

    [Header("Friendly Settings")]
    public int Friendly_Speed;
    public int FriendlySpawnCount;
    public int Friendly_MinDistance;
    public int Friendly_MaxDistance;

    [Header("Map Settings")]
    public int Map_Count;

    [Header("Pooling Settings")]
    [SerializeField] private Enemy_Pooling _enemyPoolScript;
    [SerializeField] private Friendly_Pooling _friendlyPoolScript;


    private void FixedUpdate()
    {
        if (GameStop)
        {
            _enemyPoolScript.enabled = false;
            _friendlyPoolScript.enabled = false;
        }
    }

}
