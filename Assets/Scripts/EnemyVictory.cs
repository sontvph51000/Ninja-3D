using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVictory : IStateExample
{
    private readonly EnemyBase enemyBase;

    public EnemyVictory(EnemyBase enemyBase)
    {
        this.enemyBase = enemyBase;
    }

    public void Enter()
    {
        enemyBase.EnterVictory();
    }

    public void Update()
    {
        enemyBase.UpdateVictory();
    }

    public void Exit()
    {
        enemyBase.ExitVictory();
    }
}
