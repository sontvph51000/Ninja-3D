using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CharacterBase player = other.GetComponent<CharacterBase>();
            player.health -= 10f;
            if(player.health <= 0)
            {
                player.ChangeState( new DeathStateCharacter(player) );
            }
            Destroy(gameObject);
        }
    }
}
