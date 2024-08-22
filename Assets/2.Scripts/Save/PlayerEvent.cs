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
    public bool Stage1Clear;
    public bool FirstGacha;
    public bool GoldenPass;
    public bool DelayCoupon;
    public bool Package1;
}

public class PlayerEvent : Manager
{
    public TMP_InputField couponInput;
    public GameObject falseCoupon;//번호틀림
    public GameObject falseUseCoupon;//이미사용
    public GameObject canUseCoupon;//사용한UI    
    public GameObject useDelayCoupon;

    public static bool UseStartCoupon { get; private set; } = false;
    public static bool Stage1Clear { get; private set; } = false;
    public static bool FirstGacha { get; private set; } = false;
    public static bool GoldenPass { get; private set; } = false;
    public static bool DelayCoupon { get; private set; } = false;
    public static bool Package1 { get; private set; } = false;

    public void CouponUISetting(TMP_InputField field, GameObject false1, GameObject false2, GameObject canUseCoupon,GameObject useDelayCoupon)
    {
        couponInput = field;
        falseCoupon = false1;
        falseUseCoupon = false2;
        this.canUseCoupon = canUseCoupon;
        this.useDelayCoupon = useDelayCoupon;
    }

    public void CouponCheck()
    {
        if (couponInput.text == "2024TTAL0816")
        {
            CanUseStartCoupon();
        }
        else if (couponInput.text == "1s3bhellingfarm")
        {
            HellingFarm();
        }
        else if (couponInput.text == "IMSORRY0820")
        {
            CanDelayCoupon();
        }
        else
        {
            falseCoupon.SetActive(true);
        }

        couponInput.text = "";
    }

    private void CanDelayCoupon()
    {
        if (DelayCoupon == true)
        {
            falseUseCoupon.SetActive(true);
            return;
        }

        GameManager.Instance.MoneyChange(MoneyType.KEY, 30);
        GameManager.Instance.MoneyChange(MoneyType.Gold, 1000);
        GameManager.Instance.UnitManager.ChangeUnitPiece(100);

        DelayCoupon = true;
        useDelayCoupon.SetActive(true);
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

    public void BuyPackage1()
    {
        Package1 = true;
    }

    //-----------------------------------------------------SaveLoad

    public void Save(ref Save_PlayerEvenet saveData)
    {
        saveData.UseStartCoupon = UseStartCoupon;
        saveData.Stage1Clear = Stage1Clear;
        saveData.FirstGacha = FirstGacha;
        saveData.GoldenPass = GoldenPass;
        saveData.DelayCoupon = DelayCoupon;
        saveData.Package1 = Package1;
    }

    public void Load(Save_PlayerEvenet saveData)
    {
        UseStartCoupon = saveData.UseStartCoupon;
        Stage1Clear = saveData.Stage1Clear;
        FirstGacha = saveData.FirstGacha;
        GoldenPass = saveData.GoldenPass;
        DelayCoupon = saveData.DelayCoupon;
        Package1 = saveData.Package1;
    }
}
