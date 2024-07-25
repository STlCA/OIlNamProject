using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAnimation : MonoBehaviour
{
    private Animator[] animator;

    private void Start()
    {
        animator = GetComponentsInChildren<Animator>();
        animator[1].SetTrigger("useSkill");
    }
}
