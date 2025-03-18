using UnityEngine;

public class RunStateExample : IStateExample
{
    private readonly EnemyBase enemy;

    public RunStateExample(EnemyBase enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("Enter R State");
        enemy.EnterRun();
    }

    public void Update()
    {
        Debug.Log("Update R State");
        enemy.UpdateRun();
    }

    public void Exit()
    {
        Debug.Log("Exit R State");
        enemy.ExitRun();
    }
}
