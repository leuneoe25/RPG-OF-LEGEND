using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LastBreath", menuName = "CharacterSkill/Yasuo/R_LastBreath", order =4)]
public class LastBreath : Skill
{
    [SerializeField] private float CoolDown;
    [SerializeField] private float ExecuteTime;
    [SerializeField] private List<float> EffectDamage;
    private List<GameObject> floatEnemy;

    private GameObject effect;
    public override string CheckExcuteSkill()
    {
        return null;
    }

    public override IEnumerator CheckExecute(GameObject myObj)
    {
        yield break;
    }

    public override IEnumerator Execute(GameObject myObj)
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(myObj.transform.position, 100.0f,LayerMask.GetMask("EnemyBody"));
        floatEnemy = new List<GameObject>();
        foreach (var item in colls)
        {
            if(item.gameObject.GetComponent<PlayerStateController>().GetNowState("공중"))
            {
                floatEnemy.Add(item.gameObject);
            }
        }

        if(floatEnemy.Count == 0)
        {
            Debug.Log("공중에 있는 적이 없습니다");
            CooldownManager.ins.AddCoolDown(SkillType.P, 0, 0, "LastBreath");
            yield break;
        }

        CooldownManager.ins.AddCoolDown(SkillType.P, ExecuteTime, CoolDown, "LastBreath");
        Debug.Log("LastBreath");
        foreach (var item in floatEnemy)
        {
            item.GetComponent<EnemyMove>().CrowdControl(2f);
            DOTween.Kill(item.gameObject.transform);
        }
        myObj.GetComponent<Rigidbody2D>().gravityScale = 0;
        myObj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if(myObj.transform.localScale.x > 0)
            myObj.transform.localScale = new Vector3(myObj.transform.localScale.x *-1, myObj.transform.localScale.y, 0);

        Vector3 pos = floatEnemy[floatEnemy.Count - 1].transform.position;
        myObj.transform.DOMove(new Vector3(pos.x+1.7f, pos.y + 1),0.5f);
        yield return new WaitForSeconds(0.7f);

        effect = myObj.transform.GetChild(4).gameObject;
        effect.SetActive(true);
        effect.transform.position = pos;
        for (int i = 0;i<effect.transform.childCount;i++)
        {
            
            if (i != 0)
                effect.transform.GetChild(i-1).gameObject.SetActive(false);
            effect.transform.GetChild(i).gameObject.SetActive(true);

            effect.transform.GetChild(i).GetComponent<Details_LastBreath>().Damage
                = EffectDamage[i] * myObj.GetComponent<Entity>().GetStatus().ADAtk;
            yield return new WaitForSeconds(0.2f);
        }
        effect.transform.GetChild(effect.transform.childCount - 1).gameObject.SetActive(false);

        myObj.GetComponent<Rigidbody2D>().gravityScale = 5;
        foreach (var item in floatEnemy)
        {
            if(item != null)
                item.GetComponent<Rigidbody2D>().gravityScale = 5;
        }

        yield break;
    }
}

