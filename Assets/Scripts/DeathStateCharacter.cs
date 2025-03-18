using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathStateCharacter : IStateExample
{
    private readonly CharacterBase character;

    public DeathStateCharacter(CharacterBase character)
    {
        this.character = character;
    }

    public void Enter()
    {
        Debug.Log("Enter Death State Character");
        character.EnterDeath();
    }

    public void Update()
    {
        Debug.Log("Update Death State Character");
        character.UpdateDeath();
    }

    public void Exit()
    {
        Debug.Log("Exit Death State Character");
        character.ExitDeath();
    }
}
