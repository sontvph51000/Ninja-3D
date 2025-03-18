using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using UnityEngine.AI;
using WarriorAnims;

public class EnemyBase : MonoBehaviour
{

    public EnemyAnimation anim;
    public CharacterBase Player;
    public EnemyState State;
    private VisionCone visionCone;
    public GunControl gunControl;
    private IStateExample currentState;
    private float timeInState;
    private bool isRotatingBack = false;
    protected float speedRotate = 1f;
    protected float deltaTimeRotate = 3f;
    protected float speed = 3f;
    private Tween tweenRotate;
    private Tween tweenMove;

    protected float timeToShot = 0.2f;
    public Vector3 lastKnownPosition;      // Điểm cuối cùng mà Player xuất hiện
    public bool playerDetected = false;   // Kiểm tra xem Player có trong vùng nhìn không

    private void Start()
    {
        visionCone = GetComponentInChildren<VisionCone>();
        ChangeState(new IdleStateExample(this));
    }

    public void ChangeState(IStateExample newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        timeInState = 0f;
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

    public void MoveToLastPoint(Vector3 targetPoint)
    {
        float s = Vector3.Distance(transform.position, targetPoint);
        tweenMove = transform.DOMove(targetPoint, s / speed).OnComplete(() => {
            ChangeState(new IdleStateExample(this));
        });
    }

    //==========Idle state============//
    public void EnterIdle()
    {
        State = EnemyState.Idle;
        anim.PlayAnim(EnemyAnimationName.Idle);
    }

    public void UpdateIdle()
    {
        if (Player.state == CharacterState.Die)
        {
            ChangeState(new EnemyVictory(this));
            return;
        }
        visionCone.DrawVisionCone(); // Vẽ vùng nhìn

        visionCone.CheckingPlayerInSight(); //Check vị trí Player

        timeInState += Time.deltaTime;
        if(timeInState >= deltaTimeRotate)
        {
            float angle;
            if (isRotatingBack)
            {
                angle = -180;
                isRotatingBack = false;
            }
            else
            {
                angle = 180;
                isRotatingBack = true;
            }

            timeInState = 0f;
            Quaternion rotateAngle = transform.rotation * Quaternion.Euler(0, angle, 0);
            tweenRotate = transform.DORotateQuaternion(rotateAngle, speedRotate);
            //
        }
       
    }
    public void ExitIdle()
    {
        tweenRotate.Kill();
    }

    //==========Run state============//
    public void EnterRun()
    {
        MoveToLastPoint(Player.transform.position);
        State = EnemyState.Run;
        anim.PlayAnim(EnemyAnimationName.Run);
    }

    public void UpdateRun()
    {
        visionCone.DrawVisionCone(); // Vẽ vùng nhìn
        visionCone.CheckingPlayerInSight(); //Check vị trí Player
    }
    public void ExitRun()
    {
        
    }

    //==============Attack state================//
    public void EnterAttack()
    {
        anim.PlayAnim(EnemyAnimationName.Idle);
        if(tweenMove != null)
        {
            tweenMove.Kill();
        }
        State = EnemyState.Atk;
    }
    public void UpdateAttack()
    {
        visionCone.DrawVisionCone(); // Vẽ vùng nhìn
        visionCone.CheckingPlayerOutSight();
        transform.LookAt(Player.transform.position);
        timeInState += Time.deltaTime;
        if(timeInState >= timeToShot)
        {
            gunControl.Shot();
            timeInState = 0f;
        }

        if(Player.state == CharacterState.Die)
        {
            ChangeState(new EnemyVictory(this));
        }
    }
    public void ExitAttack()
    {

    }

    //==============Death state================//
    public void EnterDeath()
    {
        State = EnemyState.Die;
        visionCone.gameObject.SetActive(false);
        GetComponent<Collider>().enabled = false;
        anim.PlayAnim(EnemyAnimationName.Death);
    }
    public void UpdateDeath()
    {

    }
    public void ExitDeath()
    {

    }

    //================Enemy Victory===============//
    public void EnterVictory()
    {
        State = EnemyState.Victory;
        anim.PlayAnim(EnemyAnimationName.Idle);
        visionCone.VisionConeMesh.Clear();
        visionCone.enabled = false;
    }
    public void UpdateVictory()
    {

    }
    public void ExitVictory()
    {

    }


}

public enum EnemyState
{
    Idle,
    Run,
    Atk,
    Die,
    Victory
}