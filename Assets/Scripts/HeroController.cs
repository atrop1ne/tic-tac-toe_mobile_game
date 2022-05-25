using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    // [SerializeField]
    // private float moveSpeed = 0.01f;
    // private MoveState moveState = MoveState.Idle;
    private (float x, float y) fieldCellSize;
    private (float x, float y) edgeCellsCoordinates;
    private (float x, float y) currentCellCoord = (0, 0);

    [SerializeField]
    private GameFieldManager gfm;

    public void MoveHero(Vector2 direction)
    {
        (float x, float y) destinationCellCoord = (currentCellCoord.x + direction.x, currentCellCoord.y + direction.y);
        
        if(Mathf.Abs(destinationCellCoord.x) <= Mathf.Abs(1) 
            && Mathf.Abs(destinationCellCoord.y) <= Mathf.Abs(1))
            {
                var destinationPoint = new Vector2(transform.position.x + direction.x * fieldCellSize.x, 
                                                    transform.position.y + direction.y * fieldCellSize.y);

                transform.position = destinationPoint;
                currentCellCoord = destinationCellCoord;
            }
    }

    private void OnSwipe(Vector2 direction)
    {
        MoveHero(direction);
    }

    void Start()
    {
        SwipeDetection.SwipeEvent += OnSwipe;
        fieldCellSize = gfm.GetFieldCellSize();
        edgeCellsCoordinates = gfm.GetEdgeCellsCoords();
    }
}
