using Constants;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitAnimation : MonoBehaviour
{
    public string type;

    public Animator unitAnimator;
    public Animator skillAnimator;

    public void TypeSet(string type, Animator sa, int tier)
    {
        this.type = type;
        skillAnimator = sa;

        if (type == "Sword")
        {
            unitAnimator.SetBool("Sword", true);
            skillAnimator.SetBool("Sword", true);
        }
        else if (type == "Archer")
        {
            unitAnimator.SetBool("Archer", true);
            skillAnimator.SetBool("Archer", true);
        }
        else if (type == "Wizard")
        {
            unitAnimator.SetBool("Wizard", true);
            skillAnimator.SetBool("Wizard", true);
        }
        else
            Debug.Log("¿Ø¥÷≈∏¿‘º≥¡§æ»µ ");

        skillAnimator.SetBool(tier.ToString(), true);
    }

    public void AttackEffect()
    {
        unitAnimator.SetTrigger("Attack");
        skillAnimator.SetTrigger("Attack");
    }
    public void AttackSkillEffect()
    {
    }

}
