using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public delegate void EnemyDelegate(List<CellCoordinates> cellsCoordinates);
    public static event EnemyDelegate EnemyAlertEvent;
    public static event EnemyDelegate EnemyAttackEvent;

    [SerializeField]
    private int healthPoints = 5;
    [SerializeField]
    private float attackDelay = 0f;
    private bool isAlertGoing = false;
    private float timer = 5f;
    private AttackPattern currentPattern;

    [SerializeField]
    public List<AttackPattern> patterns;

    private void EnemyGetDamage()
    {
        healthPoints--;
    }
    
    private void OnAlertDone(List<CellCoordinates> cellsCoordinates)
    {
        isAlertGoing = false;
        Debug.Log($"AlertDone {cellsCoordinates.Count}");
        EnemyAttackEvent(cellsCoordinates);
        timer = 0;
    }

    void Awake()
    {
        HeroController.HeroAttackEvent += EnemyGetDamage;
        AttackAlert.AlertIsDoneEvent += OnAlertDone;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(!isAlertGoing && timer >= attackDelay)
        {
            isAlertGoing = true;
            currentPattern = patterns[Random.Range(0, patterns.Count)];
            EnemyAlertEvent(currentPattern.cellsCoordinates);
        }
    }
}
