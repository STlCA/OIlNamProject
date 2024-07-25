using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimation : MonoBehaviour
{
    public Animator unitAnimator;
    public Animator skillAnimator;

    public void AttackEffect()
    {
        unitAnimator.SetTrigger("Attack");
    }
    public void AttackSkillEffect()
    {
        skillAnimator.SetTrigger("Attack");
    }

}
