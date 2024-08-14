using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageAnimation : MonoBehaviour
{
    public Animator animator;
    private static int stage = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (stage != 0)
            SetStage(stage);
    }

    public void SetStage(int setStage)//btn
    {
        stage = setStage;

        animator.SetBool("1", false);
        animator.SetBool("2", false);

        animator.SetBool(stage.ToString(), true);
    }

    public void ReAnim()
    {
        if (stage == 0)
            return;

        animator.SetBool("1", false);
        animator.SetBool("2", false);

        animator.SetBool(stage.ToString(), true);
    }
}
