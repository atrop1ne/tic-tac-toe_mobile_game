using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public delegate void EnemyDelegate();
    public static event EnemyDelegate EnemyAlertEvent;
    public static event EnemyDelegate EnemyAttackEvent;

    private Animator animator;

    [SerializeField]
    private Text enemyNameTextField;
    [SerializeField]
    private int healthPoints = 5;
    [SerializeField]
    private float attackDelay = 0.5f;
    [SerializeField]
    private EnemyHealthBar healthBar;
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
        healthBar.SetSliderValue(healthPoints);
    }
    
    private void OnAlertIsDone()
    {
        alertIsDone = true;
    }

    void Awake()
    {
        currentPattern = patterns[Random.Range(0, patterns.Count)];
        animator = gameObject.GetComponent<Animator>();

        if(!instance)
        {
            instance = this;
        }

        HeroController.HeroAttackEvent += EnemyGetDamage;
        AttackAlert.AlertIsDoneEvent += OnAlertIsDone;

        Debug.Log(currentPattern);
    }

    void Start()
    {
        healthBar.InitalizeSlider(healthPoints);
        enemyNameTextField = healthBar.GetComponentInChildren<Text>();
        enemyNameTextField.text = gameObject.name;
    }

    void OnDestroy()
    {
        HeroController.HeroAttackEvent -= EnemyGetDamage;
        AttackAlert.AlertIsDoneEvent -= OnAlertIsDone;
    }

    void FixedUpdate()
    {
        if(healthPoints <= 0)
        {
            LevelManager.instance.CompleteLevel();
        }

        timer += Time.deltaTime;
        if(!alertIsGoing && timer >= attackDelay)
        {
            alertIsGoing = true;
            currentPattern = patterns[Random.Range(0, patterns.Count)];
            EnemyAlertEvent();
            animator.Play(currentPattern.Animation.name);
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
