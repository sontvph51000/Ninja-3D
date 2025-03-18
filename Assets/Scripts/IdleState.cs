using UnityEngine;

public class IdleState : IEnemyState
{
    public void EnterState(EnemyController enemy)
    {
        Debug.Log("Entered Idle State");
    }

    public void UpdateState(EnemyController enemy)
    {
        if (enemy.IsPlayerInRange())
        {
            enemy.SetState(new ChaseState());
        }
        else
        {
            enemy.SetState(new PatrolState());
        }
    }

    public void ExitState(EnemyController enemy)
    {
        Debug.Log("Exited Idle State");
    }
}
