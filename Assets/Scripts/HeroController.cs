using System.Linq;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public delegate void OnHeroAttackInput();
    public static event OnHeroAttackInput HeroAttackEvent;
    public static HeroController instance { get; private set; }

    // [SerializeField]
    // private float moveSpeed = 0.01f;
    // private MoveState moveState = MoveState.Idle;
    private (float x, float y) fieldCellSize;
    private (float x, float y) edgeCellsCoordinates;
    private (float x, float y) currentCellCoord = (0, 0);
    [SerializeField]
    private GameObject attackEffect;
    private GameObject currentAttack;
    private bool attackIsGoing = false;
    private float attackTimer = 0;
    [SerializeField]
    private int healthPoints = 5;
    [HideInInspector]
    public bool heroDeath;

    public void MoveHero(Vector2 direction)
    {
        (float x, float y) destinationCellCoord = (currentCellCoord.x + direction.x, currentCellCoord.y + direction.y);
        
        if(Mathf.Abs(destinationCellCoord.x) <= Mathf.Abs(edgeCellsCoordinates.x) 
            && Mathf.Abs(destinationCellCoord.y) <= Mathf.Abs(edgeCellsCoordinates.y))
            {
                var destinationPoint = new Vector2(transform.position.x + direction.x * fieldCellSize.x, 
                                                    transform.position.y + direction.y * fieldCellSize.y);

                transform.position = destinationPoint;
                currentCellCoord = destinationCellCoord;
            }
    }

    private void HeroAttack(Vector2 direction)
    {
        HeroAttackEvent();
        attackIsGoing = true;
        currentAttack = Instantiate(
            attackEffect,
            new Vector2(
                gameObject.transform.position.x,
                gameObject.transform.position.y + 1.5f), 
            Quaternion.identity
        );
    }

    private void OnSwipe(Vector2 direction)
    {
        if(currentCellCoord.y + direction.y > edgeCellsCoordinates.y
            && currentCellCoord.x == 0)
        {
                HeroAttack(direction);
        }
        
        else
        {
            MoveHero(direction);
        }
    }

    private void OnEnemyAttack()
    {
        if(EnemyController.instance.currentPattern.cellsCoordinates.Any(c => (float)c.x == currentCellCoord.x 
                                    && (float)c.y == currentCellCoord.y))
        {
            healthPoints--;
        };
    }

    private void Awake()
    {
        if (!instance)
        {
            instance = gameObject.GetComponent<HeroController>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        SwipeDetection.SwipeEvent += OnSwipe;
        EnemyController.EnemyAttackEvent += OnEnemyAttack;
        fieldCellSize = GameFieldManager.instance.GetFieldCellSize();
        edgeCellsCoordinates = GameFieldManager.instance.GetEdgeCellsCoords();
    }

    void Update()
    {
        if(attackIsGoing)
        {
            attackTimer += Time.deltaTime;
            if(attackTimer >= 0.2f)
            {
                Destroy(currentAttack);
                attackIsGoing = false;
                attackTimer = 0;
            }
        }
        if (healthPoints == 0)
            heroDeath = true;
    }
}
