using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public CharacterStatus Status;
    public int Lv = 1;

    public float NowHp;
    private void Start()
    {
        NowHp = Status.GetStatus(Lv).Health;
    }
    public Status GetStatus()
    {
        return Status.GetStatus(Lv);
    }

    public abstract void GetDamage(float damage, GameObject obj = null);
}
