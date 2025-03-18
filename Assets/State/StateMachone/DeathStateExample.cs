using UnityEngine;

public class DeathStateExample : IStateExample
{
    private readonly EnemyBase enemy;

    public DeathStateExample(EnemyBase enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("Enter Death State");
        enemy.EnterDeath();
    }

    public void Update()
    {
        Debug.Log("Update Death State");
        enemy.EnterDeath();
    }

    public void Exit()
    {
        Debug.Log("Exit Death State");
        enemy.EnterDeath();
    }
}
