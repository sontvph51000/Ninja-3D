using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    float velocity = 0.0f;
    public float acce = 0.1f;
    int VelocityHash;

    // Start is called before the first frame update
    void Start()
    {
       animator = GetComponent<Animator>();

        VelocityHash = Animator.StringToHash("Velocity");
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey("q");
        bool runPressed = Input.GetKey("left shift");

        if (forwardPressed)
        {
            velocity += Time.deltaTime * acce;
        }

        animator.SetFloat(VelocityHash, velocity);
    }
}
