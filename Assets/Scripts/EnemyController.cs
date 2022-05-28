using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public delegate void EnemyDelegate();
    public static event EnemyDelegate EnemyAlertEvent;
    public static event EnemyDelegate EnemyAttackEvent;

    [SerializeField]
    private int healthPoints = 5;
    [SerializeField]
    private float attackDelay = 2;
    private bool alertIsGoing = false;
    private bool alertIsDone = false;
    private float timer = 0;
    public AttackPattern currentPattern;
    public static EnemyController instance {get; private set;}

    [SerializeField]
    private List<AttackPattern> patterns;

    private void EnemyGetDamage()
    {
        healthPoints--;
    }
    
    private void OnAlertIsDone()
    {
        alertIsDone = true;
    }

    void Awake()
    {
        currentPattern = patterns[Random.Range(0, patterns.Count)];

        if(!instance)
        {
            instance = gameObject.GetComponent<EnemyController>();
        }
        else
        {
            Destroy(gameObject);
        }

        HeroController.HeroAttackEvent += EnemyGetDamage;
        AttackAlert.AlertIsDoneEvent += OnAlertIsDone;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(!alertIsGoing && timer >= attackDelay)
        {
            alertIsGoing = true;
            currentPattern = patterns[Random.Range(0, patterns.Count)];
            EnemyAlertEvent();
        }
        
        if(alertIsDone)
        {
            EnemyAttackEvent();
            alertIsGoing = false;
            alertIsDone = false;
            timer = 0;
        }
    }
}
