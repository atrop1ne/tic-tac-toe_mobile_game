using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    // [SerializeField]
    // private float moveSpeed = 0.01f;
    // private MoveState moveState = MoveState.Idle;
    private (float x, float y) fieldCellSize;
    private (float top, float bottom, float right, float left) edgeCellsCoordinates;

    [SerializeField]
    private GameFieldManager gfm;

    public void MoveHero(Vector2 direction)
    {
        var destinationPoint = new Vector2(transform.position.x + direction.x * fieldCellSize.x, transform.position.y + direction.y * fieldCellSize.y);
        Debug.Log(destinationPoint);
        if(destinationPoint.x <= edgeCellsCoordinates.right && destinationPoint.x >= edgeCellsCoordinates.left &&
            destinationPoint.y <= edgeCellsCoordinates.top && destinationPoint.y >= edgeCellsCoordinates.bottom)
            {
                transform.position = destinationPoint;
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
