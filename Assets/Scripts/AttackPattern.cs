using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackPattern
{
    public AnimationClip Animation;
    public List<CellCoordinates> cellsCoordinates;
    public GameObject Sprite;
}
