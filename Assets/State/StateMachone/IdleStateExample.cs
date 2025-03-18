using UnityEngine;

public class IdleStateExample : IStateExample
{
    private readonly EnemyBase enemy;

    public IdleStateExample(EnemyBase enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        enemy.EnterIdle();
    }

    public void Update()
    {
        enemy.UpdateIdle();
    }

    public void Exit()
    {
        enemy.ExitIdle();
    }
}
