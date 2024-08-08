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

    public void TypeSet(string type)
    {
        this.type = type;

        if (type == "Sword")
            unitAnimator.SetBool("Sword", true);
        else if (type == "Archer")
            unitAnimator.SetBool("Archer", true);
        else if (type == "Wizard")
            unitAnimator.SetBool("Wizard", true);
        else
            Debug.Log("¿Ø¥÷≈∏¿‘º≥¡§æ»µ ");        
    }

    public void AttackEffect()
    {
        unitAnimator.SetTrigger("Attack");
    }
    public void AttackSkillEffect()
    {
        skillAnimator.SetTrigger("Attack");
    }

}
