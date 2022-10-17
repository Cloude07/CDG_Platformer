using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorState : MonoBehaviour
{
    Animator animator;
    private string currentAnimatorState;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentAnimatorState == newState) return;
        animator.Play(newState);

        currentAnimatorState = newState;
    }

}
