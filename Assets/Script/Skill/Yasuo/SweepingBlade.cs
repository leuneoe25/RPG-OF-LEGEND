using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SweepingBlade", menuName = "CharacterSkill/Yasuo/E_SweepingBlade", order = 3)]
public class SweepingBlade : Skill
{
    [SerializeField] private float CoolDown;
    [SerializeField] private float ExecuteTime;

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

        CooldownManager.ins.AddCoolDown(SkillType.O, ExecuteTime, CoolDown, "SweepingBlade");
        myObj.GetComponent<Rigidbody2D>().gravityScale = 0;
        myObj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (myObj.transform.localScale.x > 0)
        {
            myObj.transform.DOLocalMoveX(myObj.transform.position.x + 2f, ExecuteTime).OnComplete(() =>
            {
                myObj.GetComponent<Rigidbody2D>().gravityScale = 5;
            });
        }
        else
        {
            myObj.transform.DOLocalMoveX(myObj.transform.position.x - 2f, ExecuteTime).OnComplete(() =>
            {
                myObj.GetComponent<Rigidbody2D>().gravityScale = 5;
            });

        }
        yield break;
    }
}
