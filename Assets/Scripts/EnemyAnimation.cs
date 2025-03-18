using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Animator animator;
    
   public void PlayAnim(EnemyAnimationName name)
    {
        animator.Play(name.ToString());
    }


}
public enum EnemyAnimationName
{
    Idle,
    Run,
    Death,
}    