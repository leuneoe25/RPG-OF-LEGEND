using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Status
{
    public float Health;
    public float HealthRegeneration;
    public float ADAtk;
    public float APAtk;
    public float ADDef;
    public float APDef;
    public float Speed;
}

[CreateAssetMenu(fileName = "CharacterStatusData", menuName = "Character/CharacterStatus")]
public class CharacterStatus : ScriptableObject
{
    public Status StartStatus;
    public Status GrowthStatus;

    public Status GetStatus(int Lv)
    {
        Status n_status = StartStatus;
        n_status.Health += GrowthStatus.Health * Lv;
        n_status.HealthRegeneration += GrowthStatus.HealthRegeneration * Lv;
        n_status.ADAtk += GrowthStatus.ADAtk * Lv;
        n_status.APAtk += GrowthStatus.APAtk * Lv;
        n_status.ADDef += GrowthStatus.ADDef * Lv;
        n_status.APDef += GrowthStatus.APDef * Lv;

        return n_status;
    }
}

