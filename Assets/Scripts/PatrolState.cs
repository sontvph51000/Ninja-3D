using UnityEngine;

public class PatrolState : IEnemyState
{
    public void EnterState(EnemyController enemy)
    {
        Debug.Log("Entered Patrol State");
    }

    public void UpdateState(EnemyController enemy)
    {
        if (enemy.IsPlayerInRange())
        {
            enemy.SetState(new ChaseState());
        }
        else
        {
            Transform targetPoint = enemy.patrolPoints[enemy.currentPatrolIndex];
            enemy.MoveTo(targetPoint.position);

            if (Vector3.Distance(enemy.transform.position, targetPoint.position) < 0.1f)
            {
                // Chuy?n sang ?i?m tu?n tra ti?p theo
                enemy.currentPatrolIndex = (enemy.currentPatrolIndex + 1) % enemy.patrolPoints.Length;
            }
        }
    }

    public void ExitState(EnemyController enemy)
    {
        Debug.Log("Exited Patrol State");
    }
}
