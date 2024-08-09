using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
    public enum OpeningBGM
    {
        BGM_SAMPLE1,
        
    }

    public enum EffectBGM
    {
        EFFECT_SAMPLE1,
    }

    public enum UIEffectType
    {
        TEXT,
        IMAGE,
        OBJECT,
    }

    public enum MoneyType
    {
        Gold,
        KEY,
        Diamond,
    }

    public enum PlusChangeType
    {
        FixChange,
        NormalChange,
        LethalChange,
        DeleteChange,
    }

    public enum EffectList
    {
        Intro,
        Recall,
        Lobby,
    }

    public enum PieceType//Unit S A B
    {
        Unit,
        STier,
        ATier,
        BTier,
    }

    public enum UnitTier
    {
        STier = 1,
        ATier,
        BTier,
    }

    public enum TabType
    {
        Shop,
        Home,
        Gacha,
        Unit,
    }

    public enum UnitType
    {
        Sword,
        Archer,
        Wizard,
    }
}