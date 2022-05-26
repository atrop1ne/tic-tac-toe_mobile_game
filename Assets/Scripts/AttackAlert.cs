using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAlert : MonoBehaviour
{
    public delegate void AttackAlertDelegate(List<CellCoordinates> cellsCoordinates);
    public static event AttackAlertDelegate AlertIsDoneEvent;

    [SerializeField]
    private GameObject attackAlertSprite;

    private bool alertIsDone = false;
    private List<GameObject> currentAlerts = new List<GameObject>();
    private List<CellCoordinates> currentCells;
    private float currentScale = 0.5f;
    private float destinationScale = 1.3f;
    private (float x, float y) cellSize;
    
    [SerializeField]
    private float scaleSpeed = 10.0f;
    private void FieldOnEnemyAttack(List<CellCoordinates> cellsCoordinates)
    {
        currentCells = cellsCoordinates;
        foreach (var cellCoordinates in cellsCoordinates)
        {
            var currentAlert = Instantiate(
                attackAlertSprite,
                new Vector2(
                    cellCoordinates.x * cellSize.x + attackAlertSprite.transform.position.x,
                    cellCoordinates.y * cellSize.y + attackAlertSprite.transform.position.y),
                Quaternion.identity);

            currentAlerts.Add(currentAlert);
        }
    }

    void Start()
    {
        currentCells = new List<CellCoordinates>();
        attackAlertSprite.transform.localScale = new Vector2(1, 1);
        EnemyController.EnemyAlertEvent += FieldOnEnemyAttack;

        cellSize = GameFieldManager.instance.GetFieldCellSize();
    }

    void Update()
    {
        if(!alertIsDone)
        {
            if(currentScale < destinationScale)
            {
                currentScale += scaleSpeed * Time.deltaTime;
                foreach (var ca in currentAlerts)
                {
                    ca.transform.localScale = new Vector2(currentScale, currentScale);
                }
            }

            else
            {
                foreach (var ca in currentAlerts)
                {
                    ca.transform.localScale = new Vector2(destinationScale, destinationScale);
                    Destroy(ca);
                }

                alertIsDone = true;
                currentAlerts.Clear();
            }
        }

        else
        {
            AlertIsDoneEvent(currentCells);
            alertIsDone = false;
            currentScale = 1;
        }
    }
}
