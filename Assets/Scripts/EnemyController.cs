using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public delegate void OnEnemyAttack(List<CellCoordinates> cellsCoordinates);
    public static event OnEnemyAttack EnemyAttackEvent;

    [SerializeField]
    private int healthPoints = 5;
    private bool isAtacking = false;
    private float attackDelay = 0.5f;
    private float timer = 0;

    [SerializeField]
    public List<AttackPattern> patterns;

    private void EnemyGetDamage()
    {
        healthPoints--;
    }
    
    void Start()
    {
        HeroController.HeroAttackEvent += EnemyGetDamage;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(!isAtacking && timer >= attackDelay)
        {
            isAtacking = true;
            var pattern = patterns[Random.Range(0, patterns.Count)];
            EnemyAttackEvent(pattern.cellsCoordinates);
            isAtacking = false;
            timer = 0;
        }
    }
}
