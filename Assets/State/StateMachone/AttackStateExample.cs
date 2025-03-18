using UnityEngine;

public class AttackStateExample : IStateExample
{
    private readonly EnemyBase enemy;

    public AttackStateExample(EnemyBase enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("Enter Attack State");
        enemy.EnterAttack();
    }

    public void Update()
    {
        Debug.Log("Update Attack State");
        enemy.UpdateAttack();
    }

    public void Exit()
    {
        Debug.Log("Exit Attack State");
        enemy.ExitAttack();
    }
}
