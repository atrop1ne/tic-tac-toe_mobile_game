using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAlert : MonoBehaviour
{
    public delegate void AttackAlertDelegate();
    public static event AttackAlertDelegate AlertIsDoneEvent;
    private bool alertIsDone = false;
    private List<GameObject> currentAlerts = new List<GameObject>();
    private float currentScale = 0.5f;
    private float destinationScale = 1.3f;
    private (float x, float y) cellSize;
    private (float x, float y) fieldCenter;

    [SerializeField]
    private float scaleSpeed = 4;
    private void OnEnemyAlert()
    {
        var pattern = EnemyController.instance.currentPattern;
        foreach (var cellCoordinates in pattern.cellsCoordinates)
        {
            var currentAlert = Instantiate(
                pattern.Sprite,
                new Vector2(
                    cellCoordinates.x * cellSize.x + fieldCenter.x,
                    cellCoordinates.y * cellSize.y + fieldCenter.y),
                Quaternion.identity);

            currentAlerts.Add(currentAlert);
        }
    }

    void Start()
    {
        EnemyController.EnemyAlertEvent += OnEnemyAlert;

        cellSize = GameFieldManager.instance.GetFieldCellSize();
        fieldCenter = GameFieldManager.instance.fieldCenterPosition;
    }

    void Update()
    {
        if(!alertIsDone)
        {
            if (currentScale < destinationScale)
            {
                currentScale += Time.deltaTime / scaleSpeed;
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
                }

                alertIsDone = true;
            }
        }

        else
        {
            foreach (var ca in currentAlerts)
            {
                Destroy(ca);
            }
            currentAlerts.Clear();
            AlertIsDoneEvent();
            alertIsDone = false;
            currentScale = 0.5f;
        }
    }
}
