using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private int healthPoints = 5;

    private void GetDamage()
    {
        healthPoints -= 1;
    }
    
    void Start()
    {
        HeroController.HeroAttackEvent += GetDamage;
    }
}
