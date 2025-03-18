using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunStateCharacter : IStateExample
{
    private readonly CharacterBase character;

    public RunStateCharacter(CharacterBase character)
    {
        this.character = character;
    }

    public void Enter()
    {
        Debug.Log("Enter Run State Character");
        character.EnterRun();
    }

    public void Update()
    {
        Debug.Log("Update Run State Character");
        character.UpdateRun();
    }

    public void Exit()
    {
        Debug.Log("Exit Run State Character");
        character.ExitRun();
    }
}
