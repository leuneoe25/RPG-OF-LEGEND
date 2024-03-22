using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WindWall", menuName = "CharacterSkill/Yasuo/W_WindWall", order = 2)]
public class WindWall : Skill
{
    [SerializeField] private GameObject WindWallObj;
    [SerializeField] private float ObjectholdingTime;
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
        CooldownManager.ins.AddCoolDown(SkillType.I, ExecuteTime, CoolDown, "WindWall");

        Debug.Log("WindWall");
        GameObject g = Instantiate(WindWallObj, new Vector3(myObj.transform.position.x, myObj.transform.position.y,0), Quaternion.identity);
        g.GetComponent<Details_WindWall>().holdingTime = ObjectholdingTime;
        g.transform.localScale = new Vector3(0, 0, 1);
        if (myObj.transform.localScale.x > 0)
        {
            g.transform.DOLocalMoveX(myObj.transform.position.x + 2f, 1f);
            g.transform.DOScale(new Vector3(1, 1), 1);
        }
        else
        {
            g.transform.DOLocalMoveX(myObj.transform.position.x  - 2f, 1f);
            g.transform.DOScale(new Vector3(-1, 1), 1);
        }

        yield break;
    }
}
