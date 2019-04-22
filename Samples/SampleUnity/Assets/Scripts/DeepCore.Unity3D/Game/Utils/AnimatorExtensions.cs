using System.Collections;
using UnityEngine;

public static class AnimatorExtensions
{
    public static bool StateExists(this Animator animator, string stateName)
    {
        return animator.HasState(0, Animator.StringToHash(stateName));
    }
}