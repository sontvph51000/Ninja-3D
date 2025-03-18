using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterMovementManager : MonoBehaviour
{
    public CharacterBase character;

    public VariableJoystick joystick;

    public CharacterController controller;

    public Canvas inputCanvas;

    public Animator animator;

    public GameObject Enemy;

    public float speed;

    public float rotateSpeed;

    public bool isJoystick;


    public void EnableJoystickInput()
    {
        isJoystick = true;
        inputCanvas.gameObject.SetActive(true);
    }

    public void EnterIdle()
    {
        animator.SetFloat("AnimationSpeed", 0);
    }

    public void UpdateIdle()
    {
        if (isJoystick)
        {
            var movementDirection = new Vector3(joystick.Direction.x, 0.0f, joystick.Direction.y);
            if (movementDirection.sqrMagnitude > 0)
            {
                character.ChangeState(new RunStateCharacter(character));
            }
        }
    }

    public void UpdateRun()
    {
        if (isJoystick)
        {
            var movementDirection = new Vector3(joystick.Direction.x, 0.0f, joystick.Direction.y);
            if (movementDirection.sqrMagnitude <= 0)
            {
                character.ChangeState(new IdleStateCharacter(character));
                return;
            }

            animator.SetFloat("AnimationSpeed", movementDirection.sqrMagnitude);
            var targetDirection = Vector3.RotateTowards(controller.transform.forward,
                movementDirection, rotateSpeed * Time.deltaTime, 0.0f);
            controller.transform.rotation = Quaternion.LookRotation(targetDirection);
        }
    }

    public void UpdateAttack()
    {
        if(isJoystick)
        {
            var movementDirection = new Vector3(joystick.Direction.x, 0.0f, joystick.Direction.y);
            controller.SimpleMove(movementDirection * speed);
            var targetDirection = Vector3.RotateTowards(controller.transform.forward,
               movementDirection, rotateSpeed * Time.deltaTime, 0.0f);
            controller.transform.rotation = Quaternion.LookRotation(targetDirection);
        }

    }

}
