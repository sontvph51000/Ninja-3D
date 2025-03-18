using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateCharacter : IStateExample
{
    private readonly CharacterBase character;

    public IdleStateCharacter(CharacterBase character)
    {
        this.character = character;
    }

    public void Enter()
    {
        character.EnterIdle();
    }

    public void Update()
    {
        character.UpdateIdle();
    }

    public void Exit()
    {
        character.ExitIdle();
    }
}
