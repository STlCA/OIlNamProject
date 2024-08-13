using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEvent : Manager
{
    public TMP_InputField couponInput;
    public GameObject falseCoupon;//��ȣƲ��
    public GameObject falseUseCoupon;//�̹̻��

    public static bool UseStartCoupon { get; private set; } = false;
    public static bool Stage1Clear { get; private set; } = false;

    public void CouponUISetting(TMP_InputField field, GameObject false1, GameObject false2)
    {
        couponInput = field;
        falseCoupon = false1;
        falseUseCoupon = false2;
    }

    public void CouponCheck()
    {
        if (couponInput.text == "2024TTAL0816")
        {
            CanUseStartCoupon();
            couponInput.text = "";

            return;
        }
        else
        { 
            falseCoupon.SetActive(true);
        }
    }

    private  void CanUseStartCoupon()
    {
        if (UseStartCoupon == true)
        {
            falseUseCoupon.SetActive(true);
            return;
        }

        UseStartCoupon = true;
    }

    public void StageClear()
    {
        if (Stage1Clear == true)
            return;
        else
            Stage1Clear = true;      
    }
}
