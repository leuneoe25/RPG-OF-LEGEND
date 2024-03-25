using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Player : Entity
{
    [SerializeField] private GameObject IngameHpbar;
    private HPBar ingame_bar;
    [SerializeField] private GameObject Hpbar;
    [SerializeField] private GameObject Expbar;
    private float exp = 0;
    private void Start()
    {
        exp = 0;
        NowHp = Status.GetStatus(Lv).Health;
        Debug.Log(NowHp / Status.GetStatus(Lv).Health);
        float maxExp = 280 + ((Lv - 1) * 100);
        Expbar.GetComponent<UIBar>().ChangeValue(exp / maxExp, $"{exp}/{maxExp} ({(exp / maxExp * 100).ToString("##.##")}%)");

        Hpbar.GetComponent<UIBar>().ChangeValue(1,
               $"{NowHp}/{Status.GetStatus(Lv).Health}");

        ingame_bar = Instantiate(IngameHpbar).GetComponent<HPBar>();
        ingame_bar.Init(Color.green, Lv, gameObject);

    }
    public override void GetDamage(float damage, GameObject obj = null)
    {
        float realDamage = damage - Status.GetStatus(Lv).ADDef;
        if (realDamage > 0)
        {
            NowHp -= realDamage;
            DamageTextSystem.Instance.ShowDamage((int)realDamage, transform.position, Color.white);

            Hpbar.GetComponent<UIBar>().ChangeValue(NowHp / Status.GetStatus(Lv).Health,
                $"{NowHp.ToString("##.##")}/{Status.GetStatus(Lv).Health}");
            ingame_bar.ChageValue(NowHp / Status.GetStatus(Lv).Health,Lv);
        }

        if (NowHp <= 0)
        {
            ingame_bar.ChageValue(0,Lv);
            //Die
            //Die();
        }
        StartCoroutine(Invincibility());
    }

    public void AddExp(int m_exp)
    {
        exp += m_exp;
        float maxExp = 280 + ((Lv-1) * 100);
        if(exp >= maxExp)
        {
            exp -= maxExp;
            Lv += 1;
            maxExp += 100;

            NowHp = Status.GetStatus(Lv).Health;
            Hpbar.GetComponent<UIBar>().ChangeValue(NowHp / Status.GetStatus(Lv).Health,
                $"{NowHp.ToString("##.##")}/{Status.GetStatus(Lv).Health}");
            ingame_bar.ChageValue(NowHp / Status.GetStatus(Lv).Health, Lv);
        }

        Expbar.GetComponent<UIBar>().ChangeValue(exp / maxExp, $"{exp}/{maxExp} ({(exp / maxExp * 100).ToString("##.##")}%)");
    }
    private IEnumerator Invincibility()
    {
        gameObject.layer = 12;
        yield return new WaitForSeconds(0.1f);
        gameObject.layer = 8;
    }
}
