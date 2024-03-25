using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class Enemy : Entity
{
    [SerializeField] private GameObject HpBar;
    private HPBar bar;
    private GameObject LastAttackPlayer;
    
    private void Start()
    {
        NowHp = Status.GetStatus(Lv).Health;
        bar = Instantiate(HpBar).GetComponent<HPBar>();
        bar.Init(Color.red,Lv,gameObject);
        Debug.Log(NowHp.ToString());

        
    }
    public override void GetDamage(float damage,GameObject obj = null)
    {
        if(obj != null)
            LastAttackPlayer = obj;


        float realDamage = damage - Status.GetStatus(Lv).ADDef;
        if(realDamage > 0)
        {
            NowHp -= realDamage;
            DamageTextSystem.Instance.ShowDamage((int)realDamage, transform.position, Color.white);
            bar.ChageValue(NowHp / Status.GetStatus(Lv).Health);
        }
        
        if (NowHp <= 0)
        {
            bar.ChageValue(0);
            if(LastAttackPlayer != null)
            {
                LastAttackPlayer.GetComponent<Player>().AddExp(30);
            }
            //Die
            gameObject.transform.DOKill();
            Die();
        }
        StartCoroutine(Invincibility());
    }
    private void Die()
    {
        gameObject.transform.DOScaleY(0, 0.6f).OnComplete(() =>
        {
            gameObject.SetActive(false);
            Destroy(bar.gameObject);
            //StartCoroutine(Desto());
        });
    }
    private IEnumerator Desto()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    private IEnumerator Invincibility()
    {
        gameObject.layer = 12;
        yield return new WaitForSeconds(0.1f);
        gameObject.layer = 10;
    }
    
}
