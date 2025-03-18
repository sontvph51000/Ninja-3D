using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateCharacter : IStateExample
{
    private readonly CharacterBase character;

    public AttackStateCharacter(CharacterBase character)
    {
        this.character = character;
    }

    public void Enter()
    {
        Debug.Log("Enter Atk State Character");
        character.EnterAttack();
    }

    public void Update()
    {
        Debug.Log("Update Atk State Character");
        character.UpdateAttack();
    }

    public void Exit()
    {
        Debug.Log("Exit Atk State Character");
        character.ExitAttack();
    }
}
