using UnityEngine;

public class ChaseState : IEnemyState
{
    public void EnterState(EnemyController enemy)
    {
        Debug.Log("Entered Chase State");
    }

    public void UpdateState(EnemyController enemy)
    {
        if (!enemy.IsPlayerInRange())
        {
            enemy.SetState(new PatrolState());
        }
        else
        {
            enemy.MoveTo(enemy.player.position);
        }
    }

    public void ExitState(EnemyController enemy)
    {
        Debug.Log("Exited Chase State");
    }
}
