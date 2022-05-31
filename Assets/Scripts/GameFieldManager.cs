using System.Collections.Generic;
using UnityEngine;

public class GameFieldManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> fieldCellSprites;

    [SerializeField]
    private int fieldWidth = 3;
    [SerializeField]
    private int fieldHeight = 3;
    public (float x, float y) fieldCenterPosition;

    public static GameFieldManager instance {get; private set;}

    public (float x, float y) GetEdgeCellsCoords()
    {
        return (
            fieldWidth / 2, fieldHeight / 2
        );
    }

    public (float x, float y) GetFieldCellSize()
    {
        return (
            fieldCellSprites[0].transform.localScale.x,
            fieldCellSprites[0].transform.localScale.y
        );
    }

    private void GenerateField()
    {   
        if (!instance)
        {
            (int x, int y) FieldCenterCellIndexes = (fieldWidth / 2, fieldHeight / 2);
            for (int y = 0; y < fieldHeight; y++)
            {
                for (int x = 0; x < fieldWidth; x++)
                {
                    var current_sprite = fieldCellSprites[0];
                    var currentCell = Instantiate(
                        current_sprite, 
                        new Vector2(
                            (x - FieldCenterCellIndexes.x) * current_sprite.transform.localScale.x + fieldCenterPosition.x,
                            (y - FieldCenterCellIndexes.y) * current_sprite.transform.localScale.y + fieldCenterPosition.y),
                        Quaternion.identity);

                    currentCell.name = $"Cell ({x - FieldCenterCellIndexes.x}, {y - FieldCenterCellIndexes.y})";
                }
            }

            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }
    private void Awake()
    {
        fieldCenterPosition = (transform.position.x, transform.position.y);
    }

    void OnLevelWasLoaded()
    {
        GenerateField();
    }
}
