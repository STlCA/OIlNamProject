using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct Save_PlayerEvenet
{
    public bool UseStartCoupon;
    public bool UseSorryCoupon;
    public bool Stage1Clear;
    public bool FirstGacha;
    public bool GoldenPass;
}

public class PlayerEvent : Manager
{
    public TMP_InputField couponInput;
    public GameObject falseCoupon;//번호틀림
    public GameObject falseUseCoupon;//이미사용
    public GameObject canUseCoupon;//사용한UI    
    public GameObject canUseSorryCoupon;  

    public static bool UseStartCoupon { get; private set; } = false;
    public static bool UseSorryCoupon { get; private set; } = false;
    public static bool Stage1Clear { get; private set; } = false;
    public static bool FirstGacha { get; private set; } = false;
    public static bool GoldenPass { get; private set; } = false;

    public void CouponUISetting(TMP_InputField field, GameObject false1, GameObject false2, GameObject canUseCoupon, GameObject canUseSorryCoupon)
    {
        couponInput = field;
        falseCoupon = false1;
        falseUseCoupon = false2;
        this.canUseCoupon = canUseCoupon;
        this.canUseSorryCoupon = canUseSorryCoupon;
    }

    public void CouponCheck()
    {
        if (couponInput.text == "2024TTAL0816")
        {
            CanUseStartCoupon();
            couponInput.text = "";

            return;
        }
        else if (couponInput.text == "1s3bhellingfarm")
        {
            HellingFarm();
            couponInput.text = "";
        }
        else if (couponInput.text == "IMSORRY0820")
        {
            CanUseSorryCoupon();
            couponInput.text = "";
        }
        else
        {
            falseCoupon.SetActive(true);
        }
    }

    private void CanUseStartCoupon()
    {
        if (UseStartCoupon == true)
        {
            falseUseCoupon.SetActive(true);
            return;
        }

        GameManager.Instance.SoundManager.EffectAudioClipPlay(EffectList.Recall);
        GameManager.Instance.MoneyChange(MoneyType.Diamond, 50);
        GameManager.Instance.UnitManager.ChangeUnitPiece(100);
        UseStartCoupon = true;
        canUseCoupon.SetActive(true);
    }

    private void CanUseSorryCoupon()
    {
        if (UseSorryCoupon == true)
        {
            falseUseCoupon.SetActive(true);
            return;
        }

        GameManager.Instance.SoundManager.EffectAudioClipPlay(EffectList.Recall);
        GameManager.Instance.MoneyChange(MoneyType.Gold, 1000);
        GameManager.Instance.MoneyChange(MoneyType.KEY, 30);
        GameManager.Instance.UnitManager.ChangeUnitPiece(100);
        UseSorryCoupon = true;
        canUseSorryCoupon.SetActive(true);
    }

    private void HellingFarm()
    {
        GameManager.Instance.MoneyChange(MoneyType.Gold, 100000);
        GameManager.Instance.MoneyChange(MoneyType.Diamond, 100000);
        GameManager.Instance.MoneyChange(MoneyType.KEY, 100000);
        GameManager.Instance.UnitManager.ChangeUnitPiece(100000);
        GameManager.Instance.UnitManager.ChangePiece(PieceType.STier, 100000);
        GameManager.Instance.UnitManager.ChangePiece(PieceType.ATier, 100000);
        GameManager.Instance.UnitManager.ChangePiece(PieceType.BTier, 100000);
    }

    public void StageClear()
    {
        if (Stage1Clear == true)
            return;
        else
            Stage1Clear = true;
    }

    public void FirstGachaStart()
    {
        if (!FirstGacha)
            FirstGacha = true;
        else
            return;
    }

    public void BuyGoldenPass()
    {
        GoldenPass = true;
    }


    //-----------------------------------------------------SaveLoad

    public void Save(ref Save_PlayerEvenet saveData)
    {
        saveData.UseStartCoupon = UseStartCoupon;
        saveData.UseSorryCoupon = UseSorryCoupon;
        saveData.Stage1Clear = Stage1Clear;
        saveData.FirstGacha = FirstGacha;
        saveData.GoldenPass = GoldenPass;
    }

    public void Load(Save_PlayerEvenet saveData)
    {
        UseStartCoupon = saveData.UseStartCoupon;
        UseSorryCoupon = saveData.UseSorryCoupon;
        Stage1Clear = saveData.Stage1Clear;
        FirstGacha = saveData.FirstGacha;
        GoldenPass = saveData.GoldenPass;
    }
}
