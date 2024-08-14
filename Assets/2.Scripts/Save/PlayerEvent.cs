using Constants;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEvent : Manager
{
    public TMP_InputField couponInput;
    public GameObject falseCoupon;//번호틀림
    public GameObject falseUseCoupon;//이미사용
    public GameObject canUseCoupon;//사용한UI

    public static bool UseStartCoupon { get; private set; } = false;
    public static bool Stage1Clear { get; private set; } = false;

    public void CouponUISetting(TMP_InputField field, GameObject false1, GameObject false2,GameObject canUseCoupon)
    {
        couponInput = field;
        falseCoupon = false1;
        falseUseCoupon = false2;
        this.canUseCoupon = canUseCoupon;
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

        GameManager.Instance.SoundManager.EffectAudioClipPlay(EffectList.Recall);
        GameManager.Instance.MoneyChange(MoneyType.Diamond, 50);
        UseStartCoupon = true;
        canUseCoupon.SetActive(true);
    }

    public void StageClear()
    {
        if (Stage1Clear == true)
            return;
        else
            Stage1Clear = true;      
    }
}
