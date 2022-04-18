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

    private bool fieldAlredyGenerated = false;

    public (float x, float y) GetEdgeCellsCoords()
    {
        return (
            fieldWidth / 2 * fieldCellSprites[0].transform.localScale.x,
            fieldHeight / 2 * fieldCellSprites[0].transform.localScale.y
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
        if (!fieldAlredyGenerated)
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
                            (x - FieldCenterCellIndexes.x) * current_sprite.transform.localScale.x,
                            (y - FieldCenterCellIndexes.y) * current_sprite.transform.localScale.y),
                        Quaternion.identity);
                }
            }

            fieldAlredyGenerated = true;
        }
    }

    void Start()
    {
        GenerateField();
    }
}
