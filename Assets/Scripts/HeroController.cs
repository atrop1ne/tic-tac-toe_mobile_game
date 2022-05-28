using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public delegate void OnHeroAttackInput();
    public static event OnHeroAttackInput HeroAttackEvent;

    // [SerializeField]
    // private float moveSpeed = 0.01f;
    // private MoveState moveState = MoveState.Idle;
    private (float x, float y) fieldCellSize;
    private (float x, float y) edgeCellsCoordinates;
    private (float x, float y) currentCellCoord = (0, 0);

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
            Debug.Log("Enemy attack hit");
        };
    }

    void Start()
    {
        SwipeDetection.SwipeEvent += OnSwipe;
        EnemyController.EnemyAttackEvent += OnEnemyAttack;
        fieldCellSize = GameFieldManager.instance.GetFieldCellSize();
        edgeCellsCoordinates = GameFieldManager.instance.GetEdgeCellsCoords();
    }
}
