using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowStateExample : IStateExample
{
    private readonly EnemyBase enemy;

    public FollowStateExample(EnemyBase enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("Enter Follow State");
        enemy.EnterIdle();
    }

    public void Update()
    {
        Debug.Log("Update Follow State");
        enemy.UpdateIdle();
    }

    public void Exit()
    {
        Debug.Log("Exit Follow State");
        enemy.ExitIdle();
    }
}
