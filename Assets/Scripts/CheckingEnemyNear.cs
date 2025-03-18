using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckingEnemyNear : MonoBehaviour
{
    private CharacterBase character;
    public Collider sphereCollider;
    private void Start()
    {
        character = GetComponentInParent<CharacterBase>();
        sphereCollider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            character.target = other.GetComponent<EnemyBase>();
            character.ChangeState(new AttackStateCharacter(character));
        }
    }

}
