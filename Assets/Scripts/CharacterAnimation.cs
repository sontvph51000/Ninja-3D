using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public Animator animator;

    public void PlayAnim(PlayerAnimationName name)
    {
        animator.Play(name.ToString());
    }


}
public enum PlayerAnimationName
{
    BlendTree,
    Attack,
    Death,
}
