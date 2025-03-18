using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterBase : MonoBehaviour
{
    public Collider col;
    public CharacterAnimation anim;
    public CharacterMovementManager characterManager;
    public CheckingEnemyNear checkingEnemy;
    public CharacterState state;
    private IStateExample currentState;
    public float timeAnimAtk;
    public float timeInState;
    public float health = 100f;
    public EnemyBase target;


    private void Start()
    {
        ChangeState(new IdleStateCharacter(this));
        characterManager.EnableJoystickInput();
    }

    public void ChangeState(IStateExample newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }


    }

    //=============Idle State==============//
    public void EnterIdle()
    {
        anim.PlayAnim(PlayerAnimationName.BlendTree);
        state = CharacterState.Idle;
        characterManager.EnterIdle();
    }
    public void UpdateIdle()
    {
        characterManager.UpdateIdle();
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeState(new AttackStateCharacter(this));
        }
    }
    public void ExitIdle()
    {
        
    }

    //=============Run State==============//
    public void EnterRun()
    {
       state = CharacterState.Run;
    }
    public void UpdateRun()
    {
        characterManager.UpdateRun();
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeState(new AttackStateCharacter(this));
        }
    }
    public void ExitRun()
    {

    }

    //=============Attack State==============//
    public void EnterAttack()
    {

        state = CharacterState.Attack;
        anim.PlayAnim(PlayerAnimationName.Attack);
        DOVirtual.DelayedCall(timeAnimAtk, () =>
        {
            ChangeState(new IdleStateCharacter(this));
        });
        checkingEnemy.sphereCollider.enabled = false;
    }
    public void UpdateAttack()
    {
       // characterManager.UpdateAttack();
        timeInState += Time.deltaTime;
        if(timeInState > 0.4f)
        {
            target.ChangeState(new DeathStateExample(target));
        }
    }
    public void ExitAttack()
    {
            checkingEnemy.sphereCollider.enabled = true;
    }

    //=============Death State==============//
    public void EnterDeath()
    {
        state = CharacterState.Die;
        anim.PlayAnim(PlayerAnimationName.Death);
        checkingEnemy.enabled = false;
        col.enabled = false;
    }
    public void UpdateDeath()
    {

    }
    public void ExitDeath()
    {

    }


}
public enum CharacterState
{
    Idle,
    Run,
    Attack,
    Die
}
